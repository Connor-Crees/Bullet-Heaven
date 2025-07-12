using UnityEngine;
using TMPro;
using System;

public class DamagePopupController : MonoBehaviour
{
    private TextMeshPro textMesh;

    public float dissapearTimer = 
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(float damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
    }

    private void Update() {
        transform.position += new Vec
    }
}
