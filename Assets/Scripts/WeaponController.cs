using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Collider2D col;
    private Animator animator;

    public float damage = 10.0f;

    public float attackSpeed = 1.0f;
    public float attackDuration = 0.2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        col.enabled = false;
    }

    public void EquipWeapon()
    {
        StartCoroutine(AttackSequence());
    }

    private IEnumerator AttackSequence()
    {
        while (true)
        {
            StartCoroutine(Attack());
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private IEnumerator Attack()
    {
        col.enabled = true;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(attackDuration);
        col.enabled = false;
    }
}
