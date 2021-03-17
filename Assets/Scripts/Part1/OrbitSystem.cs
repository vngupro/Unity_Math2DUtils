using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSystem : MonoBehaviour
{
    public GameObject fixObject;
    public GameObject satellite;

    public Vector3 startSatelite = new Vector3(1, 1, 0);

    public float rotationSpeed = 1.0f;
    public float rotationOffset = -45.0f;
    private float newAngle;
    private float radius;
    
    private void Start()
    {
        satellite.transform.position = fixObject.transform.position + startSatelite;

        Vector3 startFixPos = fixObject.transform.position;
        Vector3 startSatellitePos = satellite.transform.position;
        Vector3 vectFixSatellite = startSatellitePos - startFixPos;
        newAngle = Mathf.Atan2(vectFixSatellite.y, vectFixSatellite.x);
        radius = vectFixSatellite.magnitude;

    }
    private void Update()
    {
        //orbiting around
        newAngle += rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
        satellite.transform.position = fixObject.transform.position + new Vector3(Mathf.Cos(newAngle), Mathf.Sin(newAngle), 0) * radius;

        //rotation 
        satellite.transform.rotation = Quaternion.Euler(0, 0, newAngle * Mathf.Rad2Deg + rotationOffset + 90f);
    }
}
