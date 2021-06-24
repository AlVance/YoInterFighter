using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public DinoMainManager dMM;
    private Rigidbody2D rb;
    private BoxCollider2D bC;
    public GameObject playerSprite;
    public Animator anim;

    public Transform groundCheck;
    private bool isGrounded;
    public LayerMask m_WhatIsGround;
    public float groundCheckRadius;
    public Transform jumpHeight;
    
    public float jumpForce;
    //public float fallMultiplier = 2.5f;
    //public float lowJumpMultiplier = 2f;
    //public float jumpingGravityScale = 0.8f;
    private float initialGravityScale;
    public float fallGravityScale;

    private bool jumpRequest;
    private bool canJump = true;
    // Start is called before the first frame update
    void Start()
    {
        
        bC = this.GetComponent<BoxCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        initialGravityScale = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
           
        Jump();
        Crouch();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, m_WhatIsGround);
       if(isGrounded)
       {
            anim.SetBool("IsRunning", true);
            anim.SetBool("IsJumping", false);
        }
        else
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsJumping", true);
        }
        
        if (jumpRequest && canJump)
        {
            rb.gravityScale = initialGravityScale;        
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime);
            //rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            if(this.transform.position.y >= jumpHeight.position.y || !Input.GetKey(KeyCode.UpArrow))
            {
                //jumpRequest = false;
                StartCoroutine(StayUpCD());
            }
        }

        /*if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = jumpingGravityScale;
        }*/
    }

    private void Jump()
    {
        
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
                       
            jumpRequest = true;
                        
        }
    }

    private void Crouch()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                bC.size = new Vector2(0.5f, 1);
                bC.offset = new Vector2(0, -0.5f);
                //playerSprite.transform.localScale = new Vector3(0.5f, 0.25f, 1);
                anim.SetBool("IsSliding", true);
            }
            else
            {
                bC.size = new Vector2(0.5f, 2);
                bC.offset = new Vector2(0, 0);
                //playerSprite.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                anim.SetBool("IsSliding", false);

            }
        }
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Obstacle")
        {
            Time.timeScale = 0;
            dMM.gameOverScreen.SetActive(true);
        }
    }

    private IEnumerator StayUpCD()
    {
        canJump = false;
        //rb.gravityScale = 0f;
        yield return new WaitForSeconds(0.1f);
        rb.gravityScale = fallGravityScale;
        jumpRequest = false;
        canJump = true;
    }
}
