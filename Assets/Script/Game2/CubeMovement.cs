using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.WSA.Input;
using Lean.Pool;

public class CubeMovement : MonoBehaviour
{
    [Header("Moving")]
    [SerializeField] float moveTime = 0.5f;
    [SerializeField] float jumpPower = 0.6f;
    
    [Header("DeathConfig")]
    [SerializeField] GameObject deathEffect;
    [SerializeField] float dropTime = 1f;
    [SerializeField] float reloadLevelDelay = 1f;

    [Header("TeleportConfig")]
    [SerializeField] Vector3 teleportPosition;
    [SerializeField] float fadeTime = 2;
    [SerializeField] GameObject teleportEffect;


    [Header("Sounds")]
    [SerializeField] AudioClip deathsound;



    Rigidbody rb;
    bool allowInput;
    bool isMoving;
    bool blocked;
    Sequence teleportSequence;
    Material cubeMaterial;
    
    #region DieAndDrop

    public void Die()
    {
        GameObject newObject = Instantiate(deathEffect, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySound(deathsound);
        ScenesLoader.Instance.LoadLevel(reloadLevelDelay, true);
        Destroy(gameObject);
    }
    public void CubeDrop()
    {
        print("cube drop start");
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
        blocked = false;
        cubeMaterial = GetComponent<MeshRenderer>().material;
        teleportSequence = DOTween.Sequence();
        teleportSequence.Pause();
        teleportSequence.Append(cubeMaterial.DOFade(0, fadeTime))
                     .AppendCallback(() => Teleport(teleportPosition))
                     .Append(cubeMaterial.DOFade(1, fadeTime))
                     .AppendCallback(()=> TeleportEnded())
                     .SetLoops(-1, LoopType.Restart);


        allowInput = true;
        isMoving = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!allowInput || blocked) {return;}
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //forward = (0,0,1)
            MoveForward();
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //forward = (0,0,1)
            MoveBack();
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //forward = (0,0,1)
            MoveLeft();
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //forward = (0,0,1)
            MoveRight();
        }
    }

    public void MoveRight()
    {
        if (!allowInput || blocked) { return; }
        Vector3 newPosition = transform.position + Vector3.right;
        MoveTo(newPosition);
    }

    public void MoveLeft()
    {
        if (!allowInput || blocked) { return; }
        Vector3 newPosition = transform.position + Vector3.left;
        MoveTo(newPosition);
    }

    public void MoveBack()
    {
        if (!allowInput || blocked) { return; }
        Vector3 newPosition = transform.position + Vector3.back;
        MoveTo(newPosition);
    }

    public void MoveForward()
    {
        if (!allowInput || blocked) { return; }
        Vector3 newPosition = transform.position + Vector3.forward;
        MoveTo(newPosition);
    }

    void MoveTo(Vector3 newPosition)
    {
        //Debug.DrawRay(newPosition, Vector3.down, Color.green, 2f);
        if (Physics.Raycast(newPosition, Vector3.down,1f) && !Physics.Raycast(transform.position, newPosition-transform.position, 1f))
        { 
            allowInput = false;
            isMoving = true;
            //transform.DOMove(newPosition, moveTime).SetEase(Ease.OutElastic);
            transform.DOJump(newPosition, jumpPower, 1, moveTime).OnComplete(AllowInput);
        }

    }


    void AllowInput()
    {
        allowInput = true;
        isMoving = false;
    }
    
    public void UsingPortalTo(Vector3 teleportTo)
    {
        blocked = true;
        teleportPosition = teleportTo;
        teleportSequence.Play();
    
    }

    void Teleport(Vector3 teleportPosition)
    {
        LeanPool.Spawn(teleportEffect, transform.position, transform.rotation);
        transform.position = teleportPosition;
    }
    void TeleportEnded()
    {
        LeanPool.Spawn(teleportEffect, transform.position, transform.rotation);
        blocked = false;
        teleportSequence.Pause();
        
    }

}
