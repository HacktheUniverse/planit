using UnityEngine;
using System.Collections;

public class PlanetLauncher : MonoBehaviour 
{
	[SerializeField]
	private ThalmicMyo myo;

	[SerializeField]
	private GameObject planetPrefab;

	private bool isPlanetReady = false;
	
	// Update is called once per frame
	void Update () 
	{
		if(myo.pose == Thalmic.Myo.Pose.Fist && !isPlanetReady)
		{
			isPlanetReady = true;
		}
		else if(myo.pose == Thalmic.Myo.Pose.Rest && isPlanetReady)
		{
			isPlanetReady = false;
			FirePlanet();
		}
	}

	void FirePlanet()
	{
		GameObject newPlanet = (GameObject)Instantiate (planetPrefab);
		newPlanet.transform.position = gameObject.transform.position;
		newPlanet.rigidbody.AddForce (Vector3.forward * 5000);
	}
}
