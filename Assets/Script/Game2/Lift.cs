using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using DG;
using DG.Tweening;

public class Lift : MonoBehaviour
{

    [SerializeField] UnityEngine.Vector3 finalDestination;
    [SerializeField] float moveTime;
    [SerializeField] Button button;

    public Collider[] bridge;

    void Start()
    {
        bridge = GetComponentsInChildren<Collider>();
        button.onButtonPressed += MoveToDestination;


    }

    // Update is called once per frame
    void MoveToDestination()
    {
        transform.DOMove(finalDestination, moveTime).SetEase(Ease.InSine).OnComplete(ActivateBridge);
    }

    void ActivateBridge()
    {
        for (int i = 0; i< bridge.Length; i++)
        { 
            bridge[i].enabled = true;
        }
    }
}
