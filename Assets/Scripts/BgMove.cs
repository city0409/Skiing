using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMove : MonoBehaviour 
{
    private Renderer rend;
    private Material mat;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
        mat = rend.material;
    }

    void Update()
    {
        mat.SetTextureOffset("_MainTex", new Vector3(0, Time.time / 8));
    }
}
