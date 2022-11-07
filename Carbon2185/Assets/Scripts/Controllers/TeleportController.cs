using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeleportController : MonoBehaviour
{
    public Transform teleportTarget;

    public Transform walkToPoint;
    public GameObject player;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Starting Transport to: " + teleportTarget.transform.position);        
            
            //player.GetComponent<NavMeshAgent>().isStopped = true;
            player.GetComponent<NavMeshAgent>().Warp(teleportTarget.transform.position);
            player.GetComponent<PlayerMotor>().MoveToPoint(walkToPoint.transform.position);

            Debug.Log("Transported");
        }

        
    }
}
