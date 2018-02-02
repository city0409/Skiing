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
    private bool isRolled;

    private Vector3 TowardsDown = new Vector3(-0.5f, -1f, 0);
    private Vector3 TowardsUp = new Vector3(0.5f, 1f, 0);
    private Vector3 TowardsLeft = new Vector3(-1f, 0.5f, 0);
    private Vector3 TowardsRight = new Vector3(1f, -0.5f, 0);

    [SerializeField]
    private SnowManController snowMan;
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private LayerMask layerMaskOther;

    private Quaternion initRotation;

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

        initRotation = transform.rotation;
    }

    private void Update()
    {
        //print(playColl.bounds.extents.y);
        if (!isJumped)
        {
            isJumped = Input.GetMouseButtonDown(0);
            PlasyerState.IsRoll = false;

            if (!isRolled)
            {
                isRolled = Input.GetMouseButtonDown(0);
            }
        }

<<<<<<< HEAD
        //if (myState.IsRideSnowMan)
        //{
        //    GoYou();
        //}
      
=======

        if (PlasyerState.IsRideSnowMan)
        {
            GoYou();
        }
        if (PlasyerState.IsLie)
        {
            Lie();
        }
        //if (Input.GetMouseButtonDown(1) &&   PlasyerState.IsRoll)
        //{
        //    StartCoroutine(Torque());

        //}
>>>>>>> parent of e19695b... 0201，Motor和Controller分开，导演生成Player
        DetacteRaycast();

    }

    private void FixedUpdate()
    {
        if (isJumped && PlasyerState. IsSkiing==true )
        {
            Jump();
        }
        if (isRolled && PlasyerState.IsRoll==true)
        {
            Roll();
        }
        //rig.velocity = new Vector2(speed, rig.velocity.y);
        if (!PlasyerState.IsLie )
        {
            rig.AddForce(new Vector2(force, -force * 0.5f), ForceMode2D.Force);

            speed = Mathf.Clamp(Vector3.SqrMagnitude(rig.velocity), 0f, maxSpeed);

            rig.velocity = rig.velocity.normalized * speed;
        }
        else if (PlasyerState.IsLie)
        {
            Debug.Log("躺下了");
            rig.gravityScale = 0;

        }
        
        isJumped = false;
        isRolled = false;

    }

<<<<<<< HEAD
    //private void GoYou()
    //{
    //    visual1.SetActive(false);
    //    visual2.SetActive(true);
    //    playColl.offset = new Vector2(0f, 0.34f);
    //    playColl.size = new Vector2(1.02f, 1.68f);
    //    myState.IsSkiing = true;
    //}
=======
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
>>>>>>> parent of e19695b... 0201，Motor和Controller分开，导演生成Player

    private void Roll()
    {
        print("Roll");
        rig.AddTorque(30);
        PlasyerState.IsRoll = false;

    }

    private void Lie()
    {
        Debug.Log("Lie");
        rig.velocity = Vector3.zero;
        transform.rotation = initRotation;
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
        Ray rayRight = new Ray(transform.position, TowardsRight);
        Ray rayLeft = new Ray(transform.position, TowardsLeft);
        Ray rayUp = new Ray(transform.position, TowardsUp);
        Ray rayDown = new Ray(transform.position, TowardsDown);
        RaycastHit2D rayDownHit = Physics2D.Raycast(transform.position, TowardsDown, 0.6f, layerMask);
        RaycastHit2D rayUpHit = Physics2D.Raycast(transform.position,Vector2.up , 0.6f, layerMask);//射线不动啊
        RaycastHit2D rayLeftHit = Physics2D.Raycast(transform.position, TowardsLeft, 0.6f, layerMaskOther);
        RaycastHit2D rayRightHit = Physics2D.Raycast(transform.position, TowardsRight,1f, layerMaskOther);
        if (rayDownHit && (rayDownHit.collider.gameObject.layer == 8 /*|| rayDownHit.collider.gameObject.layer == 9*/))
        {
<<<<<<< HEAD
            //myState.IsSkiing=true ;
            //myState.IsJump = false;

            myState.IsGround = true;
        }
        else if (rayDownHit == false)
        {
            myState.IsGround = false;
            
=======
            PlasyerState.IsSkiing=true ;
            PlasyerState.IsJump = false;
        }
        else if (rayDownHit == false)
        {
            PlasyerState.IsJump = false ;
            PlasyerState.IsRoll = true;

>>>>>>> parent of e19695b... 0201，Motor和Controller分开，导演生成Player
        }

         if (rayUpHit.collider !=null )
        {
            Debug.Log("jiance");
<<<<<<< HEAD
            //myState.IsJump = false;
            //myState.IsLie = true;
=======
            PlasyerState.IsJump = false;
            PlasyerState.IsLie = true;
>>>>>>> parent of e19695b... 0201，Motor和Controller分开，导演生成Player
        }

        SlipDetacte();
    }

    //判断是否滑倒了
    private void SlipDetacte() {
        MyState.IsLie = Vector3.Dot(transform.up, TowardsDown) >= 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + TowardsDown * 1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + TowardsUp * 1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + TowardsLeft * 1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + TowardsRight * 1f);

    }

}
