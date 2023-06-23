/*
 * Movement and actions of the Chaser enemy
 */
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ChaserMovement : MonoBehaviour
{
    public static event System.Action OnChaserHasSpottedPlayer;
    
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [SerializeField] private Transform thirdPersonMovement;
    [SerializeField] private float speed = 5;
    [SerializeField] private float timeForNewPath;
    
    private NavMeshPath _path;
    private Vector3 _target;
    private bool _inCoroutine;
    private bool _validPath;
    private bool _disabled;
    
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        FindObjectOfType<ThirdPersonMovement>().OnReachedEndOfLevel += Disable;
    }
    
    public void Update()
    {
        Vector3 displacementFromTarget = thirdPersonMovement.position - transform.position;
        Vector3 directionToTarget = displacementFromTarget.normalized;
        Vector3 velocity = directionToTarget * speed;

        float distanceToTarget = displacementFromTarget.magnitude;
        
        // Set Navmesh if certain distance away (kept comments just in case)
        if (distanceToTarget >= 2.0f && _disabled == false)
        {
            //   if (!inCoRoutine)
            //DoSomething();
            navMeshAgent.SetDestination(thirdPersonMovement.position);
            //GetNewPath();
        }


        //if (distanceToTarget < 2.5f && distanceToTarget > 0.1f)
        //{
            //transform.Translate(velocity * Time.deltaTime);
            //navMeshAgent.SetDestination(ThirdPersonMovement.position);
        //}

       // if (distanceToTarget < 1.5f && disabled == false)
        //{
            //transform.Translate(velocity * Time.deltaTime);
            //navMeshAgent.SetDestination(ThirdPersonMovement.position);
        //}
        else
        {
            //if ( distanceToTarget <= 0.01f)
            //{
                if (OnChaserHasSpottedPlayer != null) {

                    OnChaserHasSpottedPlayer();
                    Disable();

                //}
            }
        }
    }

    // Disable the enemy
    private void Disable()
    {
        _disabled = true;
    }

    // Create a new random position
    private static Vector3 GetNewRandomPosition()
    {
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    // Get a new path
    private void GetNewPath()
    {
        _path = new NavMeshPath();
        _target = GetNewRandomPosition();
        navMeshAgent.SetDestination(_target);
    }

    // Generate a new path?
    private IEnumerator GeneratePath()
    {
        _inCoroutine = true;
        yield return new WaitForSeconds(timeForNewPath);
        GetNewPath();
        _validPath = navMeshAgent.CalculatePath(_target, _path);
        if (!_validPath) Debug.Log("Found an invalid path");

        while (!_validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            _validPath = navMeshAgent.CalculatePath(_target, _path);
        }
        _inCoroutine = false;
    }
}
