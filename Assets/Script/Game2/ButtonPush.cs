using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class ButtonPush : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Collider buttonCollider;
    [SerializeField] float finalPositionY;
    [SerializeField] float pushTime;
    [SerializeField] MeshRenderer rend;
    [SerializeField] Material inActiveMaterial;
    // Start is called before the first frame update
    void Start()
    {
        button.onButtonPressed += Push;
    }

    void Push()
    {
        buttonCollider.enabled = false;
        rend.material = inActiveMaterial;
        transform.DOMoveY(finalPositionY, pushTime);
    }
}
