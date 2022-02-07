using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelList
{


    


    private static bool[] levelStatuses = new bool[8];
    private static int[,] levelStarArray = new int[7, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

    public static void updateLevelStatus(int index, bool cleared)
    {
        LevelList.levelStatuses[index] = cleared;
    }

    public static bool returnLevelStatus(int index)
    {
        return LevelList.levelStatuses[index];
    }

    public static bool getStarsOfLevel(int levelID, int starID)
    {
      if (levelStarArray[levelID, starID] == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public static void updateStarsOfLevel(int levelID, int starID, bool collected)
    {
        if (collected)
        {
            levelStarArray[levelID, starID] = 1;
        }
        else
        {
            levelStarArray[levelID, starID] = 0;
        }
        
    }

    

}
