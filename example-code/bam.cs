using UnityEngine;
using System.Collections;

// super-nasty n-body 

public class bam : MonoBehaviour {

	const int n = 20;
	GameObject[] pls; 
	float G = 0.01f;
	float dt = 0.01f;
	Vector3[] F;

	// Use this for initialization
	void Start () {	
		Debug.Log ("bam!");
		pls = new GameObject [n];
		F = new Vector3[n];
		for (int i = 0; i < n; i++) {	
			pls [i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			pls [i].AddComponent<Rigidbody> ();	
			pls [i].rigidbody.useGravity = false;
			pls [i].transform.position = Random.insideUnitSphere*10.0f;
			pls [i].rigidbody.velocity = Vector3.zero; // Random.insideUnitSphere/10.0f;
			pls [i].rigidbody.mass = 100.0f;
		}
	}

	Vector3 newton(GameObject p, GameObject q) { // force on p due to q
		Vector3 rp, rq;
		rp = p.transform.position;
		rq = q.transform.position;
		return  -G *
				p.rigidbody.mass *	
				q.rigidbody.mass *
				1/Mathf.Pow((rp-rq).sqrMagnitude,1.5f) * (rp-rq);
	}

	// Update is called once per frame
	void Update () {
		float m;
		// calculate net force on each body
		for (int i = 0; i < n; i++) {
			F [i] = Vector3.zero;
			for (int j = 0; j < n; j++) {
				if (i != j) {
					F [i] = F [i] + newton (pls [i], pls [j]);
				}
			}
		}
		// update velocities and positions for each body
		for (int i = 0; i < n; i++) {
			m = pls[i].rigidbody.mass;
			pls[i].rigidbody.velocity = pls[i].rigidbody.velocity + F[i]/m*dt;
			pls[i].transform.position = pls[i].transform.position + pls[i].rigidbody.velocity * dt + F[i] / m*dt*dt/2.0f;
		}
	}
}

