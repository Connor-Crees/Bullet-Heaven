using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SurvivorController : MonoBehaviour
{
    public GameObject survivor;
    public GameObject blood;
    public GameObject interactUI;

    [Header("Values")]
    public float healAmount = 20.0f;

    private InputAction interactAction;
    private bool dying = false;
    private Transform player;
    private PlayerController pc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactUI.SetActive(false);
        blood.SetActive(false);
        interactAction = InputSystem.actions.FindAction("Interact");
        GameObject playerObj = GameObject.FindWithTag("Player");
        player = playerObj.transform;
        pc = playerObj.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interactUI.activeSelf)
        {
            if (interactAction.IsPressed())
            {
                interactUI.SetActive(false);
                blood.SetActive(true);
                dying = true;
                survivor.transform.localRotation = Quaternion.Euler(0, 0, -90);
                StartCoroutine(playerEat());
            }
        }

        if (dying)
        {
            transform.localPosition = player.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            interactUI.SetActive(false);
        }
    }

    private IEnumerator playerEat()
    {
        yield return new WaitForSeconds(1.0f);
        pc.Heal(healAmount);
        Destroy(gameObject);
    }
}
