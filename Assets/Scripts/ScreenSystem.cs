using UnityEngine;
using System.Collections;

public class ScreenSystem : MonoBehaviour 
{
	Resolution nativeResolution;
	bool isCorrected = false;
	bool useCorrectedResolution = true; //should always be true unless currently testing

	// Use this for initialization
	void Start () 
	{
		nativeResolution = Screen.resolutions [Screen.resolutions.Length - 1];
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(isCorrected || !useCorrectedResolution)
			return;

		if(nativeResolution.width != Screen.width || nativeResolution.height != Screen.height)
		{
			Screen.SetResolution(nativeResolution.width, nativeResolution.height, true);
			isCorrected = true;
		}
	}
}
