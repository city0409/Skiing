using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour 
{
    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } set { speed = value; } }
    [SerializeField]
    private float jumpForce = 20f;
    [SerializeField]
    private float moveForce = 10f;
    [SerializeField]
    private float gravity = 20f;
    [SerializeField]
    private float maxSpeed = 30f;

    private Rigidbody2D rig;
    private PlayerController controller;

    private Quaternion initRotation;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();
    }

    private void Start()
    {
        initRotation = transform.rotation;
    }

    public void Movement(bool m_Jump,bool m_Roll)
    {
        if (m_Jump && controller.MyState.IsSkiing == true)
        {
            rig.AddForce(new Vector2(0, jumpForce ), ForceMode2D.Impulse);
            //controller.MyState.IsSkiing = false;
            //controller.MyState.IsJump = true;
            if (m_Roll && controller.MyState.IsRoll == true)
            {
                Roll();
            }
        }
        rig.AddForce(new Vector2(moveForce, -gravity), ForceMode2D.Force);

        speed = Mathf.Clamp(Vector3.SqrMagnitude(rig.velocity), 0f, maxSpeed);
        
        rig.velocity = rig.velocity.normalized * speed;
    }

    private void Roll()
    {
        Debug.Log("Roll");
        //rig.AddTorque(30);
        controller.MyState.IsRoll = false;
    }

    public void Lie()
    {
        Debug.Log("Lie");
        rig.velocity = Vector3.zero;
        gravity = 0;
        moveForce = 0;
        transform.rotation = initRotation;
    }
}
