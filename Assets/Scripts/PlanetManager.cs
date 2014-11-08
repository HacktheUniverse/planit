using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour 
{
	List<GameObject> allPlanets;

	// Use this for initialization
	void Start () 
	{
		//initialize our list
		allPlanets = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void FindAllPlanets()
	{
		GameObject[] planetArray = GameObject.FindGameObjectsWithTag ("Planet");

		for(int i = 0; i < planetArray.Length; i++)
		{
			allPlanets.Add(planetArray[i]);
		}
	}
}
