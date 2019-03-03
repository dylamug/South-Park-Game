using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;


public class Redneckscript : MonoBehaviour
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
    private float attackDistance = 1.5f;
    private float goBackToChasingDistance = 2f;
    private bool hasRoared = true;
   public int attackCounter = 1;
    public bool waiting = true;
    private bool p;
    public float playerDist;
    public Transform lookAtPoint;

    public Bat_hitter hitter;
    public bool timeToAttack;

    private bool triggered;
    public bool otherEnemyPresent;

    int enemiesNearby;
   
    
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

        otherEnemyPresent = false;

        enemiesNearby = 0;

        timeToAttack = false;
        p = true;


        lookTarget = transform.position + transform.forward;

        myModes = RNModes.Walking;
        GoToPoint();
        

    }

    
    void FixedUpdate()

    {
        playerDist = Vector3.Distance(transform.position, PlayerLocation.position);

        

        if (enemiesNearby > 0)
        {
            otherEnemyPresent = true;
        }

        else
        {
            otherEnemyPresent = false;
        }
       
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
            myModes = RNModes.Walking;
            GoToPoint();
           

        }

            if (playerDist <= attackDistance)
            {
                myModes = RNModes.Attacking;
            }

        }
    }

    void Attacking()
    {
        
        transform.LookAt(PlayerLocation);
        myAgent.isStopped = true;
        if (playerDist >= goBackToChasingDistance)
        {
            myModes = RNModes.Chasing;
        }

        myAnim.SetInteger("Walking", 3);

        int t;

        if (otherEnemyPresent == true)
        {
            if (timeToAttack == false && p == true)
            {
                myAnim.SetInteger("Walking", 3);
                t = Random.Range(3, 6);
                Invoke("BackToAttack", t);
                p = false;
            }



            if (timeToAttack == true)
            {


             
                if (attackCounter % 2 == 0 && attackCounter % 7 != 0)
                {
                    myAnim.SetTrigger("attack1");




                }

                else if (attackCounter % 2 != 0 && attackCounter % 7 != 0)
                {
                    myAnim.SetTrigger("attack2");

                }

                else if (attackCounter % 7 == 0)
                {
                    myAnim.SetTrigger("attack3");
                }

            }
        }

        else
        {
            if (timeToAttack == false && p == true)
            {
                myAnim.SetInteger("Walking", 3);
                t = Random.Range(1, 2);
                Invoke("BackToAttack", t);
                p = false;
            }



            if (timeToAttack == true)
            {



                if (attackCounter % 2 == 0 && attackCounter % 7 != 0)
                {
                    myAnim.SetTrigger("attack1");




                }

                else if (attackCounter % 2 != 0 && attackCounter % 7 != 0)
                {
                    myAnim.SetTrigger("attack2");

                }

                else if (attackCounter % 7 == 0)
                {
                    myAnim.SetTrigger("attack3");
                }

            }
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

    private void OnTriggerEnter(Collider other)
    {

        

        if (other.gameObject.tag == "Enemy")
        {
            enemiesNearby++;
            

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesNearby--;
        }
    }




    void increaseAttackCounter()
    {
        attackCounter++;
        
       hitter.TurnOffCollider();
        timeToAttack = false;
        p = true;
       
    }

   
       
    void stopRoaring()
    {
        hasRoared = true;
    }

   

    void beginAttack()
    {
        hitter.TurnBackOn();
    }

    void backToPatrolling()
    {
        myModes = RNModes.Walking;
        
        waiting = false;
        Invoke("makeWaitingTrue", 5);
        
    }

    void makeWaitingTrue()
    {
        waiting = true;
    }

    void BackToAttack()
    {
        timeToAttack = true;
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






