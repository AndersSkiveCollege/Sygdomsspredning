using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour
{
    public int numberOfAgents;
    public GameObject agent;
    public float x;
    public float z;

    void Start()
    {
        for (int i = 0; i < numberOfAgents; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-x, x), 1, Random.Range(-z, z));     //Random Vector3
            Vector3 myPos = new Vector3();                                                    //Lav en tom Vector3
            myPos = NavMeshUtil.GetRandomPoint(randomPos);                                    //Kald GetRandomPoint og gem i myPos
            Instantiate(agent, myPos,Quaternion.identity);                                    //Spawn en agent på myPos
        }
    }
}

public static class NavMeshUtil
{
                                                                                                // Get Random Point on a Navmesh surface
    public static Vector3 GetRandomPoint(Vector3 pos)
    {
        NavMeshHit hit;                                                                         // NavMesh Sampling Info Container                                                                                           // from randomPos find a nearest point on NavMesh surface in range of 100
        NavMesh.SamplePosition(pos, out hit, 100, NavMesh.AllAreas);
        return hit.position;
    }
}
