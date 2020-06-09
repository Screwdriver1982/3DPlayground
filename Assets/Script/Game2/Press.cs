using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class Press : MonoBehaviour
{
    [SerializeField] Button activateButton;
    [SerializeField] AudioClip dropSound;
    [Header("LiftAndDropConfig")]
    [SerializeField] float maxYValue = 3f;
    [SerializeField] float liftTime = 1f;
    [SerializeField] float hangingTime = 1f;
    [SerializeField] float dropTime = 0.2f;
    [Header("ShakeConfig")]
    [SerializeField] float shakeTime = 0.3f;
    [SerializeField] float shakeStrength = 0.05f;
    [SerializeField] int vibrationNumber = 5;
    
    

    Sequence liftAndLowering;
    public bool dropOrNot;
    float baseY;

    // Start is called before the first frame update
    void Start()
    {
        if (activateButton == null)
        {
            Debug.LogError("Button isn't set");
        }

        baseY = transform.position.y;
        //print(baseY);
        dropOrNot = false;
        activateButton.onButtonPressed += PlayLift;
        activateButton.onButtonExit += PlayLowering;

        liftAndLowering = DOTween.Sequence();
        liftAndLowering.Pause();
        liftAndLowering.Append(transform.DOMoveY(maxYValue, liftTime).SetEase(Ease.OutBounce))
            .AppendCallback(() => PauseOnTheTop())
            .AppendInterval(hangingTime)
            .Append(transform.DOShakePosition(shakeTime, Vector3.up*shakeStrength, vibrationNumber).SetEase(Ease.InExpo))
            .Append(transform.DOMoveY(baseY, dropTime).SetEase(Ease.OutBounce))
            .AppendCallback(()=> PauseOnTheBottom())
            .SetLoops(-1);

    }

    void PlayLift()
    {
        liftAndLowering.Play();
    }

    void PlayLowering()
    {
        dropOrNot = true;
        liftAndLowering.Play();
    }

    void PauseOnTheTop()
    {
        if (!dropOrNot)
        { 
            liftAndLowering.Pause();
        }
    }

    void PauseOnTheBottom()
    {
        AudioManager.Instance.PlaySound(dropSound);
        dropOrNot = false;
        liftAndLowering.Pause();
    }

    private void OnTriggerEnter(Collider other)
    {
        CubeMovement cube = other.GetComponent<CubeMovement>();
        cube.Die();
    }
}
