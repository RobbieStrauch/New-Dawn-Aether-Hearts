using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStopper : MonoBehaviour
{
    float ParticlelifeTime = 0.2f;
    void Update()
    {
        StartCoroutine(Disabler());
    }
    IEnumerator Disabler()
    {
        yield return new WaitForSeconds(ParticlelifeTime);
        gameObject.SetActive(false);
    }
}
