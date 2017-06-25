using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {
    public GameObject player;
    public StickManMovement _stickManMovement;
	// Use this for initialization
	void Start () {
        _stickManMovement = player.GetComponent<StickManMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void DropWeapon()
    {
        _stickManMovement.DropWeapon();
    }
    public void FinishReleasing()
    {
        //Le decimos que ya no está lanzando el arma
        _stickManMovement._isReleasing = false;
    }
}
