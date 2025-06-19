using System.Collections;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    GameObject player;

    public GameObject[] fodder;
    private int fodderNum = 0;

    public float fodderInterval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(spawnFodder(fodderInterval));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnFodder(float interval)
    {
        while (true)
        {
            while (true)
            {
                
                Vector2 position = new Vector2(player.transform.localPosition.x + Random.Range(-10.0f, 10.0f), player.transform.localPosition.y + Random.Range(-10.0f, 10.0f));
                if (Vector2.Distance(position, player.transform.localPosition) > 10.0f)
                {
                    Instantiate(fodder[fodderNum % fodder.Length], position, Quaternion.identity);
                    break;
                }
            }
            

            yield return new WaitForSeconds(interval);
        }
    }
}
