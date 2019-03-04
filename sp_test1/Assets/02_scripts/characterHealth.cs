using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterHealth : MonoBehaviour
{

    public float charHealth;
    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        charHealth = 100;
        isDead = false;

        

        
    }

    // Update is called once per frame
    void Update()
    {
        if (charHealth <= 0 && isDead == false)
        {
            isDead = true;
        }
    }


    void WoodenSwordDamage()
    {
        charHealth -= 50;
        

    }

    void BallDamage()
    {
        charHealth -= 100;
    }


   void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Woody")
        {
           
            WoodenSwordDamage();
            Debug.Log("Triggered");


        }

        if (col.gameObject.tag == "Ball")

           
        {
            BallDamage();
        }

      




    }

   
}
