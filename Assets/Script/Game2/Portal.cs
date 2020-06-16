using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Vector3 teleportPosition;
    private void OnTriggerEnter(Collider other)
    {
        print("enterTrigger");
        CubeMovement cube = other.GetComponent<CubeMovement>();
        if (cube != null)
        {
            print("teleport me please");
            cube.UsingPortalTo(teleportPosition);
        }
    }
}
