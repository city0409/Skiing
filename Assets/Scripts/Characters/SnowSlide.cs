﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SnowSlide : MonoBehaviour
{

    [SerializeField]
    private float maxSpeed = 2f;
    [SerializeField]
    private float force = 10f;

    private Rigidbody2D rig;
    private float speed;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rig.AddForce(new Vector2(force, -force), ForceMode2D.Force);

        speed = Mathf.Clamp(Vector3.SqrMagnitude(rig.velocity), 0f, maxSpeed);
        rig.velocity = rig.velocity.normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            PlasyerState.IsLie=true;
            //弹窗todo
            Time.timeScale = 0f;
            //Destroy(collision.gameObject);
        }
    }
}
