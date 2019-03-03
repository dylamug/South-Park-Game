using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodenSwordScript : MonoBehaviour
{



   private Collider myCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {

        myCollider.enabled = false;
        Debug.Log("dongs");
    }

   public void turnOnCollider()
    {
        myCollider.enabled = true;
    }

   public void turnOffCollider()
    {
        myCollider.enabled = false;
    }
}
