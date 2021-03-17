using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AABB : MonoBehaviour
{
	public float width = 1.0f;
	public float height = 1.0f;

	void Update()
    {
		Vector3 M = transform.position; // Middle
		float xMin = M.x - width / 2.0f;
		float xMax = M.x + width / 2.0f;
		float yMin = M.y - height / 2.0f;
		float yMax = M.y + height / 2.0f;

		if (Input.GetMouseButtonDown(0))
		{
			Vector3 P = Camera.main.ScreenToWorldPoint(new Vector3(
				Input.mousePosition.x,
				Input.mousePosition.y,
				-Camera.main.transform.position.z
			));
			bool isIncluded = (P.x > xMin) && (P.x < xMax) && (P.y > yMin) && (P.y < yMax);
			if (isIncluded)
			{
				Debug.Log("Hit!");
			}
		}

		Vector3[] points = new Vector3[4];
		points[0] = new Vector3(xMin, yMin); // A : Bottom Left		
		points[1] = new Vector3(xMin, yMax); // B : Bottom Right											 
		points[2] = new Vector3(xMax, yMax); // C : Top Right											 
		points[3] = new Vector3(xMax, yMin); // D : Top Left

		for (int i = 0; i < 4; i++)
		{
			Debug.DrawLine(points[i], points[(i + 1) % 4], Color.green);
		}
	}
}
