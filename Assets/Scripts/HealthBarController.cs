using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public GameObject Background;
    public GameObject Damage;
    public GameObject Health;

    private float maxHealth = 100f;
    private float currentHealth = -1f;

    // Call once, in start method of associated player/enemy script
    public void SetMaxHealth(float health)
    {
        maxHealth = health;
        if (currentHealth == -1f)
        {
            currentHealth = health;
        }
    }
    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;
        updateCurrentHealth();
    }

    private void updateCurrentHealth()
    {
        Health.transform.localScale = new Vector3(currentHealth / maxHealth, 1, 1);
    }
}
