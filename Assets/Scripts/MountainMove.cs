using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainMove : MonoBehaviour
{
    private Material mat;
    private Renderer rend;
    [SerializeField]
    private PlayerController Player;

    private void Awake()
    {
        rend = GetComponentInChildren<Renderer>();
        mat = rend.material;
    }

    private void Update()
    {
        mat.mainTextureOffset += new Vector2(Time.deltaTime * Player.speed / 100, 0f);
    }
}
