using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    #region Singleton
    public static ScenesLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        { 
            Instance = this;
        }

    }

    #endregion

    public void RestartLevel(float delay)
    {
        StartCoroutine(RestartLevelWithCoroutine(delay));
    }

    IEnumerator RestartLevelWithCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

    }

}
