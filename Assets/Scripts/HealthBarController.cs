using System.Collections;
using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public GameObject Background;
    public GameObject Damage;
    public GameObject Health;

    private float maxHealth = 100f;
    private float currentHealth = -1f;

    private float damageHealth = -1f;
    private Coroutine damageCoroutine;
    private float applyWindowTime = 1.0f;

    // Call once, in start method of associated player/enemy script
    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        if (currentHealth == -1f)
        {
            currentHealth = health;
        }
        if (damageHealth == -1f)
        {
            damageHealth = health;
        }
    }
    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        UpdateCurrentHealth();
        if (damageCoroutine != null) 
        {
            StopCoroutine(damageCoroutine);
        }
        damageCoroutine = StartCoroutine(ApplyWindow());
    }

    private void UpdateCurrentHealth()
    {
        Health.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
        Health.transform.localPosition = new Vector3(-(1 - currentHealth / maxHealth)/2, 0, 0);
    }

    private void UpdateDamage()
    {
        damageCoroutine = null;
        StartCoroutine(UpdateDamageOverTime(0.5f));
    }

    // reduces the healthbar one damage point at a time over duration seconds
    private IEnumerator UpdateDamageOverTime(float duration)
    {
        float targetHealth = currentHealth;
        float waitTime = duration / (damageHealth - targetHealth);

        while (damageHealth >= targetHealth)
        {
            Damage.transform.localScale = new Vector3(damageHealth / maxHealth, 1, 1);
            Damage.transform.localPosition = new Vector3(-(1 - damageHealth / maxHealth) / 2, 0, 0);
            damageHealth--;
            yield return new WaitForSeconds(waitTime);
        }

        damageHealth = targetHealth;
    }

    private IEnumerator ApplyWindow()
    {
        yield return new WaitForSeconds(applyWindowTime);
        UpdateDamage();
    }
}
