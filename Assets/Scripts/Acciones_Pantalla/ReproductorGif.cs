using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReproductorGif : MonoBehaviour {
	int index;
	public int FramesPorSegundo=10;
	public Texture2D[] Frames;
	public Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rend.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		index=(int)(Time.time*FramesPorSegundo)%Frames.Length;
		rend.material.mainTexture = Frames[index];
	}

}
