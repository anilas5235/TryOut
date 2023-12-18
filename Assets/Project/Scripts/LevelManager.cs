using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool[] unlockedLevels;
    
    void Awake()
    {
       DontDestroyOnLoad(this);
    }

    public void ChangeLevelLockState(bool change, int level)
    {
        unlockedLevels[level] = change;
    }
}
