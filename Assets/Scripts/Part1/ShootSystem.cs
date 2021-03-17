using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSystem : MonoBehaviour
{
    public GameObject player;

    public GameObject crossHair;
    public float distToPlayer = 1.0f;

    public Rigidbody2D bulletPrefab;
    public float bulletSpeed = 1.0f;

    private void Update()
    {
        //Get mouse position in world
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x, 
            Input.mousePosition.y, 
            -Camera.main.transform.position.z));
        Vector3 playerPos = player.transform.position;
        //that vect is already giving the direction but the speed of the bullet will vary with the distance player-mouse
        Vector3 vectPlayerMouse = worldMousePos - playerPos;
        //that's why normalized the vector to 1 = bulletSpeed no dependence with the distance of the mouse
        Vector3 normalizePlayerMouse = vectPlayerMouse.normalized;

        //crossHair is vector plus a distance you add
        Vector3 crossHairPos = normalizePlayerMouse * distToPlayer;
        crossHair.transform.position = playerPos + crossHairPos;

        //to rotate you need an angle
        float aimAngle = Mathf.Atan2(vectPlayerMouse.y, vectPlayerMouse.x);
        player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, aimAngle * Mathf.Rad2Deg - 45));



        if (Input.GetMouseButtonDown(0))
        {
            //Not gameobject but rigidbody2d to access velocity directly
            Rigidbody2D newBullet = Instantiate(bulletPrefab, playerPos, Quaternion.identity);
            Vector2 bulletVelocity =  normalizePlayerMouse * bulletSpeed;
            newBullet.velocity = bulletVelocity;
        }
    }
}
