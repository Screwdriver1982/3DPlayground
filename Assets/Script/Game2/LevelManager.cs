using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager Instance { get; private set; }

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

    public Action endLevel = delegate { };
    
    [SerializeField] int coinsToEnd =0;
    [SerializeField] int collectedCoins =0;


    public void NewCoin()
    {
        coinsToEnd += 1;
    }

    public void AddCoin()
    {
        collectedCoins += 1;
        if (collectedCoins >= coinsToEnd)
        {
            print("End level action called");
            endLevel();
            coinsToEnd = 0;
            collectedCoins = 0;
            endLevel = null;
        }
    }




}
