/*
 * Speed power-up that the player can pick up
 */
using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float multiplier = 1.5f;
    [SerializeField] private float duration = 4f;

    // Player pick up object
    private void OnTriggerEnter(Collider triggerCollider)
    {
        if (triggerCollider.CompareTag("Player"))
        {
            StartCoroutine(Pickup(triggerCollider));
        }
    }

    // Change stats after duration and reverse
    private IEnumerator Pickup(Collider player)
    {
        ThirdPersonMovement stats = player.transform.GetComponent<ThirdPersonMovement>();
        stats.speed *= multiplier;

        // Disable view of powerup when touched
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Wait x amount of seconds
        yield return new WaitForSeconds(duration);

        // Reverse the effect on our player
        stats.speed /= multiplier;

        Destroy(gameObject);
    }
}
