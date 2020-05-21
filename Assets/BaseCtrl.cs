using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCtrl : MonoBehaviour
{
	private bool x = false;
	private int mutiply;
	// Update is called once per frame
	void Start(){

	}
	void FixedUpdate()
	{	
		/*************************** old stuffs **************************************/
		/*
		if(transform.position.x < 0.2f && !x)
		{
			Instantiate(gameObject, new Vector3(17.79f, 0, 5), Quaternion.identity, GameObject.FindGameObjectWithTag("Terrain").transform);
			x = true;
		}
		if (transform.position.x < -17.79f){
			Destroy(gameObject);
		}
		*/
		/*************************** old stuffs **************************************/
		/*************************** new stuffs **************************************/
		
		if(transform.position.x < -10f && !x)
		{
			Instantiate(gameObject, new Vector3(14f, 0, 5), Quaternion.identity, GameObject.FindGameObjectWithTag("Terrain").transform);
			x = true;
		}
		if (transform.position.x < -11f){
			Destroy(gameObject);
		}
		
		/*************************** new stuffs **************************************/
	}
}
