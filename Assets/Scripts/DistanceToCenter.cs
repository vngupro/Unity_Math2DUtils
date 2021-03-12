using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceToCenter : MonoBehaviour
{
    public GameObject ball;
    public GameObject plan;

    private void Update()
    {
        Vector3 ballPos = ball.transform.position;
        Vector3 planPos = plan.transform.position;
        Vector3 vectPlanToBall = ballPos - planPos;

        //float distPlanToBall = Vector3.Distance(planPos, ballPos);
        //Debug.Log(distPlanToBall);

        Vector3 projection = Vector3.Project(ballPos, plan.transform.up);
        float angle = Mathf.Atan2(vectPlanToBall.y, vectPlanToBall.x);
        float distBallToPlan = vectPlanToBall.magnitude * Mathf.Sin(angle);
        Debug.Log(distBallToPlan);
    }
}
