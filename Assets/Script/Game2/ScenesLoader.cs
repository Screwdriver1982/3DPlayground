using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    #region Singleton
    public static ScenesLoader Instance { get; private set; }
    public Action LoadLevelAction = delegate { };

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        { 
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }

    }

    #endregion

    public Action levelLoadAction;



    public void LoadLevel(float delay, bool restartOrNot)
    {
        levelLoadAction();        
        StartCoroutine(LoadLevelWithCoroutine(delay, restartOrNot));

    }

    IEnumerator LoadLevelWithCoroutine(float delay, bool restartOrNot)
    {

        yield return new WaitForSeconds(delay);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (!restartOrNot)
        {
            currentScene += 1;
        }
        SceneManager.LoadScene(currentScene);

    }



}
