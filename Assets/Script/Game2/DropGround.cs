using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropGround : MonoBehaviour
{
    [SerializeField] float dropY = -10f;
    [SerializeField] float shakeTime = 1f;
    [SerializeField] float dropTime = 3f;
    [SerializeField] float waitTime = 1f;
    

    Sequence dropSequence;
    CubeMovement cube;
    bool catchCube;
    float baseY;
    // Start is called before the first frame update
    void Start()
    {
        baseY = transform.position.y;
        catchCube = false;

        dropSequence = DOTween.Sequence();
        dropSequence.Pause();
        dropSequence
            .Append(transform.DOShakePosition(shakeTime, new Vector3(0, 0.2f, 0), 5 , 0))
            .AppendCallback(()=> DropCube())
            .Append(transform.DOMoveY(dropY, dropTime).SetEase(Ease.InExpo))
            .AppendInterval(waitTime)
            .Append(transform.DOMoveY(baseY, dropTime).SetEase(Ease.InExpo))
            .AppendCallback(()=> StopSequence())
            .SetLoops(-1);


    }

    



    void StopSequence()
    {
        dropSequence.Pause();
    }
    void PlaySequence()
    {
        dropSequence.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        cube = other.GetComponent<CubeMovement>();

        if (cube != null)
        {
            catchCube = true;
            PlaySequence();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CubeMovement exitCube = other.GetComponent<CubeMovement>();
        if (exitCube != null)
        {
            catchCube = false;
        }
    }

    void DropCube()
    {
        if (catchCube)
        {
            cube.CubeDrop();
        }
    }
}
