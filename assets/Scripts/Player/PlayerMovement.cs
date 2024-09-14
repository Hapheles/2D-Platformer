using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private Vector3 respawnPoint;
    private GameObject attackArea = default;

    [SerializeField] private float speed ;
    [SerializeField] private float jumpingPower;

    private float horizontal;
    private bool isFacingRight = true;
    private bool isJumping = false;
    private bool canDoubleJump = false;
    private bool attacking = false;
    private float attackTimer = 0;
    private float timetoAttack = 0.25f;


    private void Awake()
    {
        //Grab ref for rigidbidy and animator from object
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        respawnPoint = transform.position;
        attackArea = transform.GetChild(0).gameObject;

    }

    private void FixedUpdate()
    {

        if (InputManager.GetInstance().RegisterAttackPressed() && !attacking)
        {
            Attack();
        }

        if (attacking)
        {
            attackTimer += Time.deltaTime;

            if(attackTimer >= timetoAttack)
            {
               EndAttack();
            }
        }
        

        if(DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        Vector2 moveInput = InputManager.GetInstance().GetMoveDirection();

        //Set horizontal movement based on input
        float horizontal = moveInput.x;
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }

        //Set anim parameters
        animator.SetBool("walk", horizontal != 0);
        animator.SetBool("grounded", IsGrounded());


        CheckGrounded();

        if (InputManager.GetInstance().GetJumpPressed())
        {
            Jump();
        }

    }   

    public void Jump()
    {
        if (IsGrounded() || (canDoubleJump && !isJumping))
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            if (!IsGrounded())
            {
                canDoubleJump = false; // Disable double jump after using it
            }
        }
    }

    private void CheckGrounded()
    {
        animator.SetBool("grounded", IsGrounded());
        if (IsGrounded())
        {
            isJumping = false;
            canDoubleJump = true; // Reset double jump when grounded
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void Attack()
    {
        attacking = true;
        attackTimer = 0;
        attackArea.SetActive(true);
        animator.SetTrigger("Attack");
    }

    private void EndAttack()
    {
        attacking = false;
        attackArea.SetActive(false);
        attackTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "NextLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (collision.tag == "PreviousLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

    }

    
}
