using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentNavScript : MonoBehaviour
{
    
    public NavMeshAgent agent;
    public GameObject foodCheck;
    public Vector3 destination;
    public Vector3 home;
    public float agentmode;
    private float foodCollected;
    public float wanderx;
    public float wandery;
    public float wanderz;
    public bool DestTF;
    public float agentStamina;
    public bool HomeTF;
    public bool ReproduceTF;

    void Start()
    {
        agentmode = 0;
        foodCollected = 0; 
        DestTF = false;
        HomeTF = false;
        ReproduceTF = false;
        InvokeRepeating("WanderReset", 0f, 2f);
        agentStamina = 1500;
        home = agent.transform.position;
    }

    void WanderReset() 
    {
        DestTF = false;
    }

    void WanderDest()
    {
        wanderx = Random.Range(-13, 13);
        wandery = 2;
        wanderz = Random.Range(-13, 13);
        agent = this.GetComponent<NavMeshAgent>();
        destination = new Vector3(wanderx, wandery, wanderz);
        agent.SetDestination(destination);
        DestTF = true;
    }

    void FoodDetection()
    {
        Collider[] hitColliders = Physics.OverlapSphere(agent.transform.position, 3);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            GameObject hitCollider = hitColliders[i].gameObject;
            if (hitCollider.CompareTag("Food"))
            {
                agentmode = 1;
                agent.SetDestination(hitCollider.transform.position);
                foodCheck = hitCollider;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Food")) {
            Destroy(other.gameObject);
            foodCollected += 1;
            agentmode = 0;
        }
    }

    void Reproduce()
    {
        Instantiate(agent);
    }

    
    void Update()
    {
        agentStamina -= 0.2f;

        if (agentStamina <= 400) {
            agentmode = 2;
        }

        if (foodCollected >= 2) {
            agentmode = 2;
        }
        
        if (agentStamina <= 0) {
            
            Collider[] hitColliders2 = Physics.OverlapSphere(agent.transform.position, 2);
            for (int i = 0; i < hitColliders2.Length; i++)
            {
                GameObject hitCollider = hitColliders2[i].gameObject;
                if (hitCollider.CompareTag("Home"))
                {
                    HomeTF = true;
                    Debug.Log("Made it home!");
                }
            }
            if (HomeTF == false) {
                Debug.Log("I Didn't make it home!");
                Destroy(gameObject);
            }
            else {
                if (foodCollected <= 0) {
                    Debug.Log("I Didn't get enough food!");
                    Destroy(gameObject);
                }
                else if (foodCollected == 1) {
                    Debug.Log("I survived!");
                }
                else if (foodCollected >= 2) {
                    if (ReproduceTF == false){
                        Reproduce();
                        ReproduceTF = true;
                    }
                }
            }
                
        } 

        if (agentmode == 0)
        {
            Debug.Log("Wandering");   
            FoodDetection();
            if (DestTF == false) {
                WanderDest();          
            }             
        }

        if (agentmode == 1)
        {
            Debug.Log("Getting food");
        }

        if (agentmode == 2) {
            Debug.Log("Heading Home");
            agent.SetDestination(home);
        }

        if (foodCheck == null) {
            Debug.Log("Food was taken");
            agentmode = 0;
        }
    }

}

