using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour 
{
	List<Planet> allPlanets;

	// Use this for initialization
	void Start () 
	{
		//initialize our list
		allPlanets = new List<Planet> ();
        FindAllPlanets();
        Debug.Log(allPlanets[0]);
	}
	
	// Update is called once per frame
	void Update () 
	{
        timestep();
	}

	void FindAllPlanets()
	{
		GameObject[] planetArray = GameObject.FindGameObjectsWithTag ("Planet");

		for(int i = 0; i < planetArray.Length; i++)
		{
            Debug.Log(planetArray[i]);
			allPlanets.Add(planetArray[i].GetComponent<Planet>() );
		}
	}

    //Update planet 'pos' by dt.
    void timestep(){
        double dt = 0.1;

        double r;
        int i,j;
        int n = allPlanets.Count;

        double[] r1;
        double[] r2;
        double[,] ir = new double[n,n];
        for(i = 0; i<n; i++) {
            r1 = allPlanets[i].pos;
            ir[i,i] = 0.0;
            for(j = 0; j<n; j++) { 
                if(i != j) {
                    r2 = allPlanets[j].pos;
                    r = System.Math.Sqrt((r1[0]-r2[0])*(r1[0]-r2[0])
                                + (r1[1]-r2[1])*(r1[1]-r2[1])
                                + (r1[2]-r2[2])*(r1[2]-r2[2]));
                    r = 1.0/r;
                    ir[i,j] = r;
                    ir[j,i] = r;
                }
            }
        }

        double[,] f = new double[n,3];
        for(i=0; i<n; i++) {
            f[i,0] = 0.0;
            f[i,1] = 0.0;
            f[i,2] = 0.0;
            r1 = allPlanets[i].pos;
            for(j=0; j<n; j++) {
                r2 = allPlanets[j].pos;
                f[i,0] += -allPlanets[j].mass * (r1[0]-r2[0]) *ir[i,j]*ir[i,j]*ir[i,j];
                f[i,1] += -allPlanets[j].mass * (r1[1]-r2[1]) *ir[i,j]*ir[i,j]*ir[i,j];
                f[i,2] += -allPlanets[j].mass * (r1[2]-r2[2]) *ir[i,j]*ir[i,j]*ir[i,j];
            }
        }

        for(i=0; i<n; i++){
            allPlanets[i].pos[0] += dt*allPlanets[i].vel[0];
            allPlanets[i].pos[1] += dt*allPlanets[i].vel[1];
            allPlanets[i].pos[2] += dt*allPlanets[i].vel[2];
            allPlanets[i].vel[0] += dt*f[i,0];
            allPlanets[i].vel[1] += dt*f[i,1];
            allPlanets[i].vel[2] += dt*f[i,2];
        }
    }
}
