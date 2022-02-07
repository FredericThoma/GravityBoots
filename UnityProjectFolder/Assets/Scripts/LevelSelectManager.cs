using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class LevelSelectManager : MonoBehaviour
{
   

   public LevelStatus[] LevelArray;


    [System.Serializable]
    public class LevelStatus
    {
       public bool cleared;
       
       public  int id;

        public Image[] starsOfLevel = new Image[3];

        public GameObject checkMark;

     

    }


    public void Awake()
    {
        foreach(LevelStatus level in LevelArray)
        {
           // Debug.Log(level.id);
           // Debug.Log(LevelList.returnLevelStatus(level.id));
            level.cleared = LevelList.returnLevelStatus(level.id);


            if (level.cleared)
            {
                level.checkMark.SetActive(true);
            }
            else
            {
                level.checkMark.SetActive(false);
            }

            for(int i = 0; i<3; i++)
            {
                bool foundStar = LevelList.getStarsOfLevel(level.id, i);
                Image thisStar = level.starsOfLevel[i];
                if (foundStar)
                {
                    
                    thisStar.color = new Color(255, 255, 0, 255);

                    
                }
                else
                {
                    thisStar.color = new Color(0, 0, 0, 100);
                }
            }
        }


    }



    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
