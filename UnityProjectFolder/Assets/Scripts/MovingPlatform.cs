using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

   
    public float speed;
    float moveDir = -1;


    bool hitLeft;
    bool hitRight;



    private void FixedUpdate()
    {
        if(moveDir == -1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }


        hitLeft = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, 2.5f);
        hitRight = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, 2.5f);

        if(hitLeft || hitRight)
        {
           
            Turn();
        }

        transform.position = new Vector3(transform.position.x + speed * moveDir * Time.deltaTime, transform.position.y, transform.position.z);

        if(Input.GetButtonDown("GravUp") || Input.GetButtonDown("GravDown") || Input.GetButtonDown("GravLeft") || Input.GetButtonDown("GravRight"))
        {

            this.transform.DetachChildren();
        }
           

    }


    void Turn()
    {
        moveDir *= -1;
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.collider.transform.SetParent(null);
    }
}
