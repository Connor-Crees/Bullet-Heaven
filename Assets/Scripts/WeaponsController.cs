using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    WeaponController[] weapons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weapons = GetComponentsInChildren<WeaponController>();
        foreach (WeaponController weapon in weapons)
        {
            weapon.EquipWeapon();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
