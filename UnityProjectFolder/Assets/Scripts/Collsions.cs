
using UnityEngine;

public class Collsions : MonoBehaviour
{
    public PlayerMovement movement;
    public GameObject gameManager;


    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }
    private void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        
        if (collisionInfo.collider.tag == "Spike")
        {
            gameManager.GetComponent<ManageGame>().EndGame();
            
        }

        if(collisionInfo.collider.tag == "MovingPlatform")
        {
            this.transform.parent = collisionInfo.transform;
        }
    }

    public void OnCollisionExit2D(Collision2D collisionInfo)
    {
        if(collisionInfo.collider.tag == "MovingPlatform")
        {
            this.transform.parent = null;
        }
    }
}
