using System.Collections;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class WeaponController : MonoBehaviour
{
    private Collider2D col;
    private Animator animator;

    private Vector3 initialScale;

    public string weaponType = "Melee";

    public float damage = 10.0f;

    public float attackSpeed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        col.enabled = false;

        initialScale = transform.localScale;
    }

    private void Update()
    {
        if(weaponType == "Melee" && !col.enabled)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            if(horizontal < 0)
            {
                transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
            }
            if (horizontal > 0)
            {
                transform.localScale = initialScale;
            }
        }
    }

    public void EquipWeapon()
    {
        StartCoroutine(AttackSequence());
    }

    private IEnumerator AttackSequence()
    {
        while (true)
        {
            col.enabled = true;
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            col.enabled = false;
            yield return new WaitForSeconds(1.0f/attackSpeed);
        }
    }

}
