using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetSamples : MonoBehaviour
{
    public static void TwoBodyStart()
    {
        PlanetManager.G = 1.0;
        PlanetManager.dt = 0.1;
        PlanetManager.lscale = 1.0;
        PlanetManager.vscale = 1.0;
        PlanetManager.mscale = 1.0;

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

        Planet.MakeAPlanet(1.0, r1, v1, "", 1.0);
        Planet.MakeAPlanet(1.0, r2, v2, "", 1.0);
    }


    public static void TheSolarSystem(bool plutosrevenge)
    {
        PlanetManager.G = 6.673e-11;
        PlanetManager.dt = 3.0e4;
        PlanetManager.lscale = 4.0e10;
        PlanetManager.vscale = 1.0e4;
        PlanetManager.mscale = 1.0e25;

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

        double phi;

        string tex;

        tex = "yellow_dwarf";
        createPlanet(MSun, 0.0, 1.0, 0.0, 0.0, 0.0, tex, 1.0);
        phi = 2*System.Math.PI * rand.NextDouble();
        tex = "dead2p";
        createPlanet(MMer, aMer, TMer, eMer, iMer, phi, tex, 0.2);
        phi = 2*System.Math.PI * rand.NextDouble();
        tex = "blue_giant_real";
        createPlanet(MVen, aVen, TVen, eVen, iVen, phi, tex, 0.4);
        phi = 2*System.Math.PI * rand.NextDouble();
        tex = "new-itokawa-mosaic";
        createPlanet(MEar, aEar, TEar, eEar, iEar, phi, tex, 0.5);
        phi = 2*System.Math.PI * rand.NextDouble();
        tex = "red_dwarf";
        createPlanet(MMar, aMar, TMar, eMar, iMar, phi, tex, 0.6);
        phi = 2*System.Math.PI * rand.NextDouble();
        tex = "dotted2p";
        createPlanet(MJup, aJup, TJup, eJup, iJup, phi, tex, 0.9);
        phi = 2*System.Math.PI * rand.NextDouble();
        tex = "moon_real";
        createPlanet(MSat, aSat, TSat, eSat, iSat, phi, tex, 0.8);
        phi = 2*System.Math.PI * rand.NextDouble();
        tex = "blue_giant_color";
        createPlanet(MUra, aUra, TUra, eUra, iUra, phi, tex, 0.8);
        phi = 2*System.Math.PI * rand.NextDouble();
        tex = "bluegas2p";
        createPlanet(MNep, aNep, TNep, eNep, iNep, phi, tex, 0.8);

        if(plutosrevenge)
        {
            phi = 2*System.Math.PI * rand.NextDouble();
            double[] r = new double[3];
            double[] v = new double[3];
            r[0] = 2*aNep; r[1] = 0.0; r[2] = 0.0;
            v[0] = -2.0e5; v[1] = 0.0; v[2] = 0.0;
            tex = "yellow_dwarf";
            Planet.MakeAPlanet(5.0e29, r, v, tex, 2.0);
        }

        GameObject o = new GameObject("Neil");
        o.AddComponent<Camera>();
        Camera cam = o.GetComponent<Camera>();
        cam.transform.position = new Vector3((float)0.0, (float) (100.0), (float)0.0);
        cam.transform.LookAt(new Vector3(0.0f,0.0f,0.0f));


    }

    static void createPlanet(double mass, double a, double T, double e, double i, double phi, string tex, double radius)
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

        Planet.MakeAPlanet(mass, r, v, tex, radius);
    }
}
