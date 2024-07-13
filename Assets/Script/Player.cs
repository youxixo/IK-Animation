using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce = 5;
    private bool isGrounded;
    private Rigidbody2D rb;
    private Animator animator;
    private bool airAttack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.Play("idle");
    }

    void Update()
    {
        SwitchAnimation();
    }

    private void SwitchAnimation()
    {
        bool run = Input.GetKey(KeyCode.D);
        bool attack = Input.GetKey(KeyCode.J) && isGrounded;
        bool jump = animator.GetBool("jump");
        airAttack = Input.GetKey(KeyCode.J) && !isGrounded;

        if (run)
        {
            animator.SetBool("run", true);
            if (attack)
            {
                animator.SetBool("attack", true);
            }
            else
            {
                animator.SetBool("attack", false);
            }
        }
        else
        {
            animator.SetBool("run", false);
            animator.SetBool("attack", attack);
        }

        if (airAttack)
        {
            animator.SetBool("airAttack", true);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jump = true;
            Jump();
        }
        else if (!isGrounded)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        animator.SetBool("jump", jump);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            animator.SetBool("jump", false);
            animator.SetBool("airAttack", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
}

