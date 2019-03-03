using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBP_puncherScript : MonoBehaviour
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
