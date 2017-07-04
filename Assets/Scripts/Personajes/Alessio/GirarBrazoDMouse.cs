using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirarBrazoDMouse : MonoBehaviour {
    SpriteRenderer _sprite;
	// Use this for initialization
	void Start () {

        _sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.rotation.z >= 0 && transform.rotation.z <= 145)
            transform.Rotate(new Vector3(0, 0, Input.GetAxis("Mouse Y") * 10));

	}
}
