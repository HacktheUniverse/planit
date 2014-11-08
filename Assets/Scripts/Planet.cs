using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    public double mass;
    public double[] pos;
    public double[] vel;

	// Use this for initialization
	void Start () {
        mass = 1.0;
        pos = new double[3];
        vel = new double[3];
        pos[0] = this.transform.position.x;
        pos[1] = this.transform.position.y;
        pos[2] = this.transform.position.z;
        vel[0] = 0.0;
        vel[1] = 0.0;
        vel[2] = 0.0;
	}
	
	// Update is called once per frame
	void Update () {
	    this.transform.position = new Vector3((float)pos[0], (float)pos[1], (float)pos[2]);
	}
}
