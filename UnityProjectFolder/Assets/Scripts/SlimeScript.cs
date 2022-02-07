using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public float moveSpeed;
    public float moveDir = 1; // 1 = right, -1 = left 
    public float range;
    public Transform wallCheck;
    Animator slimeAnimator;
    Rigidbody2D slimeRB;
    SpriteRenderer slimeSprite;
    ManageGame gameManager;

    private float baseMoveSpeed = 8;
    GameObject player;
    public float jumpForce;

    public float airFactor = 1;

    public float jumpAttackCooldown = 0f;

    bool grounded;

    bool canAttack = false;

    public Vector2 gravDir = new Vector2(0, -1);
    public float gravForce = 10;
    public Transform slimeTransform;

    bool attacking = false;

   


    public Transform playerTransform;
    private void Awake()
    {
        slimeAnimator = gameObject.GetComponent<Animator>();
        slimeTransform = gameObject.GetComponent<Transform>();
        slimeRB = gameObject.GetComponent<Rigidbody2D>();
        range = 15;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        slimeSprite = gameObject.GetComponent<SpriteRenderer>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<ManageGame>();
    }


    private void FixedUpdate()
    {

        float random = Random.Range(0.7f, 1f);
        moveSpeed = baseMoveSpeed * random;

      //  grounded = Physics2D.Raycast(transform.position, -transform.up, 2, 1 << LayerMask.NameToLayer("Ground"));
        applyGrav(gravDir);
        setRotation(gravDir);
        if(checkWalls(wallCheck.position, moveDir))
        {
            moveDir *= -1;
            
        }

        if(Mathf.Abs(slimeTransform.position.x - playerTransform.position.x) < 15 && gameManager.playerAlive == true)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
        
        if (!canAttack)
        {
            Patrol();
        }
        if (canAttack)
        {
            Attack();
        }

        if (Mathf.Abs(playerTransform.position.x - slimeTransform.position.x) < 5 && Mathf.Abs(playerTransform.position.y - slimeTransform.position.y) > 15)
        {
            jumpAttack();
        }


        if (!isGrounded())
        {
            airFactor = 0.25f;
        }
        else
        {
            airFactor = 1f;
        }

    }



    void setRotation(Vector2 gravDir)
    {
        if (gravDir.y == 1)
        {
            slimeTransform.localRotation = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            slimeTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
    void applyGrav(Vector2 gravDir)
    {
        slimeRB.AddForce(gravDir * gravForce);

    }

    void Attack()
    {

        float distXToPlayer = slimeTransform.position.x - playerTransform.position.x;
        if(gravDir.y == -1)
        {
            if (distXToPlayer > 0)
            {
                slimeSprite.flipX = false;
            }
            else
            {
                slimeSprite.flipX = true;
            }
        }
        if(gravDir.y == 1)
        {
            if (distXToPlayer > 0)
            {
                slimeSprite.flipX = true;
            }
            else
            {
                slimeSprite.flipX = false;
            }
        }
        

        Vector3 target = new Vector3(playerTransform.position.x, slimeTransform.position.y, slimeTransform.position.z);
            slimeTransform.position = Vector3.MoveTowards(slimeTransform.position, target, moveSpeed * airFactor * Time.deltaTime);
       

    }

    public IEnumerator resetAttackTimer()
    {
        yield return new WaitForSeconds(0.5f);
        gravDir = new Vector2(0, gravDir.y * -1);
        yield return new WaitForSeconds(2f);
        attacking = false;
    }

    void jumpAttack() {

        Debug.Log("callled");
        if (!attacking)
        {

            slimeAnimator.SetTrigger("JumpAttack");
            attacking = true;
            slimeRB.AddForce(new Vector2(0, jumpForce * gravDir.y * -1), ForceMode2D.Impulse);
            StartCoroutine(resetAttackTimer());

        }
       
        
       
    }


    bool isGrounded()
    {
        if (Physics2D.Raycast(new Vector2(slimeTransform.position.x, slimeTransform.position.y), Vector2.down, 1f, 1 << LayerMask.NameToLayer("Ground")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void Patrol()
    {
        Vector3 target = new Vector3(slimeTransform.position.x + (100 * moveDir) , slimeTransform.position.y, slimeTransform.position.z);
        slimeTransform.position = Vector3.MoveTowards(slimeTransform.position, target, moveSpeed * Time.deltaTime);

        if (moveDir == 1)
        {
            if(gravDir.y == -1) {
                slimeSprite.flipX = true;
            }
            else
            {
                slimeSprite.flipX = false;
            }
            
        }
        else
        {
            if (gravDir.y == -1)
            {
                slimeSprite.flipX = false;
            }
            else
            {
                slimeSprite.flipX = true;
            }
        }
    }
bool checkWalls(Vector3 wallcheck, float moveDir)
    {
        Debug.Log(Physics2D.Raycast(wallcheck, Vector2.right, 2f) || Physics2D.Raycast(wallcheck, Vector2.left, 2f));
       if(Physics2D.Raycast(wallcheck, Vector2.right, 10f, 1 << LayerMask.NameToLayer("Ground")) && moveDir == 1)
        {
            return true;
       }


        if (Physics2D.Raycast(wallcheck, Vector2.left, 3f, 1 << LayerMask.NameToLayer("Ground")) && moveDir == -1)
        {
            return true;
        }

        if (Physics2D.Raycast(wallcheck, Vector2.right, 5f, 1 << LayerMask.NameToLayer("Player")) || Physics2D.Raycast(wallcheck, Vector2.left, 5f, 1 << LayerMask.NameToLayer("Player")))
        {
            if (gameManager.playerAlive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
      
        return false;
    }






    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            gameManager.EndGame();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        slimeAnimator.SetBool("canJumpAttack", false);
    }
}
