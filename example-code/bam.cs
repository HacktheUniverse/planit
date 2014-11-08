using UnityEngine;
using System.Collections;

/*
 * super-cheap n-body
 * 
 * waltp
 */

public class bam : MonoBehaviour {


	const int n = 100;
	GameObject[] pls; 
	const float G = 0.01f; // physical parameters are made up to give interesting results
	const float dt = 0.1f;
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

	float NFW(float r) { // NFW dark matter profile
	/*	M(r) for dark matter density profile in
	 * Navarro, Julio F.; Frenk, Carlos S.; White, Simon D. M. (May 10, 1996).
	 * "The Structure of Cold Dark Matter Halos". The Astrophysical Journal 463: 563.
	 * arXiv:astro-ph/9508025
	 * Also see http://en.wikipedia.org/wiki/Navarro%E2%80%93Frenk%E2%80%93White_profile
	 */
		const float c = 1.0f; // parameters chosen arbitrarily!!!
		const float rs = 100.0f;
		return c * (Mathf.Log ((rs + r) / rs) - r / (rs + r)); // total dark matter mass with radius r of origin
	}

	double Einasto(float r) { // Einasto dark matter profile
		/* M(r) for dark matter density profile in
		 * Merritt, David; Graham, Alister; et al. (2006). 
		 * "Empirical Models for Dark Matter Halos". The Astronomical Journal 132 (6): 2685–2700.
		 * arXiv:astro-ph/0509417. Bibcode:2006AJ....132.2685M. doi:10.1086/508988
		 */
		// This M(r) is given in terms of gamma functions, which Unity lacks.
		return 0.0f;
	}
 
	// Update is called once per frame
	void Update () {
		float m;
		Vector3 o;
		float r;
		// calculate net force on each body
		for (int i = 0; i < n; i++) {
			F [i] = Vector3.zero;
			for (int j = 0; j < n; j++) {
				if (i != j) {
					F [i] = F [i] + newton (pls [i], pls [j]);
					o=pls[i].transform.position;
					r = o.magnitude;
					Vector3.Normalize(o);
					F [i] = F [i] - G * o * pls[i].rigidbody.mass * NFW(r)/(r*r); // just pull it to the origin, a point mass of mass M(r)
				}
			}
		}
		// update velocities and positions for each body
		for (int i = 0; i < n; i++) { // the worst integrator possible
			m = pls[i].rigidbody.mass;
			pls[i].rigidbody.velocity = pls[i].rigidbody.velocity + F[i]/m*dt;
			pls[i].transform.position = pls[i].transform.position + pls[i].rigidbody.velocity * dt + F[i] / m*dt*dt/2.0f;
		}
	}
}

