using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Singleton<PlayerController>
{
    public enum State { Idle, Jump, RideSnowMan }

    [SerializeField]
    private Collider BGcoll;
    [SerializeField]
    private float maxSpeed = 2f;
    [SerializeField]
    private float force = 10f;
    [SerializeField]
    private float jumpForce = 200f;

    [SerializeField]
    private GameObject visual1;
    [SerializeField]
    private GameObject visual2;
    private BoxCollider2D playColl;

    private Rigidbody2D rig;
    private State currentState = State.Idle;
    public State CurrentState { get { return currentState; } set { currentState = value; } }
    private float speed;

    [SerializeField]
    private SnowManController snowMan;

    protected override void Awake()
    {
        base.Awake();
        rig = GetComponent<Rigidbody2D>();
        snowMan = GetComponent<SnowManController>();
        playColl = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentState == State.Idle)
        {
            Jump();
        }
        if (currentState ==State.RideSnowMan)
        {
            GoYou();
        }
        //if (Input.GetMouseButtonDown(1) && currentState == State.Jump)
        //{
        //    StartCoroutine(Torque());

        //}
    }

    private void FixedUpdate()
    {
        //rig.velocity = new Vector2(speed, rig.velocity.y);
        rig.AddForce(new Vector2(force, -force*0.5f), ForceMode2D.Force);

        speed = Mathf.Clamp(Vector3.SqrMagnitude(rig.velocity), 0f, maxSpeed);
        rig.velocity = rig.velocity.normalized * speed;
    }

    private void GoYou()
    {
        visual1.SetActive(false);
        visual2.SetActive(true);
        playColl.offset = new Vector2(0f, 0.34f);
        playColl.size = new Vector2(1.02f, 1.68f);
        currentState = State.Idle;
    }

    private void Jump()
    {
        rig.AddForce(new Vector2(0.8f, 1f) * jumpForce,ForceMode2D.Impulse);
        currentState = State.Jump;

        //yield return new WaitForSeconds (0.5f);不需要用协程
    }

    private IEnumerator Torque()
    {
        rig.AddTorque(10f, ForceMode2D.Force);

        yield return new WaitForSeconds(0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject .tag =="BG")
        {
            if (currentState == State.Jump)
            {
                currentState = State.Idle;

            }
        }
    }

}
