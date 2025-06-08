using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3 (player.position.x, player.position.y + 1.5f, -10f);
        transform.position = position;
    }
}
