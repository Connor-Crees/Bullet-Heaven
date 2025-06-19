using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private Vector3 initialScale;
    private Rigidbody2D rb;

    public float moveSpeed = 1.0f;
    public float contactDamage = 1.0f;

    [Header("Health")]
    public HealthBarController hb;
    public float maxHealth = 100.0f;
    public float currentHealth = 100.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hb.SetMaxHealth(maxHealth);
        player = GameObject.FindWithTag("Player").transform;
        initialScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // move towards player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        rb.linearVelocity = Vector2.zero;

        float horizontal = transform.position.x - player.position.x;
        // flip horizontal scale
        if (horizontal > 0)
        {
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            ProjectileController pc = collision.GetComponentInParent<ProjectileController>();
            float damage = pc.damage;
            hb.ApplyDamage(damage);
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Death();
            }
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
