using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSamples : MonoBehaviour
{
    public static void TheSolarSystem()
    {
        double deg2rad = System.Math.PI/180.0;

        double MSun = 1.989e30;

        double MMer = 3.3022e23; //kilograms
        double aMer = 5.7909050e10; //meters
        double TMer = 87.969 * 8.64e5; //seconds
        double eMer = 0.205630;
        double iMer = 6.34 * deg2rad; //degrees

        double rp, vp;
        double[] r = new double[3];
        double[] v = new double[3];
        double phi;
        System.Random rand = new System.Random();

        r[0] = 0.0; r[1] = 0.0; r[2] = 0.0;
        v[0] = 0.0; v[1] = 0.0; v[2] = 0.0;
        
        Planet.MakeAPlanet(MSun, r, v);

        phi = 2*System.Math.PI * rand.NextDouble();
        rp = aMer*(1-eMer);
        vp = 2*System.Math.PI*aMer/TMer * System.Math.Sqrt((1+eMer)/(1-eMer));

        r[2] =  rp * System.Math.Cos(phi);
        r[0] =  rp * System.Math.Sin(phi);
        v[2] = -vp * System.Math.Sin(phi);
        v[0] =  vp * System.Math.Cos(phi);

        Planet.MakeAPlanet(MMer, r, v);

        GameObject o = new GameObject("Neil");
        o.AddComponent<Camera>();
        Camera cam = o.GetComponent<Camera>();
        cam.transform.position = new Vector3((float)0.0, (float) (2.0*aMer), (float)0.0);
        cam.transform.LookAt(new Vector3(0.0f,0.0f,0.0f));


    }
}
