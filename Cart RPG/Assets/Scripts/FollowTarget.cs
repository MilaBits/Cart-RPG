using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour {

    private NavMeshAgent agent;

    //the gameobject to follow
    public GameObject target;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update() {
        agent.SetDestination(target.transform.position);

        NavMeshPath path = new NavMeshPath();

        if (!agent.isOnNavMesh) {
            Debug.Log("Not on navmesh!");
        }

        agent.CalculatePath(target.transform.position, path);
    }
}
