using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{

    public float pHealth;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        pHealth = 100;
        isDead = false;

        

        
    }

    // Update is called once per frame
    void Update()
    {
        if (pHealth <= 0 && isDead == false)
        {
            isDead = true;
        }
    }




   void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "MBP_puncher")
        {
            pHealth -= 10;

        }

        if (col.gameObject.tag == "Bat_hitter")
        {
            pHealth -= 10;
        }





    }

   
}
