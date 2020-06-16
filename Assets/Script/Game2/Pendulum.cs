using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pendulum : MonoBehaviour
{
    [SerializeField] float rotateTime = 1f;
    [SerializeField] float maxPointTime = 3f;
    [SerializeField] float startLag;
    [SerializeField] Vector3 finalAngels;
    [SerializeField] AnimationCurve pendulumEase;

    Sequence pendulumSequence;
    [SerializeField] Vector3 startAngels;

    private void Start()
    {
        pendulumSequence = DOTween.Sequence();
        StartCoroutine(PendulumLagCoroutine(startLag));
        pendulumSequence.Pause();
        pendulumSequence.AppendInterval(maxPointTime)
                        //.Append(transform.DORotate(Vector3.zero, rotateTime).SetEase(Ease.InSine))
                        .Append(transform.DORotate(finalAngels, rotateTime).SetEase(Ease.InOutSine))
                        //.Append(transform.DORotate(Vector3.zero, rotateTime).SetEase(Ease.InSine))
                        .Append(transform.DORotate(startAngels, rotateTime).SetEase(Ease.InOutSine))
                        .SetLoops(-1, LoopType.Restart);

    }


    IEnumerator PendulumLagCoroutine(float startLag)
    {
        yield return new WaitForSeconds(startLag);
        pendulumSequence.Play();
    
    }

}
