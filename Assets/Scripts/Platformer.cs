using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer : MonoBehaviour
{
    Rigidbody2D rb;
    MainManager mainMngr;

    public enum TypeAttack { Melee, Range}
    public TypeAttack typeSelect = TypeAttack.Melee;

    public bool player2;

    [Header("Player Local Variable")]
    public float speed;
    public float jumpForce;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool isGrounded = false;
    [Header("Grounded")]
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public PhysicsMaterial2D groundPhyMat;
    public PhysicsMaterial2D airPhyMat;

    public float rememberGroundedFor;
    float lastTimeGrounded;

    [Header("Additional Jumps")]
    public int defaultAdditionalJumps = 0;
    public float airJumpForce = 3;
    public float airControl = 1;
    int additionalJumps;

    [Header("Wall Jump")]
    public bool wallJump;
    bool onWall;
    public Transform isWallRightChecker;
    public Transform isWallLeftChecker;
    public float checkWallRadius;
    public LayerMask wallLayer;
    public int wallJumpCount;
    public float wallJumpForce;
    int wallJumpIndex;
    float xScale =1;

    bool hited;
    int percentDmg;
    public TextMesh percentText;

    public AttackMelee attackScrpt;
    public float x = 0;

    public GameObject particle_jump;

    // Start is called before the first frame update
    void Start()
    {
        mainMngr = FindObjectOfType<MainManager>();
        rb = GetComponent<Rigidbody2D>();
        additionalJumps = defaultAdditionalJumps;
        wallJumpIndex = wallJumpCount;
        SetPercentLife(true, 0, true);
        if (typeSelect == TypeAttack.Melee)
        {
            attackScrpt = GetComponent<AttackMelee>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!hited)
        {
            Move();
            Jump();
            BetterJump();
            CheckIfGrounded();
            if (wallJump)
            {
                CheckWalls();
            }
        }
    }

    void Move()
    {
        if (!player2)
        {
            x = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            x = Input.GetAxisRaw("Horizontal_2");
        }
        float moveBy = 0;
        if (isGrounded)
        {
            moveBy = x * speed;
            rb.sharedMaterial = groundPhyMat;
        }
        else
        {
            moveBy = x * speed * airControl;
            rb.sharedMaterial = airPhyMat;
        }
        if (x != 0)
        {
            rb.velocity = new Vector2(moveBy, rb.velocity.y);
        }
        if (x < 0)
        {
            xScale = -1;
        }
        if (x > 0)
        {
            xScale = 1;
        }
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }
    void Jump()
    {
        if (!player2)
        {
            if (Input.GetButtonDown("Jump") && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0 || (wallJumpIndex > 0 && onWall)))
            {
                if (isGrounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                }
                else
                {
                    if (defaultAdditionalJumps != 0 && !onWall)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce + airJumpForce);
                        additionalJumps--;
                    }
                    if (wallJumpIndex > 0 && wallJump && onWall)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce + wallJumpForce);
                        wallJumpIndex--;
                    }
                }
                GameObject partJump = Instantiate(particle_jump, isGroundedChecker);
                partJump.transform.parent = null;
                Destroy(partJump, 1f);
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump_2") && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0 || (wallJumpIndex > 0 && onWall)))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                if (defaultAdditionalJumps != 0 && !onWall)
                {
                    additionalJumps--;
                }
                if (wallJumpCount != 0 && wallJump && onWall)
                {
                    wallJumpIndex--;
                }
                GameObject partJump = Instantiate(particle_jump, isGroundedChecker);
                partJump.transform.parent = null;
                Destroy(partJump, 1f);
            }
        }
    }

    void BetterJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void CheckWalls()
    {
        Collider2D colliderWallLeft = Physics2D.OverlapCircle(isWallLeftChecker.position, checkWallRadius, wallLayer);
        Collider2D colliderWallRight = Physics2D.OverlapCircle(isWallRightChecker.position, checkWallRadius, wallLayer);
        if (colliderWallLeft != null || colliderWallRight != null)
        {
            if (!onWall)
            {
                onWall = true;
            }
        }
        else
        {
            onWall = false;
        }
    }

    void CheckIfGrounded()
    {
        Collider2D colliderGround = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);
        if(colliderGround != null)
        {
            isGrounded = true;
            additionalJumps = defaultAdditionalJumps;
            wallJumpIndex = wallJumpCount;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<HitCtrl>())
        {
            HitCtrl hitCtrl = collision.GetComponent<HitCtrl>();
            if (hitCtrl.player2 != player2)
            {
                hited = true;
                SetPercentLife(true, hitCtrl.damage, false);
                Impact();
                Destroy(hitCtrl.gameObject);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Slime>())
        {
            rb.drag = 2;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Slime>())
        {
            rb.drag = 10;
        }
    }

    public void Impact()
    {
        Vector3 dirHit = Vector3.zero;
        if (!player2)
        {
            dirHit = (mainMngr.player1.transform.position - mainMngr.player2.transform.position).normalized;
        }
        else
        {
            dirHit = (mainMngr.player2.transform.position - mainMngr.player1.transform.position).normalized;
        }
        rb.AddForce(dirHit * (percentDmg / 5) , ForceMode2D.Impulse);
        Invoke("ResetHit", 0.2f);
    }

    void ResetHit()
    {
        hited = false;
    }

    void SetPercentLife(bool add, int qty, bool reset)
    {
        if (!reset)
        {
            int addInt = add ? 1 : 0;
            percentDmg += qty * addInt;
        }else
        {
            percentDmg = 0;
        }
        percentText.text = percentDmg.ToString();
    }
}
