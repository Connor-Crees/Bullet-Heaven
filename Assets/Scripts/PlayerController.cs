using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private float horizontal;
    private float vertical;
    private float diagonalScaler = 0.7f;

    public float moveSpeed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        // flip horizontal scale
        if (horizontal < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (horizontal > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }

        // reduce speed on diagonal
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= diagonalScaler;
            vertical *= diagonalScaler;
        }

        rb.linearVelocity = new Vector2 (horizontal * moveSpeed, vertical * moveSpeed);
    }
}
