using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchTrigger : MonoBehaviour
{
    private float glitchTime = 0f;
    private GlitchSet glitchSet;

    private void Start()
    {
        glitchSet = FindAnyObjectByType<GlitchSet>();    
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "Player")
       {
            glitchSet.AddGlitch(100, 5, 5f);     
       }
    }
}
