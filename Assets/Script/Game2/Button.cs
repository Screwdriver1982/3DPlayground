using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Button : MonoBehaviour
{
    public Action onButtonPressed = delegate { };
    public Action onButtonExit = delegate { };

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        onButtonPressed();
    }

    private void OnTriggerExit(Collider other)
    {
        onButtonExit();
    }

}
