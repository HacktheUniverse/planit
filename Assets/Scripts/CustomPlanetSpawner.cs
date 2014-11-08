using UnityEngine;
using System.Collections;

public class CustomPlanetSpawner : MonoBehaviour 
{
	[SerializeField]
	private GameObject customPlanetPrefab;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.K))
		{
			Instantiate(customPlanetPrefab);
		}
	}
}
