using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControlObj : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotateSpeed = 0.5f;

    protected CharacterController cc;
	// Use this for initialization
	void Start ()
	{
	    cc = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float v = ETCInput.GetAxis("Vertical");
	    float h = ETCInput.GetAxis("Horizontal");

	    cc.SimpleMove(transform.forward * v * moveSpeed);
        transform.RotateAround(transform.position , Vector3.up , h * rotateSpeed);
	}
}
