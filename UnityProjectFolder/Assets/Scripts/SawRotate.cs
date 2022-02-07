using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawRotate : MonoBehaviour
{

    public float rotateSpeed;
    public float speed;
    public Transform pos1;
    public Transform pos2;
    bool turn = true;
   
  
    void FixedUpdate()
    {
        //transform.Rotate(0, 0, rotateSpeed);
        if(pos1.position.x != transform.position.x) { 
        if(transform.position.x >= pos2.position.x)
        {
            turn = false;
        }
        if (transform.position.x <= pos1.position.x)
        {
            turn = true;
        }
        }
        else
        {
            
            if (transform.position.y >= pos2.position.y)
            {
                turn = false;
            }
            if (transform.position.y <= pos1.position.y)
            {
                turn = true;
            }
        }
        if (turn)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos2.position,speed*Time.deltaTime);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pos1.position,  speed * Time.deltaTime);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
