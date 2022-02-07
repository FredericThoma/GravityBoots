using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    PlayerMovement playerMovementScript;
    Rigidbody2D playerRB;
    Animator playerAnimator;
    SpriteRenderer playerSpriteRenderer;
    Vector2 gravDir;
    float speedVertically;
    float speedHorizontally;
    bool grounded;
    void Start()
    {
        playerMovementScript = this.GetComponent<PlayerMovement>();
        playerAnimator = this.GetComponent<Animator>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerSpriteRenderer = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        speedVertically = Mathf.Abs(Input.GetAxis("Vertical"));
        speedHorizontally = Mathf.Abs(Input.GetAxis("Horizontal"));
        if(gravDir.y != 0)
        {
            playerAnimator.SetFloat("Speed", Mathf.Abs(speedHorizontally));
            flipPlayerSprite(gravDir);
        }
        if(gravDir.y == 0)
        {
            playerAnimator.SetFloat("Speed", Mathf.Abs(speedVertically));
            flipPlayerSprite(gravDir);
        }
        
        
    }
    
    private void FixedUpdate()
    {
        grounded = playerMovementScript.grounded;
        gravDir = playerMovementScript.gravDirection;
        if (gravDir.y == 1)
        {
           
            setJumpAnimationState(-1 * playerRB.velocity.y);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 180), 12.5f);
            

        }
        if (gravDir.y == -1)
        {
            
            setJumpAnimationState(playerRB.velocity.y);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), 12.5f);
        }
        if (gravDir.x == 1)
        {
            
            setJumpAnimationState(-1 * playerRB.velocity.x);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 90), 12.5f);

        }
        if (gravDir.x == -1)
        {
            setJumpAnimationState(playerRB.velocity.x);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -90), 12.5f);
        }

    }

   


    public void setJumpAnimationState(float axisVel)
    {
        
        if (axisVel > 0.1f)
        {
        playerAnimator.SetBool("Fall", false);
            playerAnimator.SetBool("Jump", true);

        }
        else if (axisVel < -0.1f)
        {
            playerAnimator.SetBool("Fall", true);
            playerAnimator.SetBool("Jump", false);
        }
        else
        {
            playerAnimator.SetBool("Fall", false);
            playerAnimator.SetBool("Jump", false);
        }
        

    }


    void flipPlayerSprite(Vector2 gravDir)
    {
        if(gravDir.y != 0)
        {
            float speedHorizontal = Input.GetAxis("Horizontal");

            if (speedHorizontal < 0)
            {


                playerSpriteRenderer.flipX = gravDir.y == -1;


            }
            if (speedHorizontal > 0)
            {
                playerSpriteRenderer.flipX = gravDir.y == 1;
            }
        }

        else
        {

            float speedVertical = Input.GetAxis("Vertical");


            if (speedVertical > 0)
            {
                playerSpriteRenderer.flipX = gravDir.x == -1;
            }
            if (speedVertical < 0)
            {
                playerSpriteRenderer.flipX = gravDir.x == 1;
            }
        }
        
    }
    


}
