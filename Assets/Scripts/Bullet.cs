using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector3 interPos;
    public float tolerance = 1f;

    void FixedUpdate()
    {
        Vector3 pos = transform.position;
        Vector3 vBulletToTarget = interPos - pos;
        Vector3 dir = vBulletToTarget.normalized;
        Vector3 newPos = pos + dir * speed * Time.fixedDeltaTime;
        transform.position = newPos;

        float mBulletToTarget = vBulletToTarget.magnitude;
        if(Mathf.Abs(mBulletToTarget) <= tolerance)
        {
            transform.position = interPos;
            DestroyBullet();
        }

        Camera cam = Camera.main;
        Vector3 camPos = cam.transform.position;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float halfHeight = camHeight / 2f;
        float halfWidth = camWidth / 2f;
       
        if (pos.y > pos.y + halfHeight ||
            pos.y < pos.y - halfHeight ||
            pos.x > pos.x + halfWidth  ||
            pos.x < pos.x - halfWidth)
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        //calculIntersection.cs
        //enemyShip.cs
        GameEvents.onHit.Invoke();
        Destroy(this.gameObject);
    }
}
