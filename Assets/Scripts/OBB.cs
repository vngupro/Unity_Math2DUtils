using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBB : MonoBehaviour
{
	public float width = 1.0f;
	public float height = 1.0f;
	public float angle = 0.0f;

	// Update is called once per frame
	void Update()
    {
		float theta = angle * Mathf.Deg2Rad;

		Vector2 right = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
		Vector2 up = Vector2.Perpendicular(right);
		// Vector2 H = new Vector2(Mathf.Cos(theta + Mathf.PI / 2f), Mathf.Sin(theta + Mathf.PI / 2f));
		// Vector2 H = new Vector2(-right.y, right.x);

		float halfWidth = width / 2.0f;
		float halfHeight = height / 2.0f;

		Vector2 M = transform.position; // Middle
		Vector2[] points = new Vector2[4];
		points[0] = M - right * halfWidth - up * halfHeight; // A : Bottom Left		
		points[1] = M + right * halfWidth - up * halfHeight; // B : Bottom Right											 
		points[2] = M + right * halfWidth + up * halfHeight; // C : Top Right											 
		points[3] = M - right * halfWidth + up * halfHeight; // D : Top Left

		if (Input.GetMouseButtonDown(0))
		{
			Vector3 P = Camera.main.ScreenToWorldPoint(new Vector3(
				Input.mousePosition.x,
				Input.mousePosition.y,
				-Camera.main.transform.position.z
			));
			bool isIncluded = true;
			for (int i = 0; i < 4; i++)
			{
				if(!IsIncludedInHalfPlanCCW(points[i], points[(i + 1) % 4], P))
				{
					isIncluded = false;
					break;
				}
			}

			if (isIncluded)
			{
				Debug.Log("Hit!");
			}
		}

		for (int i = 0; i < 4; i++) {
			Debug.DrawLine(points[i], points[(i + 1) % 4], Color.green);
		}
	}

	bool IsIncludedInHalfPlanCCW(Vector2 A, Vector2 B, Vector2 P)
	{
		Vector2 N = Vector2.Perpendicular(B - A);
		Vector2 AP = P - A;
		return Vector2.Dot(AP, N) > 0;
	}
}
