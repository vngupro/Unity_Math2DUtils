using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCamera : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;
    public GameObject playerFour;

    private void Update()
    {
        float cameraZ = Camera.main.transform.position.z;

        Camera.main.transform.position = (
            playerOne.transform.position
            + playerTwo.transform.position
            + playerThree.transform.position
            + playerFour.transform.position)
            / 4;

        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, cameraZ);
    }
}
