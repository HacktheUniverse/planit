using UnityEngine;
using System.Collections;

public class CustomPlanet : MonoBehaviour 
{
	[SerializeField]
	private GameObject guiPrefab;

	[SerializeField]
	private ThalmicMyo myo;
	
	[SerializeField]
	private GameObject planetPrefab;
	
	private GameObject slider = null;
	private GameObject gui = null;

	//variables for current planet
	private float lowerScaleBound = 1;
	private float upperScaleBound = 10;

	//variables for slider
	private float sliderUpperBound = 2.6f;
	private float sliderLowerBound = -2.2f;

	bool isFistActionActive = false;

	int currentStage = 0; //0 for mass, 1 for velocity, 2 for distance, 3 for spawn

	float currentScale = 0;
	float currentVelocity = 0;
	float currentDistance = 0;

	float massScalar = 5;
	float distanceScalar = 10;
	float velocityScalar = 1;

	[SerializeField]
	Texture velocityGUI;
	[SerializeField]
	Texture distanceGUI;

	// Use this for initialization
	void Start () 
	{
		gui = (GameObject)Instantiate (guiPrefab);
		gui.transform.position = GameObject.FindGameObjectWithTag ("Player").transform.position;
		gui.transform.Translate (0f, 0f, 5f, Space.World);
		gui.transform.LookAt (GameObject.FindGameObjectWithTag ("Player").transform);
		gui.transform.Rotate (90f, 0f, 0f);
		slider = GameObject.FindGameObjectWithTag("Slider");
		gui = slider.transform.parent.gameObject;

		myo = GameObject.FindGameObjectWithTag ("Myo").GetComponent<ThalmicMyo>();

       this.AddComponent<Planet>()
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(slider == null)
			slider = GameObject.FindGameObjectWithTag("Slider");

		UpdateSlider ();

		if(myo.pose == Thalmic.Myo.Pose.Fist && !isFistActionActive)
		{
			isFistActionActive = true;
		}
		else if(myo.pose == Thalmic.Myo.Pose.Rest && isFistActionActive)
		{
			isFistActionActive = false;

			switch(currentStage)
			{
			case 0:
				UpdateMass();
				IncrementStage();
				break;
			case 1:
				UpdateVelocity();
				IncrementStage();
				break;
			case 2:
				UpdateDistance();
				IncrementStage();
				break;
			}
		}
	}

	void UpdateMass()
	{
		//how to get arbitrary range
		float difference = -1 * slider.transform.position.x + sliderLowerBound;
		currentScale = difference * massScalar;
        Planet p = this.GetComponent<Planet>();
        p.mass = currentScale;
	}

	void UpdateVelocity()
	{
		//how to get arbitrary range
		float difference = -1 * slider.transform.position.x + sliderLowerBound;
		currentVelocity = difference;
        Planet p = this.GetComponent<Planet>();
        Vector3 dir = transform.Forward();
        p.v[0] = currentScale * dir.x;
        p.v[1] = currentScale * dir.y;
        p.v[2] = currentScale * dir.z;
	}

	void UpdateDistance()
	{
		float difference = -1 * slider.transform.position.x + sliderLowerBound;
		currentDistance = difference * distanceScalar;

        Planet p = this.GetComponent<Planet>();
        p.pos[0] = currentDistance;
        p.pos[1] = currentDistance;
        p.pos[2] = currentDistance;
	}

	void IncrementStage()
	{
		currentStage++;

		if(currentStage == 1)
		{
			//swap out texture for velocity
			gui.renderer.material.mainTexture = velocityGUI;
		}
		if(currentStage == 2)
		{
			//swap out texture for distance
			gui.renderer.material.mainTexture = distanceGUI;

		}
		if(currentStage == 3)
		{
			GameObject newGameObject = (GameObject)Instantiate(planetPrefab);
			newGameObject.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
			newGameObject.transform.position = new Vector3(currentDistance, 0, 0);

			slider.tag = "Untagged";
			Destroy (slider.transform.parent.gameObject);

		}
	}

	void UpdateSlider()
	{
		if(myo.pose == Thalmic.Myo.Pose.WaveIn)
		{
			slider.transform.Translate (-0.01f, 0, 0);
			
			if(slider.transform.position.x < sliderLowerBound)
			{
				Vector3 newPos = new Vector3(sliderLowerBound, slider.transform.position.y, slider.transform.position.z);
				slider.transform.position = newPos;
			}
		}
		if(myo.pose == Thalmic.Myo.Pose.WaveOut)
		{
			slider.transform.Translate (0.01f, 0, 0);
			
			if(slider.transform.position.x > sliderUpperBound)
			{
				Vector3 newPos = new Vector3(sliderUpperBound, slider.transform.position.y, slider.transform.position.z);
				slider.transform.position = newPos;
			}
		}
	}
}
