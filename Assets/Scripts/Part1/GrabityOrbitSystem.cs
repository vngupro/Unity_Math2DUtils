using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabityOrbitSystem : MonoBehaviour
{
    public GameObject planet;
    public GameObject satellite;

    public float gravity = 9.81f;
    public float speed = 5.0f;
    private Vector3 vitesse = Vector3.right;
    private Vector3 acceleration;

    private void Start()
    {
        vitesse *= speed;
    }
    private void FixedUpdate()
    {
        Vector3 planetPos = planet.transform.position;
        Vector3 satellitePos = satellite.transform.position;
        Vector3 vectSatelliteToPlanet = planetPos - satellitePos;
        float radius = vectSatelliteToPlanet.magnitude;

        //acceleration = vectSatelliteToPlanet.normalized * gravity / Mathf.Pow(radius, 2);
        //Vector3 newPosition = satellitePos + vitesse * Time.fixedDeltaTime;
        Vector3 newPosition = satellitePos + vitesse * Time.fixedDeltaTime + acceleration * Mathf.Pow(Time.fixedDeltaTime,2)/ 2;
        vitesse += acceleration * Time.fixedDeltaTime;
        satellite.transform.position = newPosition;
        acceleration = vectSatelliteToPlanet.normalized * gravity / Mathf.Pow(radius, 2);

        Debug.DrawLine(satellitePos, newPosition, Color.red, 99.0f);
    }
}
