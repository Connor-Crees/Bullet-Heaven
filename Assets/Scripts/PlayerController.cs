using System;
using System.Collections;
using TMPro;
using Unity.Hierarchy;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private float horizontal;
    private float vertical;
    private float diagonalScaler = 0.7f;

    private bool invuln = false;

    private int enemyCollisionCount = 0;

    [Header("Move Speed")]
    public float moveSpeed = 1.0f;

    [Header("Health")]
    public HealthBarController hb;
    public float maxHealth = 100.0f;
    public float currentHealth = 100.0f;

    [Header("Damage Invuln")]
    public float invulnTime = 1.0f;

    [Header("Game Objects")]
    public GameObject blood;
    public AudioSource hurtSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hb.SetMaxHealth(maxHealth);
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        // flip sprite
        if (horizontal < 0)
        {
            sr.flipX = true;
        }
        else if (horizontal > 0)
        {
            sr.flipX = false;
        }

        // reduce speed on diagonal
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= diagonalScaler;
            vertical *= diagonalScaler;
        }

        rb.linearVelocity = new Vector2 (horizontal * moveSpeed, vertical * moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemyCollisionCount++;
            if (enemyCollisionCount == 1)
            {
                blood.SetActive(true);
                sr.color = Color.red;
                if (!hurtSound.isPlaying)
                {
                    hurtSound.Play();
                }
                hurtSound.loop = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyCollisionCount--;
            if (enemyCollisionCount == 0)
            {
                blood.SetActive(false);
                sr.color = Color.white;
                hurtSound.loop = false;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!invuln)
            {
                invuln = true;
                StartCoroutine(Invuln());
                EnemyController ec = collision.gameObject.GetComponent<EnemyController>();
                float damage = ec.contactDamage;
                hb.ApplyDamage(damage);
                currentHealth -= damage;
            }
        }
    }

    private IEnumerator Invuln()
    {
        yield return new WaitForSeconds(invulnTime);
        invuln = false;
    }
}
