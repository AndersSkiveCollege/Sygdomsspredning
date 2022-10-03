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
    public bool isHumanSick;
    public List<GameObject> nearbyAgents = new List<GameObject>();                  //Liste til agenter i nærheden
    public List<GameObject> nearbySickAgents = new List<GameObject>();              //Liste til syge agenter i nærheden
    public float infectiousness;
    public Renderer rend;
    public ParticleSystem flies;
    public Animator anim;

    


    void Start()
    {
        anim.SetBool("isSick",false);                                               //sætter animator bool til false
        flies.Stop();
        targetPos = new Vector3(Random.Range(-x, x), 1, Random.Range(-z, z));       //Lav en random Vector3
        targetPos = NavMeshUtil.GetRandomPoint(targetPos);                          //Kalder GetRandomPoint fra NavMeshUtil-klassen som er en hjemmelavet klasse. Tilbage kommer en valid position som var tættest på den pos som blev givet som argument.
        agent.SetDestination(targetPos);                                            //Set agentens destination
    }

    
    void Update()
    {
        if (Vector3.Distance(targetPos,agent.transform.position) < 3)               //Hvis afstanden mellem agent og destination er under 1
        {
            targetPos = new Vector3(Random.Range(-x,x), 1, Random.Range(-z, z));    //Lav en random Vector3
            targetPos = NavMeshUtil.GetRandomPoint(targetPos);                      //Kalder GetRandomPoint fra NavMeshUtil-klassen som er en hjemmelavet klasse. Tilbage kommer en valid position som var tættest på den pos som blev givet som argument.
            agent.SetDestination(targetPos);                                        //Set agentens position
        }

        if (isHumanSick == true)
        {
            anim.SetBool("isSick", true);
            agent.speed = 7;
            rend.material.color = new Color(0, 0, 0);

            if (flies.isStopped == true)
            {
                flies.Play();
            }
            
        }

        for (int i = 0; i < nearbyAgents.Count; i++)
        {
            if (nearbyAgents[i].GetComponent<Person>().isHumanSick == true)
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
            isHumanSick = true;
        }
    }
}
