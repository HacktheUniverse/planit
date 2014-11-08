﻿using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    public double mass;
    public double[] pos;
    public double[] vel;
    public double scale;
    public bool exists = false;

	// Use this for initialization
	public void Start () {
        Debug.Log("Starting Planet");
        if(!exists)
        {
            Debug.Log("Really Starting Planet");
            mass = 1.0;
            pos = new double[3];
            vel = new double[3];
            pos[0] = this.transform.position.x;
            pos[1] = this.transform.position.y;
            pos[2] = this.transform.position.z;
            vel[0] = 0.0;
            vel[1] = 0.0;
            vel[2] = 0.0;
            scale = 1.0;
            exists = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    this.transform.position = new Vector3((float)(pos[0]/scale), 
                                            (float)(pos[1]/scale), 
                                            (float)(pos[2]/scale));
	}

    public static void MakeAPlanet(double m, double[] r, double[] v)
    {
        MakeAPlanet(m, r, v, 1.0);
    }
    public static void MakeAPlanet(double m, double[] r, double[] v, double scale)
    {
        GameObject o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        o.transform.position = new Vector3((float)r[0], (float)r[1], (float)r[2]);
        o.tag = "Planet";
        o.AddComponent<Planet>();
        Planet p = o.GetComponent<Planet>();
        p.Start();
        p.mass = m;
        p.pos[0] = r[0];
        p.pos[1] = r[1];
        p.pos[2] = r[2];
        p.vel[0] = v[0];
        p.vel[1] = v[1];
        p.vel[2] = v[2];
        p.scale = scale;
    }
}
