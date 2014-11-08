using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		gameObject.transform.Translate (Vector3.up);
	}

	void OnCollisionEnter()
	{


	}

	void OnCollisionStay()
	{

	}
}
