using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMortar2 : MonoBehaviour
{
    public GameObject targetPrefab;
    public GameObject bulletPrefab;
    public float speedX = 1.0f;
    public float gravity = 9.81f;
    public float tolerance = 0.2f;
    public float delayBeforeNewTarget = 3.0f;

    [Header("Debug")]
    [SerializeField] private float speedY;
    [SerializeField] private Vector2 speed;
    [SerializeField] private Vector2 acceleration;
    [SerializeField] private float differenceY;

    private GameObject target;
    private GameObject bullet;
    private bool hasTarget = false;
    private bool hasBullet = false;

    private void Awake()
    {
        SpawnTarget();
        SpawnBullet();
    }

    private void Start()
    {
        CalculateSpeed();
    }

    private void FixedUpdate()
    {
        if(hasBullet && hasTarget)
        {
            Vector2 posTarget = target.transform.position;
            Vector2 posBullet = bullet.transform.position;
            float deltaTime = Time.fixedDeltaTime;
            speed += acceleration * deltaTime;
            Vector2 newPos = posBullet + speed * deltaTime + (acceleration * Mathf.Pow(deltaTime, 2)) / 2;

            Debug.DrawLine(bullet.transform.position, newPos, Color.white, 2f);

            bullet.transform.position = newPos;

            //Destroy all game objects on collision
            Vector2 vBullet2Target = posTarget - posBullet;
            float mBullet2Target = vBullet2Target.magnitude;
            if (mBullet2Target <= tolerance)
            {
                DestroyTarget();
                StartCoroutine(CreateTarget());
            }
        }
    }

    private void SpawnTarget()
    {
        Camera cam = Camera.main;
        Vector3 camPos = cam.transform.position;
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float halfHeight = camHeight / 2f;
        float halfWidth = camWidth / 2f;

        float offsetX = Random.Range(0, halfWidth);
        float offsetY = Random.Range(0, halfHeight);
        target = Instantiate(targetPrefab, new Vector3(offsetX, offsetY, transform.position.z), transform.rotation);
        hasTarget = true;
    }

    private void SpawnBullet()
    {
        bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        hasBullet = true;
    }

    private void DestroyTarget()
    {
        Destroy(target.gameObject);
        Destroy(bullet.gameObject);
        hasTarget = false;
        hasBullet = false;
    }

    private void CalculateSpeed()
    {
        Vector2 posTarget = target.transform.position;
        Vector2 posBullet = bullet.transform.position;
        Vector2 vBullet2Target = posTarget - posBullet;
        float mBullet2Target = vBullet2Target.magnitude;
        float t = vBullet2Target.x / speedX;
        acceleration = new Vector2(0, -gravity);
        differenceY = vBullet2Target.y;
        speedY = (differenceY - acceleration.y / 2 * Mathf.Pow(t, 2)) / t;
        speed = new Vector2(speedX, speedY);
    }

    private void MoveLauncher()
    {
        Camera cam = Camera.main;
        Vector3 camPos = cam.transform.position;
        float camHeight = 2f * cam.orthographicSize;
        float halfHeight = camHeight / 2f;

        float newYPos = Random.Range(-halfHeight, halfHeight);
        transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
    }

    IEnumerator CreateTarget()
    {
        yield return new WaitForSeconds(delayBeforeNewTarget);
        MoveLauncher();
        SpawnTarget();
        SpawnBullet();
        CalculateSpeed();
    }
}
