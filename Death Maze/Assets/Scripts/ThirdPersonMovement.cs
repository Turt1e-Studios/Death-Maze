using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public event System.Action OnReachedEndOfLevel;

    public CharacterController controller;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    bool disabled;

    void Start()
    {
        ChaserMovement.OnChaserHasSpottedPlayer += Disable;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = Vector3.zero;
        if (!disabled)
        {
            direction = new Vector3(horizontal, 0f, vertical).normalized;
        }
        
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }

    }

    void OnTriggerEnter(Collider hitCollider)
    {
        if (hitCollider.tag == "Finish")
        {
            Disable();
            if (OnReachedEndOfLevel != null)
            {
                OnReachedEndOfLevel();
            }
        }
    }

    void Disable()
    {
        disabled = true;
    }

    void OnDestroy()
    {
        ChaserMovement.OnChaserHasSpottedPlayer -= Disable;
    }
}
