using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
	public GameObject target = null;
	public float rotationSpeed = 180f;

	private float radius = 0f;
	private float currentAngle = 0f;

    void Start()
    {
		Vector3 T = target.transform.position;
		Vector3 S = transform.position;
		Vector3 TS = S - T;
		radius = TS.magnitude;
		//currentAngle = Vector2.SignedAngle(Vector2.right, TS) * Mathf.Deg2Rad;
		currentAngle = Mathf.Atan2(TS.y, TS.x);

	}

	void Update()
    {
		currentAngle += rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
		Vector3 TS = new Vector3( Mathf.Cos(currentAngle), Mathf.Sin(currentAngle), 0f) * radius;
		Vector3 T = target.transform.position;
		Vector3 S = T + TS;
		transform.position = S;

		transform.rotation = Quaternion.Euler(0, 0, currentAngle * Mathf.Rad2Deg - 90f);
	}
}
