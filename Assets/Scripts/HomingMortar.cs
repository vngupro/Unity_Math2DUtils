using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMortar : MonoBehaviour
{
    public GameObject target;
    public float speedX = 1.0f;
    public float tolerance = 0.1f;

    [Header("Debug")]
    [SerializeField] private float speedY;
    [SerializeField] private Vector2 speed;
    [SerializeField] private Vector2 acceleration;

    private void Start()
    {
        Vector2 posTarget = target.transform.position;
        Vector2 posBullet = transform.position;
        Vector2 vBullet2Target = posTarget - posBullet;
        float mBullet2Target = vBullet2Target.magnitude;
        float t = mBullet2Target / speedX;
        float gravity = 9.81f;
        acceleration = new Vector2(0, -gravity);
        speedY = -acceleration.y * t / 2;
        speed = new Vector2(speedX, speedY);
    }
    private void FixedUpdate()
    {
        Vector2 posBullet = transform.position;
        float deltaTime = Time.fixedDeltaTime;
        speed += acceleration * deltaTime;
        Vector2 newPos = posBullet + speed * deltaTime + (acceleration * Mathf.Pow(deltaTime, 2)) / 2;

        Debug.DrawLine(transform.position, newPos, Color.red, 99f);

        transform.position = newPos;

        //Destroy all game objects on collision
        Vector2 posTarget = target.transform.position;
        Vector2 vBullet2Target = posTarget - posBullet;
        float mLaucnher2Target = vBullet2Target.magnitude;
        if(mLaucnher2Target <= tolerance)
        {
            Destroy(target.gameObject);
            Destroy(this.gameObject);
        }
    }
}
