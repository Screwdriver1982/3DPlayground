using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.WSA.Input;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] float moveTime = 0.5f;
    [SerializeField] float jumpPower = 0.6f;
    [SerializeField] float reloadLevelDelay = 1f;
    [SerializeField] float dropTime = 1f;
    [SerializeField] GameObject deathEffect;

    Rigidbody rb;
    bool allowInput;
    bool isMoving;
    
    #region DieAndDrop

    public void Die()
    {
        GameObject newObject = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        //spawn particle
        //play sound
        //reload scene
        ScenesLoader.Instance.RestartLevel(reloadLevelDelay);
    }
    public void CubeDrop()
    {
        if (!isMoving)
        {
            allowInput = false;
            rb.isKinematic = false;
            StartCoroutine(CubeDieCoroutine(dropTime));
            
        }
        
    }

    IEnumerator CubeDieCoroutine(float dieTime)
    {
        yield return new WaitForSeconds(dieTime);
        Die();
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        allowInput = true;
        isMoving = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!allowInput)
        {
            //exit
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //forward = (0,0,1)
            Vector3 newPosition = transform.position + Vector3.forward;
            MoveTo(newPosition);
        }
        
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //forward = (0,0,1)
            Vector3 newPosition = transform.position + Vector3.back;
            MoveTo(newPosition);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //forward = (0,0,1)
            Vector3 newPosition = transform.position + Vector3.left;
            MoveTo(newPosition);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //forward = (0,0,1)
            Vector3 newPosition = transform.position + Vector3.right;
            MoveTo(newPosition);
        }
    }

    void MoveTo(Vector3 newPosition)
    {
        //Debug.DrawRay(newPosition, Vector3.down, Color.green, 2f);
        if (Physics.Raycast(newPosition, Vector3.down,1f) && !Physics.Raycast(transform.position, newPosition-transform.position, 1f))
        { 
            allowInput = false;
            isMoving = true;
            //transform.DOMove(newPosition, moveTime).SetEase(Ease.OutElastic);
            transform.DOJump(newPosition, jumpPower, 1, moveTime).OnComplete(ResetInput);
        }

    }

    void ResetInput()
    {
        allowInput = true;
        isMoving = false;
    }

    

}
