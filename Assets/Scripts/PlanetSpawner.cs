using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSpawner : MonoBehaviour 
{
	[SerializeField]
	private ThalmicMyo myo;
	
	[SerializeField]
	private GameObject planetPrefab;

	[SerializeField]
	private GameObject addButton = null;

	[SerializeField]
	private GameObject slider;

	private bool isPlanetReady = false;
	private List<GameObject> allPlanets;

	//variables for current planet
	private float currentScale = 1;
	private float scaleLimit = 10;
	private float scaleIncrement = 0.1f;

	private int state = 0; //0 for opening screen, 1 for placing center, 2 for placing planets

	void Start()
	{
		allPlanets = new List<GameObject> ();
	}

	// Update is called once per frame
	void Update () 
	{

	}


	void UpdateSlider()
	{
		if(myo.pose == Thalmic.Myo.Pose.WaveIn)
		{
			slider.transform.Translate (-scaleIncrement, 0, 0);

			if(slider.transform.position.x < -2.2f)
			{
				Vector3 newPos = new Vector3(-2.2f, slider.transform.position.y, slider.transform.position.z);
				slider.transform.position = newPos;
			}
		}
		if(myo.pose == Thalmic.Myo.Pose.WaveOut)
		{
			slider.transform.Translate (scaleIncrement, 0, 0);

			if(slider.transform.position.x > 2.6f)
			{
				Vector3 newPos = new Vector3(2.6f, slider.transform.position.y, slider.transform.position.z);
				slider.transform.position = newPos;
			}
		}
	}

	void UpdateCenter()
	{
		if(myo.pose == Thalmic.Myo.Pose.Fist && !isPlanetReady)
		{
			isPlanetReady = true;
		}
		else if(myo.pose == Thalmic.Myo.Pose.Rest && isPlanetReady)
		{
			isPlanetReady = false;
			CreateSystemCenter();
		}

		//adjusting scale
		if(myo.pose == Thalmic.Myo.Pose.WaveIn && currentScale >= 1)
		{
			currentScale -= scaleIncrement;
		}
		else if(myo.pose == Thalmic.Myo.Pose.WaveOut && currentScale <= scaleLimit)
		{
			currentScale += scaleIncrement;
		}
	}

	void UpdatePlanet()
	{
		//three units between 1 and 10
	}

	void CreateNewPlanet()
	{

	}

	void CreateSystemCenter()
	{
		GameObject sun = (GameObject)Instantiate (planetPrefab);
		sun.transform.position = Vector3.zero;
		sun.transform.localScale = new Vector3 (currentScale, currentScale, currentScale);

		if(addButton != null)
		{
			Destroy(addButton);
		}
	}
}
