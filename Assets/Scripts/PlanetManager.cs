using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetManager : MonoBehaviour 
{
	List<Planet> allPlanets;
    double G = 1.0;
    double dt = 0.05;

	// Use this for initialization
	void Start () 
	{
		//initialize our list
		allPlanets = new List<Planet> ();

        SampleStart();

        FindAllPlanets();
        G = 1.0;
	}
	
	// Update is called once per frame
	void Update () 
	{
        SymplecticTimestep();
	}

	void FindAllPlanets()
	{
		GameObject[] planetArray = GameObject.FindGameObjectsWithTag ("Planet");

		for(int i = 0; i < planetArray.Length; i++)
		{
			allPlanets.Add(planetArray[i].GetComponent<Planet>() );
		}
	}

    void SampleStart()
    {
        double[] r1 = new double[3];
        double[] r2 = new double[3];
        double[] v1 = new double[3];
        double[] v2 = new double[3];

        r1[0] = 1.0;
        r1[1] = 1.0;
        r1[2] = 0.0;
        r2[0] = -1.0;
        r2[1] = 1.0;
        r2[2] = 0.0;
        
        v1[0] = 0.0;
        v1[1] = 0.5;
        v1[2] = 0.0;
        v2[0] = 0.0;
        v2[1] = -0.5;
        v2[2] = 0.0;

        Planet.MakeAPlanet(1.0, r1, v1);
        Planet.MakeAPlanet(1.0, r2, v2);
    }

    void CalcForces(double[] mass, double[,] pos, double[,] fArr)
    {
        int i,j;
        int n = mass.Length;
        double ir;
        double GMM;
        double f;
        for(i=0; i<n; i++)
        {
            fArr[i,0] = 0.0;
            fArr[i,1] = 0.0;
            fArr[i,2] = 0.0;
        }

        for(i=0; i<n; i++)
            for(j=i+1; j<n; j++)
            {
                ir = System.Math.Sqrt((pos[i,0]-pos[j,0])*(pos[i,0]-pos[j,0])
                                    +(pos[i,1]-pos[j,1])*(pos[i,1]-pos[j,1])
                                    +(pos[i,2]-pos[j,2])*(pos[i,2]-pos[j,2]));
                ir = 1.0/ir;
                GMM = G*mass[i]*mass[j];
                f = GMM * (pos[j,0]-pos[i,0]) * ir*ir*ir;
                fArr[i,0] += f;
                fArr[j,0] -= f;
                f = GMM * (pos[j,1]-pos[i,1]) * ir*ir*ir;
                fArr[i,1] += f;
                fArr[j,1] -= f;
                f = GMM * (pos[j,2]-pos[i,2]) * ir*ir*ir;
                fArr[i,2] += f;
                fArr[j,2] -= f;
            }
    }

    //Update planet 'pos' by dt.
    void EulerTimestep(){

        int i;
        int n = allPlanets.Count;

        double[] m = new double[n];
        double[,] r = new double[n,3];
        double[,] v = new double[n,3];
        double[,] f = new double[n,3];

        for(i=0; i<n; i++)
        {
            m[i] = allPlanets[i].mass;
            r[i,0] = allPlanets[i].pos[0];
            r[i,1] = allPlanets[i].pos[1];
            r[i,2] = allPlanets[i].pos[2];
            v[i,0] = allPlanets[i].vel[0];
            v[i,1] = allPlanets[i].vel[1];
            v[i,2] = allPlanets[i].vel[2];
        }

        CalcForces(m, r, f);
        
        for(i=0; i<n; i++){
            allPlanets[i].pos[0] += dt*v[i,0];
            allPlanets[i].pos[1] += dt*v[i,1];
            allPlanets[i].pos[2] += dt*v[i,2];
            allPlanets[i].vel[0] += dt*f[i,0]/m[i];
            allPlanets[i].vel[1] += dt*f[i,1]/m[i];
            allPlanets[i].vel[2] += dt*f[i,2]/m[i];
        }
    }
   
    //4th Order Symplectic Time Integrator
    void SymplecticTimestep(){

        int i;
        int n = allPlanets.Count;

        double c;
        double c2 = System.Math.Pow(2.0,1.0/3.0);
        double[] m = new double[n];
        double[,] q = new double[n,3];
        double[,] p = new double[n,3];
        double[,] f = new double[n,3];

        for(i=0; i<n; i++)
        {
            m[i] = allPlanets[i].mass;
            q[i,0] = allPlanets[i].pos[0];
            q[i,1] = allPlanets[i].pos[1];
            q[i,2] = allPlanets[i].pos[2];
            p[i,0] = m[i]*allPlanets[i].vel[0];
            p[i,1] = m[i]*allPlanets[i].vel[1];
            p[i,2] = m[i]*allPlanets[i].vel[2];
        }

        for(i=0; i<n; i++)
        {
            c = 1.0/(2.0*(2.0-c2)*m[i]);
            q[i,0] += dt * p[i,0] * c;
            q[i,1] += dt * p[i,1] * c;
            q[i,2] += dt * p[i,2] * c;
        }
        CalcForces(m, q, f);
        
        for(i=0; i<n; i++)
        {
            c = 1.0/(2.0-c2);
            p[i,0] += dt * f[i,0] * c;
            p[i,1] += dt * f[i,1] * c;
            p[i,2] += dt * f[i,2] * c;
            c = (1.0-c2)/(2.0*(2.0-c2)*m[i]);
            q[i,0] += dt * p[i,0] * c;
            q[i,1] += dt * p[i,1] * c;
            q[i,2] += dt * p[i,2] * c;
        }
        CalcForces(m, q, f);

        for(i=0; i<n; i++)
        {
            c = -c2/(2.0-c2);
            p[i,0] += dt * f[i,0] * c;
            p[i,1] += dt * f[i,1] * c;
            p[i,2] += dt * f[i,2] * c;
            c = (1.0-c2)/(2.0*(2.0-c2)*m[i]);
            q[i,0] += dt * p[i,0] * c;
            q[i,1] += dt * p[i,1] * c;
            q[i,2] += dt * p[i,2] * c;
        }
        CalcForces(m, q, f);
        
        for(i=0; i<n; i++)
        {
            c = 1.0/(2.0-c2);
            p[i,0] += dt * f[i,0] * c;
            p[i,1] += dt * f[i,1] * c;
            p[i,2] += dt * f[i,2] * c;
            c = 1.0/(2.0*(2.0-c2)*m[i]);
            q[i,0] += dt * p[i,0] * c;
            q[i,1] += dt * p[i,1] * c;
            q[i,2] += dt * p[i,2] * c;
        }

        for(i=0; i<n; i++){
            allPlanets[i].pos[0] = q[i,0];
            allPlanets[i].pos[1] = q[i,1];
            allPlanets[i].pos[2] = q[i,2];
            allPlanets[i].vel[0] = p[i,0]/m[i];
            allPlanets[i].vel[1] = p[i,1]/m[i];
            allPlanets[i].vel[2] = p[i,2]/m[i];
        }
    }
}
