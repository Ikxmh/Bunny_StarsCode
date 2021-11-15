using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // foundations of movement
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private float groundCheckRadius = 0.10f;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private LayerMask whatIsGround;


    // back-end of the player
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private bool isGrounded = false;
    private bool isFacingRight = true;
    private bool isDucking = false;


    // Start is called before the first frame update
    void Start()
    {
       
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // horizontal movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = GroundCheck();

        bool duck = Input.GetKey(KeyCode.S);

        // jumping/vertical movement
        if (isGrounded && Input.GetAxisRaw("Jump") > 0)
        {
            rb.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false; 
        }

        // Ducking 

        // when moving right or left-- ducking cannot be used

        if (!duck || (rb.velocity.x < 0 || rb.velocity.x > 0))
        {
            isDucking = false;
        }

        // else work
        else if (rb.velocity.x == 0 && duck)
        {
            isDucking = true;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        // check if the player character need to be flipped
        if(isFacingRight && rb.velocity.x < 0 || !isFacingRight && rb.velocity.x > 0)
        {
            Flip();
        }

        anim.SetFloat("xSpeed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("ySpeed", rb.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDucking", isDucking);
    }

    // check the ground based on the radius and ground check gameobject
    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, whatIsGround);
    }

    private void Flip()
    {
        Vector3 flip = transform.localScale;
        flip.x *= -1;

        transform.localScale = flip;
        isFacingRight = !isFacingRight;
    }
}
