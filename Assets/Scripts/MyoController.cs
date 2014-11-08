using UnityEngine;
using System.Collections;

public class MyoController : MonoBehaviour 
{
	[SerializeField]
	ThalmicMyo myo;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(myo.pose == Thalmic.Myo.Pose.Fist)
		{
			gameObject.renderer.material.color = Color.red;
		}
		else
		{
			gameObject.renderer.material.color = Color.blue;
		}
	}
}
