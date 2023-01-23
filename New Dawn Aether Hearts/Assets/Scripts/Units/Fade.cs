using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://www.youtube.com/watch?v=oNz4I0RfsEg

public class Fade : MonoBehaviour
{
    public static Fade instance;

    public GameObject quad;
    private MeshRenderer mesh;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }

        //quad = GetComponentInChildren<GameObject>();
        mesh = quad.GetComponent<MeshRenderer>();
    }

    IEnumerator FadeOut()
    {
        for (float f = 1.0f; f >= -0.05f; f -= 0.05f)
        {
            Color c = mesh.material.color;
            c.a = f;
            mesh.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void StartFading()
    {
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
