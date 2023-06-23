/*
 * Guard enemy that follows a path. Probably unused.
 */
using UnityEngine;

public class Guard : MonoBehaviour
{
    [SerializeField] private Transform pathHolder;

    // Show path in the editor
    private void OnDrawGizmos()
    {
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
        }
    }
}
