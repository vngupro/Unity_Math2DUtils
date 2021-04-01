using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculIntersection : MonoBehaviour
{
    public SuperShip player;
    public EnemyShip ship;
    public Bullet bulletPrefab;
    public EnemyShip shipPrefab;
    public float tolerance = 1.0f;

    private bool hasBullet = false;
    private bool hasShip = true;
    private void Awake()
    {
        //bullet.cs
        GameEvents.onHit.AddListener(NextBullet);
        //enemyship.cs
        GameEvents.createShip.AddListener(CreateShip);
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
                    Bullet bullet = Instantiate(bulletPrefab, playerPos, player.transform.rotation);
                    bullet.speed = speedBullet;
                    bullet.interPos = interPos;

                    if (Mathf.Abs(tBullet - tEnemy) < tolerance)
                    {
                        Debug.Log("hit !");
                    }

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
        ship = Instantiate(shipPrefab, transform.position, transform.rotation);
        hasShip = true;
    }
}
