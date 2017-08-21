using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionarAnimacionGolpe : MonoBehaviour {
    public GameObject playerObject;

    public GameObject rightHitbox;
    public GameObject leftHitbox;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    // Use this for initialization
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableAnimator()
    {
        _animator.speed = 1;
    }

    public void DisableAnimator()
    {
        _animator.speed = 0;

        Invoke("EnableAnimator", 1);
    }

    //public void EnablePlayerControl()
    //{
    //    playerObject.GetComponent<Jugador>().canControl = true;
    //}

    //public void DisablePlayerControl()
    //{
    //    playerObject.GetComponent<Jugador>().canControl = false;
    //}

    public void EnableHitboxes()
    {
        //Debug.Log("Se activa el hitbox");
        if (_spriteRenderer.flipX)
        {
            Debug.Log("Se activa el hitbox1");
            leftHitbox.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            //Debug.Log("Se activa el hitbox2");
            rightHitbox.GetComponent<BoxCollider>().enabled = true;
            //rightHitbox.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void DisableHitboxes()
    {
        //Debug.Log("Se desactiva el collider del golpe");
        if (_spriteRenderer.flipX)
        {
            //Debug.Log("Se desaactiva el hitbox1");
            leftHitbox.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            //Debug.Log("Se desaactiva el hitbox2");
            rightHitbox.GetComponent<BoxCollider>().enabled = false;
        }

    }



    //public void EnablePlayerCanControl()
    //{
    //    playerObject.GetComponent<Jugador>().canAttack = true;
    //}

    //public void DisablePlayerCanControl()
    //{
    //    playerObject.GetComponent<Jugador>().canAttack = false;
    //}

    public void SetAttack1_HitBoxesSize()
    {
        //Debug.Log("Se amplia rango ataque");
        if (_spriteRenderer.flipX == false)
        {
            rightHitbox.GetComponent<BoxCollider>().size = new Vector2(1.641296f, 1);
            rightHitbox.GetComponent<BoxCollider>().center = new Vector2(0.3206476f, 0);
        }
        else
        {
            leftHitbox.GetComponent<BoxCollider>().size = new Vector2(1.536815f, 0f);
            leftHitbox.GetComponent<BoxCollider>().center = new Vector2(-0.2684076f, 1f);
        }

    }
}
