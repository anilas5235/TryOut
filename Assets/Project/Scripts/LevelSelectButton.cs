using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    private static LevelManager levelManager;
    private int levelNumber;
    private bool clickable;
    public Scene myLevel;
    private Button button;
    
    void Awake()
    {
        if (levelManager == null) levelManager = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();

        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "LevelSelect") return;

        clickable = levelManager.unlockedLevels[levelNumber];

        button.interactable = clickable;
    }

    public void LoadLevel()
    {
        if(clickable) SceneManager.LoadScene(myLevel.name);
    }
}
