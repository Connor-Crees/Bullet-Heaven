using System.Collections;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class WeaponController : MonoBehaviour
{
    public float attackSpeed = 1.0f;

    public GameObject projectile;

    public int projectiles = 1;

    public float projectileSpeed = 0.0f;

    public float projectileLifetime = 0.0f;

    public string projectilePattern = "Melee";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Update()
    {

    }

    public void EquipWeapon()
    {
        StartCoroutine(AttackSequence());
    }

    private IEnumerator AttackSequence()
    {
        while (true)
        {
            for (int i = 0; i < projectiles; i++)
            {
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                StartCoroutine(Lifetime(newProjectile));
            }
            yield return new WaitForSeconds(1.0f/attackSpeed);
        }
    }

    private IEnumerator Lifetime(GameObject projectile)
    {
        float time = projectileLifetime;
        if (projectileLifetime <= 0.0f) 
        {
            time = projectile.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
        yield return new WaitForSeconds(time);
        Destroy(projectile);
    }
}
