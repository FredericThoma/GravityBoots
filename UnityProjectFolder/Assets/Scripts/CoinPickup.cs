using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinPickup : MonoBehaviour
{
    public int coinID;
    public bool reached = false;
    Transform playerTransform;
    public CheckPointScript nextCheckPoint;


    // Start is called before the first frame update
    public void Awake()
    {

        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        this.GetComponent<SpriteRenderer>().enabled = true;
       
    }

    // Update is called once per frame
    private void Update()
    {
        float distX = Mathf.Abs(transform.position.x - playerTransform.position.x);
        float distY = Mathf.Abs(transform.position.y - playerTransform.position.y);
        if (distX < 2.25f && distY < 2.25f && !reached)
        {
           
            SoundManager.PlaySound(SoundManager.Sound.CoinPickUp);
            int levelID = SceneManager.GetActiveScene().buildIndex - 2;
            LevelList.updateStarsOfLevel(levelID, coinID, true);
            reached = true;
        }

        if (reached)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    
}
