using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public DinoMainManager dMM;
    private Rigidbody2D rb;
    private BoxCollider2D bC;
    public GameObject playerSprite;
    private Animator anim;
    public GameObject jumpParticles;
    public GameObject laytonRig, soraRig;

    public Transform groundCheck;
    public bool isGrounded;
    
    private float initialGravityScale;

    public bool isSliding = false;

    private float upwardsVelocity = 0.0f;
    public float upwardsJumpForce = 0.0f;
    public float gravityMultiplier = 100.0f;
    public float maxJumpHeight = 2.0f;
    private float iniY = 0.0f;

    public ParticleSystem partJump, partArrastrarse, partCatchMahou;
    public Transform mahouCatchSitio;

    public AudioSource jumpClip;
    public AudioSource hitClip;

    private bool isOnMobile;

    float _beersToUlti;
    private float currentBeers = 0;
    public Slider beerSlider;
    public float invincibilityTime;
    private bool isInvicible;
    public GameObject botellin;
    private bool canReduceSlider;
    public Animator ultTransitor;

    public GameObject lastCan;
    DinoMainManager dinoMainMngr;
    private void Awake()
    {
        isOnMobile = Application.platform == RuntimePlatform.Android;
        int rnd = Random.Range(1, 3);
        if(rnd == 1)
        {
            laytonRig.SetActive(true);
            anim = laytonRig.GetComponent<Animator>();
        }
        else
        {
            soraRig.SetActive(true);
            anim = soraRig.GetComponent<Animator>();
        }
        dinoMainMngr = FindObjectOfType<DinoMainManager>();
        _beersToUlti = dinoMainMngr.beersToUlti;
        beerSlider.maxValue = _beersToUlti;
    }
    // Start is called before the first frame update
    void Start()
    {
        partArrastrarse.Stop();
        
        iniY = transform.position.y;
        bC = this.GetComponent<BoxCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        initialGravityScale = rb.gravityScale;

        Physics.gravity = Vector3.down * gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnMobile)
        {
            Jump();
            Crouch();
        }
        else
        {
            MobileJump();
            MobileCrouch();
        }

        //WebJump();
        //WebCrouch();
        anim.SetBool("IsSliding", isSliding);

        if (canReduceSlider)
        {
           
        }

        if (!isGrounded) partArrastrarse.Stop();


    }

    private void Jump()
    {
        float dt = Time.deltaTime;

        upwardsVelocity += dt * Physics.gravity.y;
        
        if (Input.GetKey(KeyCode.UpArrow) && (transform.position.y <= iniY + maxJumpHeight && upwardsVelocity > 0 || isGrounded))
        {
            partJump.Play();

            if(isGrounded && Time.timeScale == 1) jumpClip.Play();
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
                //partArrastrarse.Play();
              
                isSliding = true;
            }
            else
            {
                isSliding = false;
                bC.size = new Vector2(0.5f, 2);
                bC.offset = new Vector2(0, 0);
                //playerSprite.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                isSliding = false;
                partArrastrarse.Play();


            }
        }
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Obstacle")
        {
            hitClip.Play();
            Time.timeScale = 0;
            dMM.GameOver();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "InivincibilityPU")
        {
            ParticleSystem A = Instantiate(partCatchMahou, mahouCatchSitio.position, Quaternion.identity);
            A.gameObject.transform.SetParent(this.transform);
            partCatchMahou.Play();
            ++currentBeers;
            collision.GetComponent<PUController>().StartCollision(beerSlider,currentBeers,lastCan);
            lastCan = collision.gameObject;
            if (currentBeers >= _beersToUlti)
            {
                StartCoroutine(Invincibility());
            }
            lastCan = collision.gameObject;
        }
    }

    
    private IEnumerator Invincibility()
    {
        Time.timeScale = 0.5f;
        ultTransitor.SetBool("Transition", true);
        isInvicible = true;
        anim.SetBool("IsRiding", true);
        botellin.SetActive(true);
        currentBeers = 0;
        ultTransitor.SetBool("Transition", false);
        Time.timeScale = 2.5f;
        bC.enabled = false;
        yield return new WaitForSeconds(invincibilityTime);
        beerSlider.value = Mathf.Lerp(beerSlider.value, currentBeers, invincibilityTime);
        currentBeers = 0;
        lastCan = null;
        dinoMainMngr.fillCans = 0;
        Time.timeScale = 1f;
        isInvicible = false;
        botellin.SetActive(false);
        anim.SetBool("IsRiding", false);
        yield return new WaitForSeconds(1f);
        bC.enabled = true;
    }




    private void MobileJump()
    {
        float dt = Time.deltaTime;

        upwardsVelocity += dt * Physics.gravity.y;

        if (Input.GetMouseButton(0) && Input.mousePosition.y > Screen.height * 0.3f && (transform.position.y <= iniY + maxJumpHeight && upwardsVelocity > 0 || isGrounded))
        {
            partJump.Play();
        
            if (isGrounded && Time.timeScale == 1) jumpClip.Play();
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

    private void MobileCrouch()
    {
        if (isGrounded)
        {
            if (Input.GetMouseButton(0) && Input.mousePosition.y < Screen.height * 0.3f)
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
                //partArrastrarse.Stop();
                partArrastrarse.Play();


            }
        }

    }
  

}
