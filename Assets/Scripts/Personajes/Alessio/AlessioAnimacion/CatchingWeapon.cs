using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchingWeapon : MonoBehaviour
{
    #region Variables
    public SpriteRenderer _spriteRenderer;
    public bool isCatching;
    public bool isDropOut;
    public Quaternion _quaternion;
    public Vector3 _localScale;
    public BoxCollider _boxCollider;
    public float dropSpeed = 1;
    public float _speed = 1;
    public float _dropTime = 10;
    public Rigidbody _rigidBody;
    public Shoot _shoot;
    private Transform _Parent;
    private StickManMovement _stickManMovement;
    public bool _isSeeingRight;
    #endregion 
    // Use this for initialization
    void Start()
    {
        _quaternion = new Quaternion(0, 180, 0, 0);
        _localScale = new Vector3(0.3f, 0.3f, 1);
        _boxCollider = GetComponent<BoxCollider>();
        _rigidBody = GetComponent<Rigidbody>();
        _shoot = GetComponentInChildren<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCatching)
        {
            transform.rotation = _quaternion;
            //transform.rotation = _Parent.rotation;
            transform.localScale = _localScale;
        }
        if (_stickManMovement)
        {
            if (_stickManMovement._isReleasing)
            {
                transform.rotation = _Parent.rotation;
            }
            if (!isCatching && !_stickManMovement._isReleasing && transform.rotation != Quaternion.identity && _speed < 0)
            {

                if (_isSeeingRight)
                {
                    transform.rotation = Quaternion.identity;                    
                }
                else
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0); 
                }
            }

        }
        if (isDropOut)
        {

            Vector2 moveVector = new Vector2(0, 0);
            moveVector.x = _speed;
            if (_speed > 0)
            {
                _speed -= _dropTime * Time.deltaTime;
                transform.Translate(moveVector);
            }
            else
            {
                _Parent = null;
                _stickManMovement = null;
                isDropOut = false;
            }
        }
    }
    public void CatchWeapon(Transform ObjectParent)
    {
        _stickManMovement = ObjectParent.GetComponentInParent<StickManMovement>();
        _Parent = ObjectParent;
        transform.SetParent(ObjectParent);
        transform.position = ObjectParent.position;
        isCatching = true;
        _speed = dropSpeed;

    }

    public void DropWeapon()
    {
        isCatching = false;
        isDropOut = true;
        transform.SetParent(null);
    }
    public void Disprar()
    {
        _shoot.Disparar();
    }
    public void ManageFlipping(bool _SeeingRight)
    {
        _isSeeingRight = _SeeingRight;
        if (_SeeingRight)
        {
            _quaternion = Quaternion.identity;
        }
        else
        {
            _quaternion = new Quaternion(0, 180, 0, 0);
        }
    }
}
