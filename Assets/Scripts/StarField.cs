using UnityEngine;
using System.Collections;

public class StarField : MonoBehaviour 
{
	[SerializeField]
	private GameObject starPrefab;
	
	private int starCount = 50;
	private float starFieldResolution = 100;

	// Use this for initialization
	void Start () 
	{
		PopulateStarField ();
	}
	
	void PopulateStarField()
	{
		for(int i = 0; i < starCount; i++)
		{
			GameObject newStar = (GameObject)Instantiate(starPrefab);
			Vector3 randomPos = new Vector3(Random.Range(-100 + -starFieldResolution, 100 + starFieldResolution), Random.Range(-100 + -starFieldResolution, 100 + starFieldResolution), Random.Range(-100 + -starFieldResolution, 100 + starFieldResolution));
			newStar.transform.position = randomPos;
		}
	}
}
