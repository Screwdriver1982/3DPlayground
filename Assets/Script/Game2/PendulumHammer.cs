using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumHammer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CubeMovement cube = other.GetComponent<CubeMovement>();
        cube.Die();
    }
}
