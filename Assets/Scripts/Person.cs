using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour
{
    Vector3 targetPos;
    public NavMeshAgent agent;
    public float x;
    public float z;
    public bool isSick;
    public List<GameObject> nearbyAgents = new List<GameObject>();                  //Liste til agenter i nærheden
    public List<GameObject> nearbySickAgents = new List<GameObject>();              //Liste til syge agenter i nærheden
    public float infectiousness;
    public Renderer rend;

    


    void Start()
    {
        
        targetPos = new Vector3(Random.Range(-x, x), 1, Random.Range(-z, z));       //Lav en random Vector3
        agent.SetDestination(targetPos);                                            //Set agentens destination
    }

    
    void Update()
    {
        if (Vector3.Distance(targetPos,agent.transform.position) < 1)               //Hvis afstanden mellem agent og destination er under 1
        {
            targetPos = new Vector3(Random.Range(-x,x), 1, Random.Range(-z, z));    //Lav en random Vector3
            agent.SetDestination(targetPos);                                        //Set agentens position
        }

        if (isSick == true)
        {
            rend.material.color = new Color(0, 0, 0);
        }

        for (int i = 0; i < nearbyAgents.Count; i++)
        {
            if (nearbyAgents[i].GetComponent<Person>().isSick == true)
            {
                if (nearbySickAgents.Contains(nearbyAgents[i]) == false)
                {
                    nearbySickAgents.Add(nearbyAgents[i]);
                }
            }
        }

        for (int i = 0; i < nearbySickAgents.Count; i++)
        {
            if (nearbyAgents.Contains(nearbySickAgents[i]) == false)
            {
                
                nearbySickAgents.Remove(nearbySickAgents[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        if (nearbyAgents.Count > 0)
        {
            TryInfect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (nearbyAgents.Contains(other.gameObject) == false && other.gameObject.name != "Ground")
        {
            nearbyAgents.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nearbyAgents.Remove(other.gameObject);
    }

    public void TryInfect()
    {
        

        if (infectiousness * 0.02 * (float)nearbySickAgents.Count  > Random.Range(0f,1f))
        {
            isSick = true;
        }
    }
}
