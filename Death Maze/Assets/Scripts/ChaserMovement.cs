using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaserMovement : MonoBehaviour
{
    public static event System.Action OnChaserHasSpottedPlayer;

    public Transform ThirdPersonMovement;
    public float speed = 5;

    bool disabled = false;

    public NavMeshAgent navMeshAgent;
    public NavMeshPath path;
    public float timeForNewPath;
    bool inCoRoutine;
    Vector3 target;
    bool validPath;

    

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
   
        FindObjectOfType<ThirdPersonMovement>().OnReachedEndOfLevel += Disable;
    }

    // Update is called once per frame
    public void Update()
    {

        

        Vector3 displacementFromTarget = ThirdPersonMovement.position - transform.position;
        Vector3 directionToTarget = displacementFromTarget.normalized;
        Vector3 velocity = directionToTarget * speed;

        float distanceToTarget = displacementFromTarget.magnitude;


        if (distanceToTarget >= 2.5f && disabled == false)
        {
            //   if (!inCoRoutine)
            //DoSomething();
            navMeshAgent.SetDestination(ThirdPersonMovement.position);
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

    void Disable()
    {
        disabled = true;
    }

    Vector3 getNewRandomPosition()
    {

        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;

    }

    void GetNewPath()
    {
        path = new NavMeshPath();
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);

    }


    IEnumerator DoSomething()
    {
        
        inCoRoutine = true;
        yield return new WaitForSeconds(timeForNewPath);
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);
        if (!validPath) Debug.Log("Found an inavlid path");


        while (!validPath)
        {

            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }
        inCoRoutine = false;

    }

    
}
