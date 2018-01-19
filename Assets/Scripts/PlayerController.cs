using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Idle, Jump }

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float jumpForce = 200f;

    private Rigidbody2D rig;
    private State currentState = State.Idle;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentState == State.Idle)
        {
            currentState = State.Jump;
        }
        if (  currentState == State.Jump )
        {
            currentState = State.Idle;
            StartCoroutine(Jump());
        }
        if (Input.GetMouseButtonDown(1) && currentState == State.Jump)
        {
            StartCoroutine(Torque());

        }
    }

    private IEnumerator Jump()
    {
        rig.AddForce(new Vector2(1, 1) * jumpForce);

        yield return null;
    }

    private IEnumerator Torque()
    {
        rig.AddTorque(10f, ForceMode2D.Force);

        yield return null;
    }
}
