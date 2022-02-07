using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    ManageGame gameManager;
    Vector3 positionOfCheckPoint;
    Transform playerTransform;
    Animator checkpointAnimator;
    public Laser[] lasers;

    public string gravDir = "down";

    float distX;
    float distY;


    public bool reached;
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManageGame>();
        positionOfCheckPoint = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        checkpointAnimator = gameObject.GetComponent<Animator>();
        reached = false;
        
    }

    private void Update()
    {
        distX = Mathf.Abs(transform.position.x - playerTransform.position.x);
        distY = Mathf.Abs(transform.position.y - playerTransform.position.y);

       if(distX < 2 && distY < 4)
        {
            
            if (!reached)
            {
                gameManager.UpdateSpawnPoint(positionOfCheckPoint, gravDir);
                SoundManager.PlaySound(SoundManager.Sound.CheckPoint);
                checkpointAnimator.SetBool("Reached", true);
                reached = true;
                StartCoroutine(playIdle());

                foreach(Laser laser in lasers)
                {
                    laser.activated = false;
                }
            }


        }



    }


    public IEnumerator playIdle()
    {
        yield return new WaitForSeconds(1.5f);
        //checkpointAnimator.SetBool("Reached", false);
        checkpointAnimator.SetBool("Idle", true);
    }
}
