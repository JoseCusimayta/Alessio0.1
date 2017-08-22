using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionParallax : MonoBehaviour {

    public FreeParallax parallax;
    public GameObject cloud;

    // Use this for initialization
    void Start()
    {
        if (cloud != null)
        {
            cloud.GetComponent<Rigidbody2D>().velocity = new Vector2(0.1f, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (parallax != null)
        {
            if (Input.GetKey(KeyCode.A))
            {
                parallax.Speed = 15.0f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                parallax.Speed = -15.0f;
            }
            else
            {
                parallax.Speed = 0.0f;
            }
        }
    }
}
