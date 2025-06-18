using System.Collections;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    GameObject player;

    public GameObject slime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(spawnSlimes(2.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnSlimes(float interval)
    {
        while (true)
        {
            while (true)
            {
                
                Vector2 position = new Vector2(player.transform.localPosition.x + Random.Range(-10.0f, 10.0f), player.transform.localPosition.y + Random.Range(-10.0f, 10.0f));
                if (Vector2.Distance(position, player.transform.localPosition) > 10.0f)
                {
                    Instantiate(slime, position, Quaternion.identity);
                    break;
                }
            }
            

            yield return new WaitForSeconds(interval);
        }
    }
}
