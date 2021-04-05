using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMortar4 : MonoBehaviour
{
    public GameObject target;
    public float speedMagnitude = 10.0f;
    public float tolerance = 0.3f;

    [Header("Debug")]
    [SerializeField] private Vector2 speed;

    private bool canLaunch = false;

    private void Start()
    {
        float gravity = 9.81f;
        Vector2 acceleration = new Vector2(0, -gravity);

        Vector2 targetPos = target.transform.position;
        Vector2 bulletPos = transform.position;
        Vector2 bullet2Target = targetPos - bulletPos;
        float distance2Target = bullet2Target.magnitude;

        //Am i losing data ????
        float differenceY = bullet2Target.y;
        float theta = Mathf.Atan(distance2Target / differenceY);
        float a = (-acceleration.y * Mathf.Pow(distance2Target, 2) ) / Mathf.Pow(speedMagnitude, 2);
        float b = Mathf.Pow(differenceY, 2) + Mathf.Pow(distance2Target, 2);
        float c = (a - differenceY) / Mathf.Sqrt(b);
        
        if ( c <= 1.0f &&  c >= -1.0f)
        {
            float angle = (Mathf.Acos(c) + theta) / 2;

            Debug.Log("Angle = " + angle * Mathf.Rad2Deg);

            speed.x = speedMagnitude * Mathf.Cos(angle);
            speed.y = speedMagnitude * Mathf.Sin(angle);
            canLaunch = true;
        }
        else
        {
            Debug.Log("Not enough Speed or Target too far");
        }
    }

    private void FixedUpdate()
    {
        if (!canLaunch) { return; }

        Vector2 posBullet = transform.position;
        float deltaTime = Time.fixedDeltaTime;
        Vector2 acceleration = new Vector2(0, -9.81f);

        speed += acceleration * deltaTime;
        Vector2 newPos = posBullet + speed * deltaTime + (acceleration * Mathf.Pow(deltaTime, 2)) / 2;

        Debug.DrawLine(transform.position, newPos, Color.red, 99f);

        transform.position = newPos;

        //Destroy all game objects on collision
        Vector2 posTarget = target.transform.position;
        Vector2 vBullet2Target = posTarget - posBullet;
        float mLauncher2Target = vBullet2Target.magnitude;
        if (mLauncher2Target <= tolerance)
        {
            Destroy(target.gameObject);
            Destroy(this.gameObject);
        }
    }
}
