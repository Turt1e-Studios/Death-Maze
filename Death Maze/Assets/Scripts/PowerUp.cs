using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float multiplier = 1.5f;
    public float duration = 4f;

    void OnTriggerEnter(Collider triggerCollider)
    {
        if (triggerCollider.tag == "Player")
        {
            StartCoroutine(Pickup(triggerCollider));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        ThirdPersonMovement stats = player.transform.parent.GetComponent<ThirdPersonMovement>();
        stats.speed *= multiplier;

        // disable view of powerup when touched
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Wait x amount of seconds
        yield return new WaitForSeconds(duration);

        // Reverse the effect on our player
        stats.speed /= multiplier;

        Destroy(gameObject);
    }
}
