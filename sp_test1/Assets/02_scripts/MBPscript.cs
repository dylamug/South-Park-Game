using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;


public class MBPscript : MonoBehaviour
{


    private NavMeshAgent myAgent;
    private Collider myTrigger;
    public Transform[] patrolPoints;
    public int currentPoint;
    public Transform PlayerLocation;
    private float walkSpeed = 2f;
    private float runSpeed = 5f;
    private float detectionDistance = 15f;
    private float stopChaseDistance = 20f;
    private float attackDistance = 2.2f;
    private float goBackToChasingDistance = 2.5f;
    private bool hasRoared = true;
   public int attackCounter = 1;
    public bool waiting = true;

    public float playerDist;
    public Transform lookAtPoint;

    public MBP_puncherScript puncher;
   
    
   // public characterHealth charHealth;


    private Vector3 lookTarget;
    public float lookAtSmoothFactor = 3f;

    Animator myAnim;

    float dist;

    
    
    public bool hasDied = false;

    public enum MBPModes { Idling, Walking, Chasing, Attacking, Death };
    public MBPModes myModes;


    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

        myAnim = GetComponent<Animator>();
        myTrigger = GetComponent<Collider>();
        currentPoint = 0;

        GoToPoint();

        puncher = GameObject.FindWithTag("MBP_puncher").GetComponent<MBP_puncherScript>();



        lookTarget = transform.position + transform.forward;

        myModes = MBPModes.Walking;
        GoToPoint();
        

    }

    // Update is called once per frame
    void Update()

    {
        playerDist = Vector3.Distance(transform.position, PlayerLocation.position);

        
       
        //if (charHealth.isDead == true)
        //{
        //    myModes = MBPModes.Death;
        //}



        switch (myModes)
        {
            case MBPModes.Idling:
                
                Idling();
                break;

            case MBPModes.Walking:
               
                Walking();
                break;

            case MBPModes.Chasing:
               
                Chasing();
               
                break;
            case MBPModes.Attacking:
                
                Attacking();
                break;

            case MBPModes.Death:
                
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
            myModes = MBPModes.Chasing;
           

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
                myModes = MBPModes.Idling;
                
            }

            else
            {
                GoToPoint();
                
            }
           
        }


        if (playerDist <= detectionDistance)
        {
            myModes = MBPModes.Chasing;
            

        }

        myAnim.SetInteger("Walking", 1);
        myAgent.isStopped = false;

        myAgent.speed = walkSpeed ;
    }

    void Chasing()
    {



        if (hasRoared == false)
        {
            myAgent.isStopped = true;
           transform.LookAt(PlayerLocation.position);
            myAnim.SetInteger("Walking", 3);
            

        }

        else
        {

            myAgent.isStopped = false;
        myAgent.speed = runSpeed;

        myAnim.SetInteger("Walking", 2);
        myAgent.SetDestination(PlayerLocation.position);

        if (playerDist >= stopChaseDistance)
        {
            myModes = MBPModes.Walking;
            GoToPoint();
           

        }

            if (playerDist <= attackDistance)
            {
                myModes = MBPModes.Attacking;
            }

        }
    }

    void Attacking()
    {
        
        transform.LookAt(lookAtPoint);
        myAgent.isStopped = true;
      

        
        
        if (playerDist >= goBackToChasingDistance)
        {
            myModes = MBPModes.Chasing;
        }
        if (attackCounter%2 == 0 && attackCounter%7 !=0)
        {
            myAnim.SetInteger("Walking", 5);
           
           

        }

        else if (attackCounter % 2 != 0 && attackCounter%7 !=0)
        {
            myAnim.SetInteger("Walking", 6);
           
        }

        else if (attackCounter %7 == 0)
        {
            myAnim.SetInteger("Walking", 7);
        }


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


    void catchUpToPlayer()
    {
        myAgent.isStopped = false;
        myAgent.SetDestination(PlayerLocation.position);
    }

    void increaseAttackCounter()
    {
        attackCounter++;
        
        puncher.TurnOffCollider();
       
    }

   
       
    void stopRoaring()
    {
        hasRoared = true;
    }

    void beginAttack()
    {
        puncher.TurnBackOn();
    }

    void backToPatrolling()
    {
        myModes = MBPModes.Walking;
        
        waiting = false;
        Invoke("makeWaitingTrue", 5);
        
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






