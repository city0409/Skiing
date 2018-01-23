using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Idle, Jump }

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Collider BGcoll;
    [SerializeField]
    private float maxSpeed = 2f;
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private float jumpForce = 200f;

    private Rigidbody2D rig;
    private State currentState = State.Idle;
    private float speed;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentState == State.Idle)
        {
            StartCoroutine(Jump());
        }
        
        //if (Input.GetMouseButtonDown(1) && currentState == State.Jump)
        //{
        //    StartCoroutine(Torque());

        //}
    }

    private void FixedUpdate()
    {
        //rig.velocity = new Vector2(speed, rig.velocity.y);
        rig.AddForce(new Vector2(force, -force), ForceMode2D.Force);

        speed = Mathf.Clamp(Vector3.SqrMagnitude(rig.velocity), 0f, maxSpeed);
        rig.velocity = rig.velocity.normalized * speed;
    }

    private IEnumerator Jump()
    {
        rig.AddForce(new Vector2(1f, 1f) * jumpForce,ForceMode2D.Impulse);
        currentState = State.Jump;

        yield return new WaitForSeconds (0.5f);
    }

    private IEnumerator Torque()
    {
        rig.AddTorque(10f, ForceMode2D.Force);

        yield return new WaitForSeconds(0.5f);
    }

    private void OnCollisionEnter2D(Collision2D BGcoll)
    {
        if (BGcoll.gameObject .tag =="BG")
        {
            if (currentState == State.Jump)
            {
                currentState = State.Idle;

            }
        }
    }
}
