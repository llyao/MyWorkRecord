using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelyTimeObj : MonoBehaviour
{

    public float delayTime = 10.0f;

    protected float startTime = 0;
	// Use this for initialization
	void Start ()
	{
	    startTime = Time.time;
	}

    public void Update()
    {
        if (Time.time > (startTime + delayTime))
        {
            GameObject.Destroy(gameObject);
        }
    }
	
}
