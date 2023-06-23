/*
 * Player movement and controls
 */
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public event System.Action OnReachedEndOfLevel;

    public float speed = 6f;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float turnSmoothTime = 0.1f;

    private float _turnSmoothVelocity;
    private bool _disabled;

    private void Start()
    {
        ChaserMovement.OnChaserHasSpottedPlayer += Disable;
    }

    // Update is called once per frame
    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = Vector3.zero;
        if (!_disabled)
        {
            direction = new Vector3(horizontal, 0f, vertical).normalized;
        }

        if (!(direction.magnitude >= 0.1f)) return;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        controller.Move(direction * (speed * Time.deltaTime));
    }

    // Triggers end
    private void OnTriggerEnter(Collider hitCollider)
    {
        if (!hitCollider.CompareTag("Finish")) return;
        Disable();
        OnReachedEndOfLevel?.Invoke();
    }

    // Disable the player when game over
    private void Disable()
    {
        _disabled = true;
    }

    // Add disable
    private void OnDestroy()
    {
        ChaserMovement.OnChaserHasSpottedPlayer -= Disable;
    }
}
