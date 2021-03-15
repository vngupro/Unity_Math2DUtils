using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDynamicMovement : MonoBehaviour
{
    public float speed = 1.0f;
    public float masse = 10.0f;
    private Vector3 vitesse = Vector3.right;
    private Vector3 acceleration;

    private void Start()
    {
        vitesse *= speed;
    }
    private void Update()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                -Camera.main.transform.position.z));
        Vector3 targetPos = worldMousePos;
        Vector3 vectPlayerToMouse = targetPos - transform.position;
        if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = transform.position + vitesse * Time.deltaTime;
            vitesse += acceleration * Time.fixedDeltaTime;
            transform.position = newPosition;
            
        }

        if (Input.GetMouseButtonUp(0))
        {

        }
            

        //Vector3 newPosition = transform.position + vitesse * Time.fixedDeltaTime + acceleration * Mathf.Pow(Time.fixedDeltaTime, 2) / 2;
        //vitesse += acceleration * Time.fixedDeltaTime;
        //transform.position = newPosition;
    }
}
