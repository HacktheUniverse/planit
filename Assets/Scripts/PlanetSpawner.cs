using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSpawner : MonoBehaviour 
{
	[SerializeField]
	private ThalmicMyo myo;
	
	[SerializeField]
	private GameObject planetPrefab;

	private bool isPlanetReady = false;
	private List<GameObject> allPlanets;

	void Start()
	{
		allPlanets = new List<GameObject> ();
	}

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
		}
	}

	void SpawnPlanet()
	{
		if(allPlanets.Count == 0)
		{
			CreateSystemCenter();
		}
		else
		{
			//begin custom planet generation
		}
	}

	void CreateSystemCenter()
	{
		GameObject sun = (GameObject)Instantiate (planetPrefab);
		sun.transform.position = Vector3.zero;
	}
}
