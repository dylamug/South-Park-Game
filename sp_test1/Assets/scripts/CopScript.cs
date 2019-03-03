using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;


public class CopScript : MonoBehaviour
{


    private NavMeshAgent myAgent;
    private Collider myTrigger;
    public Transform[] patrolPoints;
    public int currentPoint;
    public Transform PlayerLocation;
    private float walkSpeed = 1f;
    private float runSpeed = 5f;
    private float detectionDistance = 15f;
    private float stopChaseDistance = 20f;
    private float attackDistance = 8f;
    private float goBackToChasingDistance =10f;
    public bool is_Stopped;

    public GunShooter shooty;

    
  
    public bool waiting = true;

    public float playerDist;
    public Transform lookAtPoint;

    //public Bat_hitter hitter;
   
    
   // public characterHealth charHealth;


    private Vector3 lookTarget;
    public float lookAtSmoothFactor = 3f;

    Animator myAnim;

    float dist;

    
    
    public bool hasDied = false;

    public enum RNModes { Idling, Walking, Chasing, Attacking, Death };
    public RNModes myModes;


    // Start is called before the first frame update
    void Start()
    {

        
        myAgent = GetComponent<NavMeshAgent>();

        myAnim = GetComponent<Animator>();
        myTrigger = GetComponent<Collider>();
        currentPoint = 0;

        GoToPoint();

        //hitter = GameObject.FindWithTag("Bat_hitter").GetComponent<Bat_hitter>();



        lookTarget = transform.position + transform.forward;

        myModes = RNModes.Walking;
        GoToPoint();
        

    }

    
    void FixedUpdate()

    {
        playerDist = Vector3.Distance(transform.position, PlayerLocation.position);
        is_Stopped = myAgent.isStopped;
        
       
        //if (charHealth.isDead == true)
        //{
        //    myModes = RNModes.Death;
        //}



        switch (myModes)
        {
            case RNModes.Idling:
                
                Idling();
                break;

            case RNModes.Walking:
                
                Walking();
                break;

            case RNModes.Chasing:
               
                Chasing();
               
                break;
            case RNModes.Attacking:
                
                Attacking();
                break;

            case RNModes.Death:
                
               // Death();
                break;
        }

    





    }

    void Idling()
    {
        myAgent.isStopped = true;
      
        // transform.LookAt(PlayerLocation);
        myAnim.SetInteger("Walking", 0);

        if (playerDist <= detectionDistance)
        {
            myModes = RNModes.Chasing;
            

        }

        if (waiting == true)
        {
            Invoke("backToPatrolling", 5);
        }

    }

    void Walking()
    {
        if (myAgent.remainingDistance <= 0.3f  )
        {
            if (waiting == true)
            {
                myModes = RNModes.Idling;
                
            }

            else
            {
                GoToPoint();
                
            }
           
        }


        if (playerDist <= detectionDistance)
        {
            myModes = RNModes.Chasing;
           
        }

        myAnim.SetInteger("Walking", 1);
        myAgent.isStopped = false;

        myAgent.speed = walkSpeed ;
    }

    void Chasing()
    {




       
       

            myAgent.isStopped = false;
        myAgent.speed = runSpeed;

        myAnim.SetInteger("Walking", 2);
        myAgent.SetDestination(PlayerLocation.position);

        if (playerDist >= stopChaseDistance)
        {
            myModes = RNModes.Walking;
            GoToPoint();
           

        }

            if (playerDist <= attackDistance)
            {
                myModes = RNModes.Attacking;
            }

        
    }

    void Attacking()
    {
        
        transform.LookAt(PlayerLocation);
        myAgent.isStopped = true;



        myAnim.SetInteger("Walking", 7);

    }

    



    public void GoToPoint()
    {
      


            myAgent.SetDestination(patrolPoints[currentPoint].position);
            currentPoint++;
            if (currentPoint >= patrolPoints.Length)
            {
                currentPoint = 0;
            }
        
    }

    void OnTriggerStay(Collider col)
    {

       
    }


  

   

   
       
   

    void beginAttack()
    {
        //hitter.TurnBackOn();
    }

    void backToPatrolling()
    {
        myModes = RNModes.Walking;
        
        waiting = false;
        Invoke("makeWaitingTrue", 5);
        
    }

    void FireTheGun()
    {
        shooty.FireGun();
    }
    void makeWaitingTrue()
    {
        waiting = true;
    }

    

    //void Death()
    //{
    //    myAgent.isStopped = true;
    //    if (charHealth.isDead == true && hasDied == false)
    //    {
    //        myAnim.SetInteger("walking", 10);
    //        hasDied = true;

    //    }

    //    myTrigger.enabled = false;

    //}



}






