using UnityEngine;

public class MoveController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private float xInput;

    [Header("Collision Checks")]
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheck;
    public LayerMask whatIsGround;
    public bool isGrounded;

    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimationMoveController();

        CollisionChecks();

        FlipFaceController();

        xInput = Input.GetAxisRaw("Horizontal");

        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        
    }

    private void AnimationMoveController()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);

        anim.SetFloat("yVelocity", rb.velocity.y);

        anim.SetBool("isGrounded", isGrounded);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    private void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Jump()
    {
        if (isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }

    private void FlipFace()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipFaceController()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < transform.position.x && facingRight)
        {
            FlipFace();
        }
        else if (mousePos.x > transform.position.x && !facingRight)
        {
            FlipFace();
        }
    }
}
