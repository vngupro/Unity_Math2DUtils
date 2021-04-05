using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculIntersection : MonoBehaviour
{
    public SuperShip player;
    public Bullet bulletPrefab;
    public EnemyShip shipPrefab;
    public float tolerance = 1.0f;
    public float delayBeforeNewShip = 1.0f;
    private Bullet bullet;
    private EnemyShip ship;
    private bool hasBullet = false;
    private bool hasShip = false;
    private void Awake()
    {
        //bullet.cs
        GameEvents.onHit.AddListener(NextBullet);
        //enemyship.cs
        GameEvents.createShip.AddListener(CreateShip);
    }

    private void Start()
    {
        CreateShip();
    }

    private void FixedUpdate()
    {
        if (hasShip)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 shipPos = ship.transform.position;
            Vector3 target = ship.target - shipPos;
            Vector3 dirShip = target.normalized;
            Vector3 dirPlayer = playerPos - player.transform.up;

            if (((dirPlayer.y * dirShip.x) - (dirPlayer.x * dirShip.y)) != 0)
            {
                float k = ((playerPos.x - shipPos.x) * dirShip.y + (shipPos.y - playerPos.y) * dirShip.x) / ((dirPlayer.y * dirShip.x) - (dirPlayer.x * dirShip.y));
                Vector3 interPos = playerPos + dirPlayer * k;
                Debug.DrawLine(playerPos, interPos, Color.white, Time.fixedDeltaTime);

                Vector3 vPlayerToI = interPos - playerPos;
                float distBullet = vPlayerToI.magnitude;
                Vector3 vEnemyToI = interPos - shipPos;
                float distEnemy = vEnemyToI.magnitude;
                Vector3 speedEnemy = ship.speed;
                float tEnemy = distEnemy / speedEnemy.x;
                float tBullet = tEnemy;
                float speedBullet = distBullet / tBullet;

                if (!hasBullet)
                {
                    bullet = Instantiate(bulletPrefab, playerPos, player.transform.rotation);
                    bullet.speed = speedBullet;
                    bullet.interPos = interPos;
                    hasBullet = true;
                }

                ship.interPos = interPos;
                player.interPos = interPos;
            }
        }
    }

    public void NextBullet()
    {
        hasBullet = false;
    }

    public void CreateShip()
    {
        hasShip = false;
   
        StartCoroutine(NewShip());
    }

    IEnumerator NewShip()
    {
        yield return new WaitForSeconds(delayBeforeNewShip);
        ship = Instantiate(shipPrefab, transform.position, transform.rotation);
        yield return new WaitForSeconds(delayBeforeNewShip);
        hasShip = true;
    }
}
