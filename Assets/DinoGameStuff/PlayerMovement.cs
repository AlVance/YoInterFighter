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
    public GameObject jumpParticles;

    public Transform groundCheck;
    private bool isGrounded;
    
    private float initialGravityScale;

    private bool isSliding = false;

    private float upwardsVelocity = 0.0f;
    public float upwardsJumpForce = 0.0f;
    public float gravityMultiplier = 100.0f;
    public float maxJumpHeight = 2.0f;
    private float iniY = 0.0f;

    public ParticleSystem partJump;

    // Start is called before the first frame update
    void Start()
    {
        iniY = transform.position.y;
        bC = this.GetComponent<BoxCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        initialGravityScale = rb.gravityScale;

        Physics.gravity = Vector3.down * gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
           
        Jump();
        Crouch();
        anim.SetBool("IsSliding", isSliding);

    }

    private void Jump()
    {
        float dt = Time.deltaTime;

        upwardsVelocity += dt * Physics.gravity.y;
        
        if (Input.GetKey(KeyCode.UpArrow) && (transform.position.y <= iniY + maxJumpHeight && upwardsVelocity > 0 || isGrounded))
        {
            partJump.Play();
            upwardsVelocity = upwardsJumpForce;
        }
        
        transform.position += Vector3.up * upwardsVelocity * dt;

        isGrounded = transform.position.y <= iniY;

        if (isGrounded)
        {
            upwardsVelocity = 0.0f;
        }

        Vector3 currentPosition = transform.position;
        currentPosition.y = Mathf.Max(currentPosition.y, iniY);
        transform.position = currentPosition;


        if (isGrounded)
        {
            anim.SetBool("IsJumping", false);
        }
        else
        {
            
            anim.SetBool("IsJumping", true);
            
            isSliding = false;
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
                
                isSliding = true;
            }
            else
            {
                isSliding = false;
                bC.size = new Vector2(0.5f, 2);
                bC.offset = new Vector2(0, 0);
                //playerSprite.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                isSliding = false;


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

 
}
