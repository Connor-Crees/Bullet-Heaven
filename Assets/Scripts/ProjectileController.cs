using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float damage = 1f;

    [SerializeField] public GameObject damagePopup;

    public void DamagePopup(float amount, Vector3 position)
    {
        GameObject dmgPopup = Instantiate(damagePopup, position, Quaternion.identity);
        DamagePopupController dpc = dmgPopup.GetComponent<DamagePopupController>();
        dpc.Setup(amount);
    }
}
