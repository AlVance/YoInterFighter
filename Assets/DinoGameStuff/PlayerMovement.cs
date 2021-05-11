using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bC;
    public GameObject playerSprite;

    public Transform groundCheck;
    private bool isGrounded;
    public LayerMask m_WhatIsGround;
    public float groundCheckRadius;
    
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private bool jumpRequest;
    // Start is called before the first frame update
    void Start()
    {
        bC = this.GetComponent<BoxCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, m_WhatIsGround);
        
        Jump();
        Crouch();
    }

    void FixedUpdate()
    {

        if (jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequest = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
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
                bC.size = new Vector2(1, 1);
                bC.offset = new Vector2(0, -0.5f);
                playerSprite.transform.localScale = new Vector3(0.5f, 0.25f, 1);
               
            }
            else
            {
                bC.size = new Vector2(1, 2);
                bC.offset = new Vector2(0, 0);
                playerSprite.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                
            }
        }
      
    }

   
}
