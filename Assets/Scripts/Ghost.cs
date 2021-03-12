using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public GameObject player;
    public float speed = 1.0f;

    private void Update()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 ghostPos = transform.position;

        Vector3 vectPlayerGhost = ghostPos - playerPos;
        Vector2 forward = transform.up;

        float dot = Vector2.Dot(forward, vectPlayerGhost);

        bool isInFront = (dot >= 0);
        if (!isInFront)
        {
            Vector3 newGhostPos = ghostPos - vectPlayerGhost.normalized * speed * Time.deltaTime;
            transform.position = newGhostPos;
        }
    }
}
