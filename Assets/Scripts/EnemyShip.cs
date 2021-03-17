using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    /* 
     * how does this ship moves ?
     * let's take a random position inside the view of the camera
     * camHeight
     * camWdith
     * Vector3 location = new Vector3(Random.range(0, camWidth), Random.range(0, camHeight), transform.position.z);
     * ok you got the location now you need
     * the speed
     * the acceleration
     * the deceleration
     * 
     * float minSpeed;
     * float maxSpeed;
     * Vector3 speed;
     * Vector3 acceleration;
     * Vector3 decceleration;
     * 
     * AnimationCurve speedCurve;
     * AnimationCurve accelerationCurve;
     * AnimationCurve DeccelerationCurve;
     * 
     * you can take acceleration and deceleration as constante and juste curve speed, it work the same
     * but not that fun
     * 
     * how can you add acceleration to speed 
     * and have an animation Curve ?
     * 
     * speed += a * Time.fixedDeltaTime;
     * 
     * newPos = actualPos + new Vector3 (dir.x * newSpeed.x, dir.y * newSpeed.y, transform.z);
     * 
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
}
