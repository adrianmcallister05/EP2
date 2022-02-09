using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnScript : MonoBehaviour
{

    public Transform Food;
    public float foodCount;
    public float foodStop;
    public float foodx;
    public float foody;
    public float foodz;

    void Start()
    {
        foodCount = Random.Range(10, 15);  
        foodStop = 0;
        InvokeRepeating("FoodSpawn", 0f, 0.05f);
    }

    void FoodSpawn()
    {
        foodx = Random.Range(-13, 13);
        foody = 0;
        foodz = Random.Range(-13, 13);
        Food.position = new Vector3(foodx, foody, foodz);
        Instantiate(Food);
        foodStop += 1;
    }

    void Update()
    {
        if(foodStop == foodCount) {
            CancelInvoke();
        }    
    }
}