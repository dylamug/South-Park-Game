using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManagerScript : MonoBehaviour
{


    public Queue<GameObject> enemyQueue;
    // Start is called before the first frame update
    void Start()
    {

        enemyQueue = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {


        
    }
}
