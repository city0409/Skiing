using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    [SerializeField]
    private Transform player;

    private Vector3 dis;
    private Vector3 tempPos;
    [SerializeField]
    private LayerMask mask;

    //private Transform mainCamera;
    private void Awake () 
	{
        //mainCamera = GetComponent<Transform>();
        dis = player.position - transform.position;
    }
	
	private void LateUpdate () 
	{
        tempPos = transform.position;
        //transform.position = Vector3.Lerp(player.position - dis, transform .position , Time.deltaTime * 5);
        //if (Physics2D.Raycast(player.position, Vector2.down,2f)) {
        //    Debug.Log(hit.point);
        //}
        RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, 10f, mask);
        tempPos = Vector2.Lerp(tempPos, hit.point + new Vector2(10,-5), Time.deltaTime * 5);
        tempPos.z = -10;
        transform.position = tempPos;


    }
}
