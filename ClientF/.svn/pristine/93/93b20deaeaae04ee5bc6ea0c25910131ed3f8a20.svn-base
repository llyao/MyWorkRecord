using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void OnPlayEffect(GameObject pref_)
    {
        Transform t = transform.Find(pref_.name);
        if (t == null)
            t = transform;

        GameObject go = GameObject.Instantiate(pref_) as GameObject;
        go.transform.parent = t;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
    }
}
