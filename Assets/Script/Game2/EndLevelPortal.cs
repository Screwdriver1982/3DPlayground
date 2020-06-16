using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelPortal : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] AudioClip openPortalSound;
    [SerializeField] Collider colliderOfPortal;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.Instance.endLevel += ActivatePortal;
        colliderOfPortal = GetComponent<Collider>();
        colliderOfPortal.enabled = false;
    }

    void ActivatePortal()
    {
        colliderOfPortal.enabled = true;
        AudioManager.Instance.PlaySound(openPortalSound);
        Instantiate(effect, transform.position, effect.transform.rotation);

    }

    private void OnTriggerEnter(Collider other)
    {
        CubeMovement cube = other.GetComponent<CubeMovement>();
        if (cube != null)
        {
            cube.enabled = false;
        }

        ScenesLoader.Instance.LoadLevel(3f, true);
    }
}
