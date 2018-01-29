using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Singleton<PlayerController>
{
    //public enum State { Skiing, Jump, RideSnowMan, lie }

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
    //private State currentState = State.Skiing;
    //public State CurrentState { get { return currentState; } set { currentState = value; } }
    public  float speed; //ToDo


    private bool isJumped;

    [SerializeField]
    private SnowManController snowMan;
    [SerializeField]
    private LayerMask layerMask;

    protected override void Awake()
    {
        base.Awake();
        rig = GetComponent<Rigidbody2D>();
        snowMan = GetComponent<SnowManController>();
        playColl = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        PlasyerState.IsSkiing = true;
        print("123@@@"+PlasyerState.IsJump);
    }

    private void Update()
    {
        if (!isJumped)
            isJumped = Input.GetMouseButtonDown(0);

        if (PlasyerState.IsRideSnowMan)
        {
            GoYou();
        }
        if (PlasyerState.IsLie)
        {
            Lie();
        }
        //if (Input.GetMouseButtonDown(1) && currentState == State.Jump)
        //{
        //    StartCoroutine(Torque());

        //}
        DetacteRaycast();

    }

    private void FixedUpdate()
    {
        print(PlasyerState.IsSkiing);
        if (isJumped && PlasyerState. IsSkiing==true )
        {
            Jump();
            print(PlasyerState.IsSkiing);
        }
        //rig.velocity = new Vector2(speed, rig.velocity.y);
        rig.AddForce(new Vector2(force, -force * 0.5f), ForceMode2D.Force);

        speed = Mathf.Clamp(Vector3.SqrMagnitude(rig.velocity), 0f, maxSpeed);
        rig.velocity = rig.velocity.normalized * speed;
        isJumped = false;

    }

    private void GoYou()
    {
        visual1.SetActive(false);
        visual2.SetActive(true);
        playColl.offset = new Vector2(0f, 0.34f);
        playColl.size = new Vector2(1.02f, 1.68f);
        PlasyerState.IsSkiing = true;
    }

    private void Jump()
    {
        rig.AddForce(new Vector2(0.8f, 1f) * jumpForce, ForceMode2D.Impulse);
        //currentState = State.Jump;
        PlasyerState.IsSkiing = false;
        PlasyerState.IsJump = true;
        //yield return new WaitForSeconds (0.5f);不需要用协程
    }

    private void Lie()
    {
        rig.velocity = Vector3.zero;
    }

    private IEnumerator Torque()
    {
        rig.AddTorque(10f, ForceMode2D.Force);

        yield return new WaitForSeconds(0.5f);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject .tag =="BG")
    //    {
    //        if (currentState == State.Jump)
    //        {
    //            currentState = State.Skiing;

    //        }
    //    }
    //}

    private void DetacteRaycast()
    {
        //Ray rayRight = new Ray(transform.position, Vector3.right);
        //Ray rayLeft = new Ray(transform.position, Vector3.left );
        Ray rayDown = new Ray(transform.position, Vector3.down);
        RaycastHit2D rayDownHit = Physics2D.Raycast(transform.position, Vector3.down, 0.6f, layerMask);
        if (rayDownHit && (rayDownHit.collider.gameObject.layer == 8 || rayDownHit.collider.gameObject.layer == 9))
        {
            PlasyerState.IsSkiing=true ;
            PlasyerState.IsJump = false;
        }
        else if (rayDownHit == false)
        {
            PlasyerState.IsJump = false ;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1f);
    }

}
