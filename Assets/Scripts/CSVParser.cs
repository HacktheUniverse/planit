using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System;


/**
 * There be dragons here. Please don't mind the utter trashing of OO best practices. 
 * 
 */ 
public class CSVParser : MonoBehaviour
{	
	static IList<Exoplanet> exoplanets = new List<Exoplanet> ();
	
	void Start() {
		Debug.Log ("EXOPLANET LOADING..."); 
		parseFile ();
		Debug.Log ("Loaded!");
	}
	
	void parseFile() {
		// Open the file and read it back. 
		using (System.IO.StreamReader sr = System.IO.File.OpenText("Assets/Data/exoplanets.csv"))
 
		{
			string s = "";
			sr.ReadLine();  // we don't care -- headers. 
			while ((s = sr.ReadLine()) != null) 
			{
				string[] split = s.Split(',');
				try {				
					Star star = new Star(split[6],Single.Parse (split[7]),Single.Parse (split[8]),Single.Parse(split[9]),split[10]); 
					Exoplanet ep = new Exoplanet(split[0],star,Single.Parse (split[1]),Single.Parse (split[2]),Single.Parse(split[3]),Single.Parse(split[4]),Single.Parse(split[5]));
					star.addExoPlanet(ep);
					exoplanets.Add(ep);
				}catch(Exception e) { continue; } 
			}
		}
		
	}
}

class Star {
	public string name;
	public StarProperties props;
	public IList<Exoplanet> exoplanets = new List<Exoplanet> ();
	public Star(string name, 
	            float star_distance,
	            float star_mass,
	            float star_radius,
	            string star_sp_type) {
		props = new StarProperties (star_distance, star_mass, star_radius, star_sp_type);
		this.name = name; 
	}
	public void addExoPlanet(Exoplanet ep) { this.exoplanets.Add (ep); }
}

class StarProperties {
	public float star_distance;
	public float star_mass;
	public float star_radius;
	public string star_sp_type;
	
	public StarProperties(float star_distance,
	                      float star_mass,
	                      float star_radius,
	                      string star_sp_type) {
		this.star_distance = star_distance;
		this.star_mass = star_mass;
		this.star_radius = star_radius;
		this.star_sp_type = star_sp_type;
	}
}

class Exoplanet {
	public Star nearbyStar; 
	public string name;
	public OrbitalParameters orbitalParams; 
	
	public Exoplanet(string name, 
	                 Star nearbyStar,
	                 float mass,
	                 float radius,
	                 float orbital_period,
	                 float semi_major_axis,
	                 float eccentricity) {
		this.orbitalParams = new OrbitalParameters (mass, radius, orbital_period, semi_major_axis, eccentricity);
		this.name = name; 
		this.nearbyStar = nearbyStar;
	}
	
}

class OrbitalParameters {
	public float mass;
	public float radius;
	public float orbital_period;
	public float semi_major_axis;
	public float eccentricity;
	
	public OrbitalParameters(float mass,
	                         float radius,
	                         float orbital_period,
	                         float semi_major_axis,
	                         float eccentricity) {
		this.mass = mass; 
		this.radius = radius;
		this.orbital_period = orbital_period;
		this.semi_major_axis = semi_major_axis;
		this.eccentricity = eccentricity;
	}
}

