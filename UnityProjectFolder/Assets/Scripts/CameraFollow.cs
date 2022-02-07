using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
   // public Transform gravityUI;
    public Transform playerTransform;
    public Vector3 offset;
    public float camerPositionSmoothFactor = 0.125f;
    public float offsetSmoothFactor = 0.5f;
    public Vector3 smoothedOffset;
    public Vector3 desiredOffset = new Vector3(25,12, -1);
    public Vector3 diffOffsetDesOffset = new Vector3(0, 0, -1);
    public PlayerMovement playerMovementScript;
    public Rigidbody2D playerRB;
    public Sprite[] arrowSprites = new Sprite[4];
    public Image arrow;
    public Vector2 prevGrav = new Vector2(0, 1);


   

    public Vector2 gravity;
   

    private void Awake()
    {
        
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        
        transform.position = new Vector3(playerTransform.position.x + desiredOffset.x, playerTransform.position.y + desiredOffset.y, transform.position.z);

    }
    public void FixedUpdate()
    {
        updateGrav();
        Vector3 dest = new Vector3(playerTransform.position.x + desiredOffset.x, playerTransform.position.y + desiredOffset.y, transform.position.z);
       
        if (Mathf.Abs(transform.position.y - dest.y) < 0.01f)
        {
            prevGrav = gravity;

        }

        float tempZ = transform.position.z;

        float distance = Mathf.Abs(Vector3.Distance(transform.position, dest));
        Vector3 smoothed = Vector3.Lerp(transform.position, dest, 1/distance);
        smoothed.z = tempZ;
       
       transform.position = smoothed;
        


        if (Input.GetButtonDown("GravLeft") || Input.GetButtonDown("GravRight") || Input.GetButtonDown("GravUp") || Input.GetButtonDown("GravDown"))
        {
            prevGrav = gravity;
           
        }

      
    }

    

    public void updateGrav()
    {

        
        gravity = playerMovementScript.returnGravDir();

        if (gravity.y == 1)
        {
           
            arrow.sprite = arrowSprites[0];
            desiredOffset = new Vector3(25, 8, -1);
           
        }
        if (gravity.y == -1)
        {
            
            arrow.sprite = arrowSprites[1];
            desiredOffset = new Vector3(25,8, -1);
           
        }
        if (gravity.x == -1)
        {
            
            arrow.sprite = arrowSprites[2];
            desiredOffset = new Vector3(0, 0, -1);
           
        }
        if (gravity.x == 1)
        {
            
            arrow.sprite = arrowSprites[3];
            desiredOffset = new Vector3(0, 0, -1);
          
        }


        
       
            

        
    }

    

    public void updateOnGravChange(Vector2 gravDir)
    {
        if(gravity.x != 0)
        {
            float lerpX = Mathf.Lerp(transform.position.x, playerTransform.position.x + desiredOffset.x, 0.1f);
            transform.position = new Vector3(lerpX, transform.position.y, transform.position.z);
        }

        if(gravity.y != 0)
        {
            float lerpY = Mathf.Lerp(transform.position.y, playerTransform.position.y + desiredOffset.y, 0.1f);
            transform.position = new Vector3(transform.position.x, lerpY, transform.position.z);
        }
    }


}
