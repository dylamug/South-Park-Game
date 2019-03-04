using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour

    
   
{
    public float bulletSpeed;
    public Transform playerLocation;
    public Vector3 bulletTarget;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed);

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        timer = 5;
    }

    void TurnOff()
    {
        gameObject.SetActive(false);
    }

 
}
