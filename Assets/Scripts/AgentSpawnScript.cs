using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentSpawnScript : MonoBehaviour
{
    public Transform agent;

    public float agentCount;
    public float agentStop;
    public float agentx;
    public float agenty;
    public float agentz;
    public float agentSide;

    void Start()
    {
        VariableLibrary.agentCount = Random.Range(10, 15);  
        agentStop = 0;
        InvokeRepeating("AgentSpawn", 0f, 0.05f);
    }

    void AgentSpawn()
    {
        agentSide = Random.Range(0, 4);
        agenty = 0;
        if (agentSide == 0) {
            agentx = -17;
            agentz = Random.Range(-17, 17);   
        }
        else if (agentSide == 1) {
            agentx = 17;
            agentz = Random.Range(-17, 17);
        }
        else if (agentSide == 2) {
            agentx = Random.Range(-17, 17);
            agentz = -17;
        }
        else if (agentSide == 3) {
            agentx = Random.Range(-17, 17);
            agentz = 17;
        }
        
        agent.position = new Vector3(agentx, agenty, agentz);
        Instantiate(agent);
        agentStop += 1;
    }

    void Update()
    {
        if(agentStop == VariableLibrary.agentCount) {
            CancelInvoke("AgentSpawn");
        }   
    }
}
