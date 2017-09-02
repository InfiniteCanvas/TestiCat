using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CatMovement : MonoBehaviour
{
    public NavMeshAgent Agent;

    private void Start()
    {
        StartCoroutine(MoveCat());
    }

    private IEnumerator MoveCat()
    {
        Vector3 goal = Vector3.forward*10;

        for (;;)
        {
            goal = Quaternion.Euler(0,90,0) * goal;
            Agent.SetDestination(goal);
            for (int i = 0; i < 200; i++)
            {
                
                yield return new WaitForFixedUpdate();
            }
        }
    }
}
