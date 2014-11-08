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
        double TMer = 87.969 * 8.64e4; //seconds
        double eMer = 0.205630;
        double iMer = 6.34 * deg2rad; //degrees

        double MVen = 4.8676e24; //kilograms
        double aVen = 1.08208e11; //meters
        double TVen = 224.701 * 8.64e4; //seconds
        double eVen = 0.0067;
        double iVen = 3.39458 * deg2rad; //degrees

        double MEar = 5.97419e24; //kilograms
        double aEar = 1.49598261e11; //meters
        double TEar = 365.256363004 * 8.64e4; //seconds
        double eEar = 0.01671123;
        double iEar = 7.155 * deg2rad; //degrees

        double MMar = 6.4185e23; //kilograms
        double aMar = 2.279391e11; //meters
        double TMar = 779.96 * 8.64e4; //seconds
        double eMar = 0.0934;
        double iMar = 1.85 * deg2rad; //degrees

        double MJup = 1.8986e27; //kilograms
        double aJup = 7.785472e11; //meters
        double TJup = 4332.59 * 8.64e4; //seconds
        double eJup = 0.048775;
        double iJup = 1.305 * deg2rad; //degrees

        double MSat = 5.6846e26; //kilograms
        double aSat = 1.43344937e12; //meters
        double TSat = 10759.22 * 8.64e4; //seconds
        double eSat = 0.055723219;
        double iSat = 2.485240 * deg2rad; //degrees

        double MUra = 8.6810e25; //kilograms
        double aUra = 2.8706714e12; //meters
        double TUra = 3068715 * 8.64e4; //seconds
        double eUra = 0.047220087;
        double iUra = 0.772556 * deg2rad; //degrees

        double MNep = 1.0243e26; //kilograms
        double aNep = 4.4985426e12; //meters
        double TNep = 60190.03 * 8.64e4; //seconds
        double eNep = 0.00867797;
        double iNep = 1.767975 * deg2rad; //degrees

        System.Random rand = new System.Random();

        double scale = 4.0e10;
        double phi;

        createPlanet(MSun, 0.0, 1.0, 0.0, 0.0, 0.0, scale);
        phi = 2*System.Math.PI * rand.NextDouble();
        createPlanet(MMer, aMer, TMer, eMer, iMer, phi, scale);
        phi = 2*System.Math.PI * rand.NextDouble();
        createPlanet(MVen, aVen, TVen, eVen, iVen, phi, scale);
        phi = 2*System.Math.PI * rand.NextDouble();
        createPlanet(MEar, aEar, TEar, eEar, iEar, phi, scale);
        phi = 2*System.Math.PI * rand.NextDouble();
        createPlanet(MMar, aMar, TMar, eMar, iMar, phi, scale);
        phi = 2*System.Math.PI * rand.NextDouble();
        createPlanet(MJup, aJup, TJup, eJup, iJup, phi, scale);
        phi = 2*System.Math.PI * rand.NextDouble();
        createPlanet(MSat, aSat, TSat, eSat, iSat, phi, scale);
        phi = 2*System.Math.PI * rand.NextDouble();
        createPlanet(MUra, aUra, TUra, eUra, iUra, phi, scale);
        phi = 2*System.Math.PI * rand.NextDouble();
        createPlanet(MNep, aNep, TNep, eNep, iNep, phi, scale);

        phi = 2*System.Math.PI * rand.NextDouble();
        double[] r = new double[3];
        double[] v = new double[3];
        r[0] = 2*aNep; r[1] = 0.0; r[2] = 0.0;
        v[0] = -2.0e5; v[1] = 0.0; v[2] = 0.0;
        Planet.MakeAPlanet(5.0e29, r, v, scale);

        GameObject o = new GameObject("Neil");
        o.AddComponent<Camera>();
        Camera cam = o.GetComponent<Camera>();
        cam.transform.position = new Vector3((float)0.0, (float) (100.0), (float)0.0);
        cam.transform.LookAt(new Vector3(0.0f,0.0f,0.0f));


    }

    static void createPlanet(double mass, double a, double T, double e, double i, double phi, double scale)
    {
        double[] r = new double[3];
        double[] v = new double[3];
        r[0] = 0.0; r[1] = 0.0; r[2] = 0.0;
        v[0] = 0.0; v[1] = 0.0; v[2] = 0.0;

        double rp = a*(1-e);
        double vp = (2.0*System.Math.PI*a/T) * System.Math.Sqrt((1.0+e)/(1.0-e));
        r[2] =  rp * System.Math.Cos(phi);
        r[0] =  rp * System.Math.Sin(phi);
        v[2] = -vp * System.Math.Sin(phi);
        v[0] =  vp * System.Math.Cos(phi);

        Planet.MakeAPlanet(mass, r, v, scale);
    }
}
