using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : Singleton<PlayerController>
{
    private PlayerMotor playerMotor;
    private LevelDirector levelDirector;
    private PlayerState myState;
    public PlayerState MyState { get { return myState; }set { myState = value; } }

    [SerializeField]
    private GameObject visual1;
    [SerializeField]
    private GameObject visual2;
    private BoxCollider2D playColl;

    private bool m_Jump;
    private bool m_Roll;

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


    protected override void Awake()
    {
        base.Awake();
        snowMan = GetComponent<SnowManController>();
        playColl = GetComponent<BoxCollider2D>();
        myState = new PlayerState();

        playerMotor = GetComponent<PlayerMotor>();
    }

    private void Start()
    {
        myState.IsSkiing = true;
    }

    private void Update()
    {
        if (!m_Jump)
        {
            m_Jump = Input.GetMouseButtonDown(0);
            myState.IsRoll = false;

            if (!m_Roll)
                m_Roll = Input.GetMouseButtonDown(0);
        }

        if (myState.IsRideSnowMan)
        {
            GoYou();
        }
      
        DetacteRaycast();
    }

    private void FixedUpdate()
    {
        playerMotor.Movement(m_Jump, m_Roll);

        if (myState.IsLie)
        {
            Debug.Log("躺下了");
            playerMotor.Lie();
        }
        
        m_Jump = false;
        m_Roll = false;

    }

    private void GoYou()
    {
        visual1.SetActive(false);
        visual2.SetActive(true);
        playColl.offset = new Vector2(0f, 0.34f);
        playColl.size = new Vector2(1.02f, 1.68f);
        myState.IsSkiing = true;
    }

    private void DetacteRaycast()
    {
        RaycastHit2D rayDownHit = Physics2D.Raycast(transform.position, TowardsDown, 0.6f, layerMask);
        RaycastHit2D rayUpHit = Physics2D.Raycast(transform.position,TowardsUp  , 0.6f, layerMask);//射线不动啊
        RaycastHit2D rayLeftHit = Physics2D.Raycast(transform.position, TowardsLeft, 0.6f, layerMaskOther);
        RaycastHit2D rayRightHit = Physics2D.Raycast(transform.position, TowardsRight,1f, layerMaskOther);
        if (rayDownHit && (rayDownHit.collider.gameObject.layer == 8 /*|| rayDownHit.collider.gameObject.layer == 9*/))
        {
            myState.IsSkiing=true ;
            myState.IsJump = false;
        }
        else if (rayDownHit == false)
        {
            myState.IsJump = false ;
            myState.IsRoll = true;
        }

         if (rayUpHit.collider !=null )
        {
            Debug.Log("jiance");
            myState.IsJump = false;
            myState.IsLie = true;
        }
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
