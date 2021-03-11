using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillationPlateformScript : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    [Header("Lerp 1")]
    public float animSpeed = 0.01f;
    private float t = 0;

    [Header("Lerp 2")]
    public int animTimeInSeconds = 3;
    private int interpolationFramesCount;
    //public int interpolationFramesCount = 60; // Number of frames to completely interpolate between the 2 positions
    private int elapsedFrames = 0;

    [Header("Lerp 3")]
    public float speed = 1.0f;          // Movement speed in units per second.
    private float startTime;            // Time when the movement started.
    private float journeyLength;        // Total distance between the markers.
    private int count = 0;

    [Header("Lerp 4")]
    public float animationSpeed = 5.0f;
    private int totalFrameCount = 0;

    [Header("Lerp 5")]
    public float lerp5Speed = 3.0f;
    private float animTotalTime;

    [Header("Lerp 6")]
    public float lerp6Speed = 0.01f;
    private float lerp6t;

    [Header("Lerp 7")]
    public float lerp7speed = 1.0f;
    private float lerp7t;
    private Transform minBound;
    private Transform maxBound;

    private bool reverse = false;

    private void Start()
    {
        transform.position = startPoint.position;

        //Lerp 2
        //interpolationFramesCount = 60 * animTimeInSeconds;

        //Lerp 3
        // Keep a note of the time the movement started.
        startTime = Time.time;
        // Calculate the journey length.
        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);

        //Lerp 5
        //Calculate Animation Total Time
        animTotalTime = lerp5Speed * (float)Vector2.Distance(startPoint.position, endPoint.position);

        //Lerp 7
        minBound = startPoint;
        maxBound = endPoint;
    }
    private void Update()
    {
        //Lerp 1 count by time OK
        //if(!reverse)
        //{
        //    t += animSpeed;
        //}
        //else
        //{
        //    t -= animSpeed;
        //}
        //transform.position = Vector2.Lerp(startPoint.position, endPoint.position, t);
        //if(t >= 1.0f)
        //{
        //    reverse = true;
        //}else if(t <= .0f)
        //{
        //    reverse = false;
        //}

        //Lerp 2 not good
        //float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
        //transform.position = Vector2.Lerp(startPoint.position, endPoint.position, interpolationRatio * Time.time);
        //if((elapsedFrames + 1) % (interpolationFramesCount + 1))
        //{

        //}
        //elapsedFrames = (elapsedFrames + 1) % (interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)

        //Lerp 3 count by distance OK
        //// Distance moved equals elapsed time times speed..
        //float distCovered = 0;
        ////one way
        ////distCovered = (Time.time - startTime) * speed;
        //if (!reverse)
        //{
        //    count++;
        //}
        //else
        //{
        //    count--;
        //}
        ////d = t * v
        //distCovered = (Time.fixedDeltaTime * count) * speed;
        //// Fraction of journey completed equals current distance divided by total distance.
        //float fractionOfJourney = distCovered / journeyLength;
        //// Set our position as a fraction of the distance between the markers.
        //transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionOfJourney);
        ////To reverse, you can switch or reverse fractionOfJourney by changing distCovered you cannot pass by Time.time
        //if (fractionOfJourney >= 1)
        //{
        //    reverse = true;
        //}
        //else if (fractionOfJourney <= 0)
        //{
        //    reverse = false;
        //}

        //Lerp 4 count by frame OK
        //float timeAnim = 0;
        ////first time don't have total frame count 
        //if (totalFrameCount == 0 && !reverse)
        //{
        //    timeAnim = Time.fixedDeltaTime * Time.frameCount;
        //}
        ////non reverse animation
        //else if(!reverse)
        //{
        //    //Time.frameCount % totalFrameCount == 0 on end point
        //    if (Time.frameCount % totalFrameCount == 0)
        //    {
        //        timeAnim = Time.fixedDeltaTime * totalFrameCount;
        //    }
        //    else
        //    {
        //        timeAnim = Time.fixedDeltaTime * (Time.frameCount % totalFrameCount);
        //    }   
        //}
        ////reverse animation
        //else
        //{
        //    // Time.frameCount % totalFrameCount == 0 on start point
        //    if(Time.frameCount % totalFrameCount == 0)
        //    {
        //        timeAnim = 0;
        //    }
        //    else
        //    {
        //        timeAnim = Time.fixedDeltaTime * (totalFrameCount - (Time.frameCount % totalFrameCount));
        //    }
        //}
        //transform.position = Vector2.Lerp(startPoint.position, endPoint.position, timeAnim * animationSpeed);
        //if (transform.position == endPoint.position)
        //{
        //    reverse = true;
        //    if (totalFrameCount == 0)
        //    {
        //        totalFrameCount = Time.frameCount;
        //    }
        //}
        //else if(transform.position == startPoint.position)
        //{
        //    reverse = false;
        //}    

        //Lerp 5 count by animation time (you need to create an animation to get the animation)
        //
        //Lerp 6 Count by switch bound OK
        //lerp6t += lerp6Speed;
        //if (!reverse)
        //{
        //    transform.position = Vector2.Lerp(startPoint.position, endPoint.position, lerp6t);
        //    if (transform.position == endPoint.position)
        //    {
        //        reverse = true;
        //        lerp6t = 0;
        //    }
        //}
        //else
        //{
        //    transform.position = Vector2.Lerp(endPoint.position, startPoint.position, lerp6t);
        //    if (transform.position == startPoint.position)
        //    {
        //        reverse = false;
        //        lerp6t = 0;
        //    }
        //}

        //Lerp 7 Count by switch bound 2 OK
        //lerp7t += lerp7speed;
        //transform.position = Vector2.Lerp(minBound.position, maxBound.position, lerp7t);
        ////reverse
        //if(lerp7T >= 1.0f){
        //  if (transform.position == endPoint.position)
        //{
        //    minBound = endPoint;
        //    maxBound = startPoint;
        //    lerp7t = 0.0f;
        //}
        //else if(transform.position == startPoint.position)
        //{
        //    minBound = startPoint;
        //    maxBound = endPoint;
        //    lerp7t = 0.0f;
        //}
        //}

        //ping pong good if you want to work with Time.time
        //transform.position = new Vector2(Mathf.PingPong(Time.time, endPoint.position.x - startPoint.position.x), Mathf.PingPong(Time.time, endPoint.position.y - startPoint.position.y));
        transform.position = Vector2.Lerp(startPoint.position, endPoint.position, Mathf.PingPong(Time.time, 1));

        
    }
}
