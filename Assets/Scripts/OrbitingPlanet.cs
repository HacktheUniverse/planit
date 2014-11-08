using UnityEngine;
using System.Collections;

public class OrbitingPlanet : MonoBehaviour 
{
	float revolutionsPerSecond = 0.1f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.RotateAround (Vector3.zero, Vector3.up, revolutionsPerSecond);
	}
}
