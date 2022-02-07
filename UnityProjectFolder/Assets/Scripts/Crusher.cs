using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    bool drop = false;
    public float dropSpeed;
    public float riseSpeed;
    Animator crusherAnimator;
    public ManageGame gamemanager;
    float crushAnimationCounter = 0;
    public bool altTopPoint = false;
    int fallDir = 1;
    public bool inverted = false;
    
    void Start()
    {
        crusherAnimator =  this.GetComponent<Animator>();
        gamemanager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManageGame>();
        if (inverted)
        {
            fallDir = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        float distToPlayer = Mathf.Abs(Vector3.Distance(transform.position, playerPosition));

        if (!inverted)
        {
            if (transform.position.y >= top.position.y)
            {
                drop = true;
            }
        }
        else
        {
            if (transform.position.y <= top.position.y)
            {
                drop = true;
            }
        }
        
        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -transform.up, 2f) && drop ==true )
        {
            drop = false;
            if(distToPlayer < 30)
            {
                SoundManager.PlaySound(SoundManager.Sound.CrusherFall);
            }
            
        }
        if (drop)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - (100*fallDir)), dropSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y + (100*fallDir)), riseSpeed * Time.deltaTime);
        }
    }


    private void FixedUpdate()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -transform.up, 2, 1 << LayerMask.NameToLayer("Ground")))
        {
            
            crusherAnimator.SetBool("hitGround", true);       
        }
        else
        {
            crushAnimationCounter -= Time.deltaTime;
            if(crushAnimationCounter < -1)
            {
                crusherAnimator.SetBool("hitGround", false);
                crushAnimationCounter = 0;
            }
            
        }

        if(altTopPoint && Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), transform.up, 2, 1 << LayerMask.NameToLayer("Ground"))){
            drop = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            gamemanager.EndGame();
        }

        
    }
}
