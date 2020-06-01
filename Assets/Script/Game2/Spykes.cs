using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spykes : MonoBehaviour
{
    [SerializeField] float moveTime =1f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] float maxYValue = 0.2f;
    [SerializeField] bool sequencePlayer = false;

    Sequence movementSequence2;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Movement());

        /*
        Sequence movementSequence = DOTween.Sequence();
        
        movementSequence.AppendInterval(waitTime);
        movementSequence.Append(transform.DOMoveY(maxYValue, moveTime));
        
        // вызов с параметрами
        movementSequence.AppendCallback(() => PrintUp("UP"));
        movementSequence.AppendInterval(waitTime);
        movementSequence.Append(transform.DOMoveY(0, moveTime));

        movementSequence.AppendCallback(() => 
            { 
               // Debug.Log("I'm down!");
               // Debug.Log("Finished sequence!");
            }
        );

        movementSequence.SetLoops(-1);

        */

        movementSequence2 = DOTween.Sequence();
        movementSequence2.AppendInterval(waitTime)
            .Append(transform.DOMoveY(maxYValue, moveTime).SetEase(Ease.InExpo))
            .AppendInterval(waitTime/2)
            .SetLoops(-1, LoopType.Yoyo);
        


    }

    

    
    void PrintUp(string position)
    {
       // Debug.Log("I'm " + position + "!");
    }

    IEnumerator Movement()
    {
        while (true)
        { 
            
            transform.DOMoveY(maxYValue, moveTime);

            yield return new WaitForSeconds(moveTime + waitTime);

            transform.DOMoveY(0, moveTime);
            
            yield return new WaitForSeconds(moveTime + waitTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CubeMovement cube = other.GetComponent<CubeMovement>();
        cube.Die();
    }

}
