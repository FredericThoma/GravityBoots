using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    public GameObject[] popUps;
    private int popUpIndex;
    public int count=0;
    public GameObject player;
    ManageGame gameManager;
    public Laser laser;
    bool basicsDone = false;
    public CheckPointScript cp1;
    public CheckPointScript cp2;
    bool coinQuestaccepted = false;
    bool cornerQuestaccepted = false;
    bool jumped = false;
    
    void Start()
    {


       
        popUps[0].SetActive(true);
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManageGame>();
        player.GetComponent<PlayerMovement>().enabled = false;
        

        
    }

    private void Awake()
    {
        
    }
    void Update()
    {
        
            for (int i = 0; i < popUps.Length; i++)
            {
                if (popUpIndex != i)
                {
                    popUps[i].SetActive(false);
                }
                else
                {
                    popUps[i].SetActive(true);
                }



            }
        if (!basicsDone)
        {
            

        }

        
        if(Input.GetButtonDown("Jump") && popUpIndex == 3)
        {
            popUpIndex = -1;
            jumped = true;
        }

        if (cp1.reached && !coinQuestaccepted)
        {
            coinQuestaccepted = true;
            player.GetComponent<PlayerMovement>().enabled = false;

            popUpIndex = 4;

        }

        if(laser.activated == false && ! cp2.reached)
        {
            popUpIndex = -1;
        }


        if (cp2.reached && !cornerQuestaccepted)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            cornerQuestaccepted = true;
            popUpIndex = 5;
            

        }

        if (LevelList.getStarsOfLevel(0, 0))
        {
            
            laser.activated = false;
        }


        if(GameObject.FindGameObjectWithTag("DeathMenu"))
        {
            popUpIndex = -1;
        }

    }

    public void coinQuest()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        popUpIndex = -1;

    }

    public void cornerWalkPopUp()
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        popUpIndex = -1;
    }

    public IEnumerator emptyPopup(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        popUpIndex = -1;

    }
    public IEnumerator nextPopUp(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        popUpIndex++;
    }

    public IEnumerator setPopUpIndex(int index, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        popUpIndex = index;
    }

    public void nextButton()
    {
        popUpIndex++;
    }
    public void resume()
    {
        basicsDone = true;

        popUpIndex = -1;
        if (!jumped)
        {
            StartCoroutine(setPopUpIndex(3, 1));
        }
       
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
