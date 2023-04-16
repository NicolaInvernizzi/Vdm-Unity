using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRed : MonoBehaviour
{
    private void Update()
    {
        if(!GetComponentInParent<MeshRenderer>().enabled)
        {
            GetComponent<ParticleSystem>().Stop();
        }

    }
}

