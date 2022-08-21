using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int numberOfAgents;
    public GameObject agent;
    public float x;
    public float z;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfAgents; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-x,x), 1, Random.Range(-z,z));     //Random Vector3
            Instantiate(agent, randomPos,Quaternion.identity);                              //Spawn en agent
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
