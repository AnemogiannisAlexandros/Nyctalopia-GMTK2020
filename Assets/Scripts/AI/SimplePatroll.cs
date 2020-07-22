using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class PatrolSet 
{
    [SerializeField]
    public Transform[] patrolPoints;
    [SerializeField]
    public int currentPointIndext;
    public int setIdleTime;
}

public enum AgentState
{
    Patroling,
    Idle,
    Chasing
}
public class SimplePatroll : MonoBehaviour
{
    [SerializeField]
    private int currentPaternIndex = 0;
    [SerializeField]
    private PatrolSet[] patrolPaterns;
    [SerializeField]
    private AgentState currentState;
    private NavMeshAgent agent;
    private Transform currentPatrolPoint;
    private Animator anim;
    bool hasOrders = false;
    // Start is called before the first frame update
    void Start()
    {
        TryGetComponent<NavMeshAgent>(out agent);
        TryGetComponent<Animator>(out anim);
    }

    public void ChangeToNewSet(int setIndex) 
    {
        currentPaternIndex = setIndex;
    }
    public IEnumerator IdleWaitFor(float seconds) 
    {
        hasOrders = true;
        anim.SetBool("Idle", true);
        anim.SetBool("Chasing", false);
        yield return new WaitForSeconds(seconds);

        if (patrolPaterns[currentPaternIndex].currentPointIndext + 1 >= patrolPaterns[currentPaternIndex].patrolPoints.Length)
        {

            if (currentPaternIndex + 1 < patrolPaterns.Length)
            {
                currentPaternIndex++;
            }
            else 
            {
                patrolPaterns[currentPaternIndex].currentPointIndext = 0;
            }
        }
        else
        {
            patrolPaterns[currentPaternIndex].currentPointIndext++;
        }
        currentState = AgentState.Patroling;
        hasOrders = false;
    }
    public IEnumerator TraverseToPoint(Transform newPoint) 
    {
        hasOrders = true;
        anim.SetBool("Idle", false);
        anim.SetBool("Chasing", true);
        agent.SetDestination(newPoint.position);
        while (Vector3.Distance(transform.position, newPoint.position) >= 0.4f)
        {
            yield return new WaitForEndOfFrame();
        }
        currentState = AgentState.Idle;
        hasOrders = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (!hasOrders)
        {
            switch (currentState)
            {
                case AgentState.Idle:
                    Debug.Log("Chilling");
                    StartCoroutine(IdleWaitFor(patrolPaterns[currentPaternIndex].setIdleTime));
                    return;

                case AgentState.Patroling:
                    Debug.Log("Patroling");
                    StartCoroutine(TraverseToPoint(patrolPaterns[currentPaternIndex].patrolPoints[patrolPaterns[currentPaternIndex].currentPointIndext]));
                    return;

                case AgentState.Chasing:

                    return;
            }

        }
    }
}
