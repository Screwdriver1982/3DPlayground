using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropGround : MonoBehaviour
{
    [Header("DropConfig")]
    [SerializeField] float dropY = -10f;
    [SerializeField] float dropTime = 3f;
    
    [Header("ShakeConfig")]
    [SerializeField] float shakeTime = 1f;
    [SerializeField] float waitTime = 1f;
    

    Sequence dropSequence;
    CubeMovement cube;
    [SerializeField] bool catchCube = false;
    float baseY;
    Collider colliderOfground;
    // Start is called before the first frame update
    void Start()
    {
        baseY = transform.position.y;
        catchCube = false;
        colliderOfground = GetComponent<Collider>();

        dropSequence = DOTween.Sequence();
        dropSequence.Pause();
        dropSequence
            .Append(transform.DOShakePosition(shakeTime, new Vector3(0, 0.2f, 0), 2 , 0))
            .AppendCallback(()=> DropCube())
            .Append(transform.DOMoveY(dropY, dropTime).SetEase(Ease.InExpo))
            .AppendInterval(waitTime)
            .Append(transform.DOMoveY(baseY, dropTime))
            .AppendCallback(()=> StopSequence())
            .SetLoops(-1);


    }

    



    void StopSequence()
    {
        colliderOfground.enabled = true;
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
            print("catch");
            PlaySequence();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        CubeMovement exitCube = other.GetComponent<CubeMovement>();
        if (exitCube != null)
        {
            catchCube = false;
            colliderOfground.enabled = false;
            print("uncatch");
        }
    }

    

    void DropCube()
    {
        print("dropCube method " + catchCube);
        colliderOfground.enabled = false;
        if (catchCube)
        {
            print("try to drop");
            cube.CubeDrop();
        }
    }
}
