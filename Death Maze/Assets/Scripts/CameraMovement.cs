/*
 * Camera movement
 */
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    private const int YDisplacement = 20;

    // Makes the camera always directly above the player
    private void Update()
    {
        transform.position = player.transform.position + new Vector3(0, YDisplacement, 0);
    }
}
