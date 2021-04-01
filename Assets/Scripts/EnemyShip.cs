using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    /*
     * and clamp speed
     * speed.MagnetizedClamp(minSpeed, maxSpeed);
     * 
     * ok how do you decelerate in a natural way.
     * speed -= a *Time.fixedDeltatime
     * 
     * One problem -> it's easier to have constant speed 
     * so start with constant speed and when with acceleration and deceleration (like he wants to dodge)
     * 
     */

    public float minSpeed = 1.0f;
    public float maxSpeed = 10.0f;
    public Vector3 speed = new Vector3 (1, 1, 0);                       //if x is different from y the mouvement get curvy
    public Vector3 acceleration;
    public Vector3 decceleration;

    public AnimationCurve speedCurve;
    public AnimationCurve accelerationCurve;
    public AnimationCurve deccelerationCurve;

    public float delayBeforeChangeLocation = 3.0f;
    public float distMargin = 0.1f;

    public float angle = 0f;
    public Vector3 target;

    public Vector3 interPos;
    private bool isGettingNewLocation = false;
    private void Start()
    {
        Camera cam = Camera.main;
        Vector3 camPos = cam.transform.position;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float halfHeight = camHeight / 2f;
        float halfWidth = camWidth / 2f;

        target = new Vector3(Random.Range(camPos.x - halfWidth, camPos.x + halfWidth), 
                             Random.Range(camPos.y - halfHeight, camPos.y + halfHeight), 
                             transform.position.z);
    }


    private void Awake()
    {
        //bullet.cs
        GameEvents.onHit.AddListener(GotHit);       
    }
    private void FixedUpdate()
    {
        Vector3 shipPos = transform.position;
        Vector3 vShipToTarget = target - shipPos;
        Vector3 dirShipToTarget = vShipToTarget.normalized;
        float mShipToTarget = vShipToTarget.magnitude;
        Vector3 newPos = shipPos + new Vector3(dirShipToTarget.x * speed.x, 
                                               dirShipToTarget.y * speed.y, 
                                               transform.position.z) * Time.fixedDeltaTime;
        transform.position = newPos;

        Vector3 forward = transform.position + Vector3.up;
        Vector3 vForwardToTarget = target - forward;
        Vector3 dirForwardToTarget = vForwardToTarget.normalized;
        angle = Mathf.Atan2(dirForwardToTarget.y, dirForwardToTarget.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (mShipToTarget <= distMargin)
        {
            transform.position = target;
        }

        if (shipPos == target && !isGettingNewLocation)
        {
            StartCoroutine(GetNewPosition());
        }

        Debug.DrawLine(transform.position, target, Color.green, Time.fixedDeltaTime);        
        Debug.DrawLine(transform.position, target, Color.red, Time.fixedDeltaTime);
    }

    IEnumerator GetNewPosition()
    {
        Camera cam = Camera.main;
        Vector3 camPos = cam.transform.position;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float halfHeight = camHeight / 2f;
        float halfWidth = camWidth / 2f;
        isGettingNewLocation = true;
        yield return new WaitForSeconds(delayBeforeChangeLocation);
        target = new Vector3(Random.Range(camPos.x - halfWidth, camPos.x + halfWidth),
                       Random.Range(camPos.y - halfHeight, camPos.y + halfHeight),
                       transform.position.z);
        isGettingNewLocation = false;
    }

    public void GotHit()
    {
        Vector3 pos = transform.position;
        if (Mathf.Abs((pos - interPos).magnitude) <= 0.1f) 
        {
            GameEvents.createShip.Invoke();
            DestroyShip();
        }
    }

    public void DestroyShip()
    {
        Destroy(this.gameObject);
    }
}
