using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarUIScript : MonoBehaviour
{
    public Image[] Stars = new Image[3];
    int levelID;

    public void Awake()
    {
        levelID = SceneManager.GetActiveScene().buildIndex - 2;
    }

    public void Update()
    {

        

        for (int i=0; i<3; i++)
        {
          //  Debug.Log(LevelList.getStarsOfLevel(levelID, i));
            
                UpdateStarUI(i, LevelList.getStarsOfLevel(levelID, i));
            
        }
    }


    public void UpdateStarUI(int id, bool collected)
    {
        if (collected)
        {
            Stars[id].color = new Color(255, 255, 0, 255);
        }
        else
        {
            Stars[id].color = new Color(0, 0, 0, 255);
        }
    }
}
