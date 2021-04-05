using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMortar3 : MonoBehaviour
{
    /*
     * you want to calculate the angle at which the bullet should be launch
     * you got speed but only the magnitude (so you don't know the angle)
     * you know 
     *         acceleration = gravity
     *         target pos
     *         bullet pos
     *         distance between target and bullet
     *         time :
     *              time = d / v
     *         deltaTime = Time.fixedDeltatime
     *         equation horaire :
     *              t = 0 or time or deltaTime :
     *                  a :
     *                      ax = 0;
     *                      ay = -gravity
     *                  v :
     *                      vx = V0x (unknown)
     *                      vy = -gravity * t (known) + V0y (unknown)
     *                  p : 
     *                      x = V0x * t (+ x0)
     *                      y = -gravity/2 * t2 + V0y * t + y0
     *                  
     *                  x0 = pos.x (known)
     *                  y0 = pos.y (known)
     *                  
     *        projection vitesse : 
     *              V0x = V.Magnitude * cos(angle)
     *              V0y = V.Magnitude * sin(angle)
     *              
     *              V.Magnitude (known)
     *       
     *       new Equation : 
     *              v :
     *                  vx = V.Magnitude * cos(angle)
     *                  vy = V.Magnitude * sin(angle) - gravity * t
     *              p :
     *                  x = V.Magnitude * cos(angle) * t + x0
     *                  y = -gravity/2 * t2 + V.Magnityde * sin(angle) * t + y0
     *               
     *      calculate angle :
     *          t = time (final) :
     *                 angle = ACos( (x (- x0)) / (v.Magnitude * t) ;
     *                 
     *                 x - x0 = bullet to target vector magnitude (known)
     *                 
     *     calcule V0x and V0y : 
     *          V0x = V.Magnitude * cos(angle);
     *          V0y = V.Magnitude * sin(angle);
     *          
     *     New Equation (expression of t) :
     *          x = V.Magnitude * cos(angle) * t 
     *          t = x / (v.magnitude * cos(angle);
     *          
     *          y = -gravity/2 * t2 + V.Magnityde * sin(angle) * t + y0
     *          y = -gravity/2 * (x / (v.magnitude * cos(angle))^2 + V.magnitude * sin(angle) * (x / v.magnitude * cos(angle)) + y0
     *          y = -gravity/(2 * v.magnitude^2 * cos(angle)^2) * x^2 + tan(angle) * x
     *          
     *     New equation again
     *          y = 0 & y0 = 0
     *          0 = sin(2 * angle) - (g * x) / V.magnitude^2
     *           
     */

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

        if(((acceleration.y * distance2Target) / (speedMagnitude * speedMagnitude)) <= 1.0f && ((acceleration.y * distance2Target) / (speedMagnitude * speedMagnitude)) >= -1)
        {
            float angle = Mathf.Abs(Mathf.Asin((acceleration.y * distance2Target) / (speedMagnitude * speedMagnitude)) / 2);

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
        if (!canLaunch) { return;  }

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
