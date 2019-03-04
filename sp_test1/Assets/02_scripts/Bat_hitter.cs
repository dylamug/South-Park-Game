using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat_hitter : MonoBehaviour
{
    private Collider myCollider;
    

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            myCollider.enabled = false;
            
        }

    }

    public void TurnOffCollider()
    {
        myCollider.enabled = false;
       
    }

    public void TurnBackOn()
    {
        myCollider.enabled = true;
    }
}
