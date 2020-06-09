using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject collectEffect;
    [SerializeField] float maxRotateDelay;
    [SerializeField] AudioClip collectSound;
    
    float rotateDelay;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.NewCoin();
        rotateDelay = Random.Range(0f,maxRotateDelay);
        //print(rotateDelay);
        StartCoroutine(RotateCoroutine(rotateDelay));
    }

    IEnumerator RotateCoroutine(float rotateDelay)
    {
        yield return new WaitForSeconds(rotateDelay);
        anim.enabled = true;
    
    }
    private void OnTriggerEnter(Collider other)
    {
        
        LevelManager.Instance.AddCoin();
        AudioManager.Instance.PlaySound(collectSound);
        Instantiate(collectEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}
