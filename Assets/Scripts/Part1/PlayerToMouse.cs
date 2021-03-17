using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToMouse : MonoBehaviour
{
    public float speed = 2.0f;
    private Vector3 targetMousePos;
    private bool follow = false;

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x, 
                Input.mousePosition.y, 
                -Camera.main.transform.position.z));
            targetMousePos = worldMousePos;
            follow = true;
        }

        if (follow)
        {
            Vector3 vectPlayerToMouse = targetMousePos - transform.position;
            transform.position = transform.position + vectPlayerToMouse.normalized * speed * Time.deltaTime;
            //if(Mathf.Abs(transform.position.x) >= Mathf.Abs(targetMousePos.x) 
            //    && Mathf.Abs(transform.position.y) >= Mathf.Abs(transform.position.x))
            //{
            //    follow = false;
            //    transform.position = targetMousePos;
            //}
        }

    }
}
