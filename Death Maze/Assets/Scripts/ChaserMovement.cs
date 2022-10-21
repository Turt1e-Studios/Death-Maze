using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserMovement : MonoBehaviour
{

    public static event System.Action OnChaserHasSpottedPlayer;

    public Transform ThirdPersonMovement;
    public float speed = 7;

    bool disabled = false;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<ThirdPersonMovement>().OnReachedEndOfLevel += Disable;
    }

    // Update is called once per frame
    public void Update()
    {
        Vector3 displacementFromTarget = ThirdPersonMovement.position - transform.position;
        Vector3 directionToTarget = displacementFromTarget.normalized;
        Vector3 velocity = directionToTarget * speed;

        float distanceToTarget = displacementFromTarget.magnitude;
        if (distanceToTarget > 1.5f && disabled == false)
        {
            transform.Translate(velocity * Time.deltaTime);
        }
        else
        {
            if (OnChaserHasSpottedPlayer != null)
            {
                OnChaserHasSpottedPlayer();
                Disable();
            }
        }
        
    }

    void Disable()
    {
        disabled = true;
    }
}
