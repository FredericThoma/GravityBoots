using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{



    public ParticleSystem footsteps;
    private ParticleSystem.EmissionModule dust;
    Rigidbody2D rb;
    public LayerMask groundLayer;
    
    public GameObject cameraa;
    public Vector2 gravDirection;
    public float gravForce;
    public float movementSpeed;
    public float jumpForce;
    public float jumpSmoothener;
    public float smallJumpFactor = 0.7f;

    public float hangTime;
    public float hangTImeGrav = -0.45f;
    public float hangCounter;

    public float jumpBufferTime = 0.12f;
    public float jumpBufferCount;

    

    public Vector3 targetRotation;
    
    public bool grounded;
  
    bool jumpReleased;

    private void Awake()
    {
        hangTImeGrav = -0.45f;
        jumpBufferTime = 0.12f;
        targetRotation = new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);
        rb = gameObject.GetComponent<Rigidbody2D>();
        footsteps = gameObject.GetComponentInChildren<ParticleSystem>();
        dust = footsteps.emission;
        cameraa = GameObject.FindGameObjectWithTag("MainCamera");

    }
    void Start()
    {
        
        rb.velocity = new Vector2(0, 0);
        gravDirection = Vector2.down;
        grounded = true;
        
    }

   
    
    void Update()
    {
        //dust.Play();
        if (Input.GetButtonDown("GravUp") && hangCounter > hangTImeGrav)
        {

            rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y * 0.5f);

            gravDirection = Vector2.up;

            hangCounter -= (Mathf.Abs(hangTImeGrav) + hangTime);

            SoundManager.PlaySound(SoundManager.Sound.GravChange);

            

        }
        if (Input.GetButtonDown("GravDown") && hangCounter > hangTImeGrav)
        {

            rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y * 0.5f);
            gravDirection = Vector2.down;
            SoundManager.PlaySound(SoundManager.Sound.GravChange);
            

        }
        if (Input.GetButtonDown("GravLeft") && hangCounter > hangTImeGrav)
        {

            rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y * 0.5f);
            gravDirection = Vector2.left;
            hangCounter -= (Mathf.Abs(hangTImeGrav) + hangTime);
           

            SoundManager.PlaySound(SoundManager.Sound.GravChange);
        }
        if (Input.GetButtonDown("GravRight") && hangCounter > hangTImeGrav)
        {

            rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y * 0.5f);
            gravDirection = Vector2.right;
            hangCounter -= (Mathf.Abs(hangTImeGrav) + hangTime);
            

            SoundManager.PlaySound(SoundManager.Sound.GravChange);
        }
        if(Input.GetButtonDown("Jump"))
        {
            
            jumpReleased = false;
            jumpBufferCount = jumpBufferTime;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }
        if (Input.GetButtonUp("Jump")){
            jumpReleased = true;
            
        }

        if (grounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
        
    }

    private void FixedUpdate()
    {

        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -transform.up, 2.05f, 1 << LayerMask.NameToLayer("Ground")))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        this.ApplyGravity(gravDirection);
        this.Move(gravDirection);
        this.Jump(gravDirection);


    }

   

    void ApplyGravity(Vector2 gravity)
    {
        rb.AddForce(gravity * gravForce);
     }

    void Move(Vector2 gravity)
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

       
            if (gravity.y == 0)  // Gravity left or right
            {

                if(gravity.x == 1)
                {
                    transform.position += new Vector3(0, vertical * gravity.x, 0) * Time.deltaTime * movementSpeed;
                }
                else
                {
                    transform.position += new Vector3(0, -1 * vertical * gravity.x, 0) * Time.deltaTime * movementSpeed;
                }
                
            }
            if (gravity.y != 0)  // Gravity up or down
            {
                transform.position += new Vector3(horizontal, 0, 0) * Time.deltaTime * movementSpeed;
            }
            if (horizontal != 0 && grounded)
            {
                dust.rateOverTime = 30;
            }
            else
            {
                dust.rateOverTime = 0;
            }

            bool movingVertically = Mathf.Abs(vertical) > 0.5f && gravity.x != 0;
            bool movingHorizontally = Mathf.Abs(horizontal) > 0.5f && gravity.y != 0;

            if (movingHorizontally || movingVertically)
            {

                if(hangCounter > 0)
            {
                SoundManager.PlaySound(SoundManager.Sound.PlayerMove);
            }
               
            }
           
        
    }

    void Jump(Vector2 gravity)
    {
        float forcevertical = -1 * (gravity.y) * jumpForce;
        float forcehorizontal = -1 * (gravity.x) * jumpForce;


        if (jumpBufferCount > 0 && hangCounter > 0)
        {
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(new Vector2(forcehorizontal, forcevertical), ForceMode2D.Impulse);
            hangCounter -= 0.2f;
            SoundManager.PlaySound(SoundManager.Sound.PlayerJump);
            
        }
        
         
        // reduce fall time

        if (gravity.y == 1)
        {
            if (rb.velocity.y > 0 && grounded == false)
            {
               

                rb.velocity += gravity * jumpSmoothener * Time.deltaTime;
            }
            else if(jumpReleased && rb.velocity.y < 0)
            {

                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * smallJumpFactor);
            }
        }
        if (gravity.y == -1)
        {
            if (rb.velocity.y < 0)
            {
                
                
                rb.velocity += gravity * jumpSmoothener * Time.deltaTime;
            }
            else if (jumpReleased && rb.velocity.y > 0)
            {
                
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * smallJumpFactor);
            }
        }
        if (gravity.x == 1)
        {
            if (rb.velocity.x > 0 && grounded == false)
            {
                
                rb.velocity += gravity * jumpSmoothener * Time.deltaTime;
            }
            else if (jumpReleased && rb.velocity.x < 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * smallJumpFactor, rb.velocity.y);
            }
        }
        if (gravity.x == -1)
        {
            if (rb.velocity.x < 0 && grounded == false)
            {
                
                rb.velocity += gravity * jumpSmoothener * Time.deltaTime;
            }
            else if (jumpReleased && rb.velocity.x > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x * smallJumpFactor, rb.velocity.y);
            }

        }
    }


    
  public Vector2 returnGravDir()
    {
        return this.gravDirection;
    }


    void playDust()
    {
       if(!grounded || Input.GetAxisRaw("Horizontal") == 0)
        {
           
        }
   }
    

}
