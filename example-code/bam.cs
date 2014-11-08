using UnityEngine;
using System.Collections;

// super-nasty n-body 

public class bam : MonoBehaviour {

	GameObject[] pls; 
	float G = 0.001f;
	float dt = 0.01f;

	// Use this for initialization
	void Start () {	
		Debug.Log ("bam!");
		pls = new GameObject [10];
		for (int i = 0; i < 10; i++) {	
			pls [i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			pls [i].AddComponent<Rigidbody> ();	
			pls [i].rigidbody.useGravity = false;
			pls [i].transform.position = Random.insideUnitSphere; // new Vector3 (0, 0, i);
			pls [i].rigidbody.velocity = Vector3.zero; // Random.insideUnitSphere/10.0f;
			pls[i].rigidbody.mass = 100.0f;
		}
	}

	Vector3 newton(GameObject p, GameObject q) { // force on p due to q
		Vector3 rp, rq;
		rp = p.transform.position;
		rq = q.transform.position;
		return  -G *
				p.rigidbody.mass *	
				q.rigidbody.mass *
				1/Mathf.Pow(Vector3.Dot ((rp-rq),(rp-rq)),1.5f) * (rp-rq);
	}

	// Update is called once per frame
	void Update () {
		Vector3 F;
		foreach ( GameObject p in pls ) {
			foreach ( GameObject q in pls) {
				F = Vector3.zero;
				if (q != p) {
					F = F + newton(p,q);
			}
			p.transform.position = p.transform.position + p.rigidbody.velocity * dt + F / p.rigidbody.mass * dt*dt/2.0f;
		}
	}
}
}
