using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;


public class NPC_navscript : MonoBehaviour
{


    private NavMeshAgent myAgent;
    private Collider myTrigger;
    public Transform[] patrolPoints;
    public int currentPoint;
    public Transform PlayerLocation;
    public Transform fleeingPoint;
    private Vector3 testVec = new Vector3(0,0,0);
    public float fleeingDist;
    //public Transform fleeingPoint;
    public characterHealth charHealth;

    bool soundMade = false;
    int i = 0;


    private Vector3 lookTarget;
    public float lookAtSmoothFactor = 3f;

    public AudioClip[] IdleSounds;
    private AudioSource myAudio;

    Animator myAnim;

    float dist;

    bool contact = false;
  public  bool fleeing = false;
    public bool hasDied = false;

    public enum NPCModes { Idling, Walking, Fleeing, Death };
    public NPCModes myModes;


    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

        myAnim = GetComponent<Animator>();
        myTrigger = GetComponent<Collider>();
        currentPoint = 0;

        GoToPoint();
        myAudio = GetComponent<AudioSource>();



        lookTarget = transform.position + transform.forward;

        myModes = NPCModes.Walking;
        GoToPoint();


    }

    // Update is called once per frame
    void Update()

    {
        if (charHealth.isDead == true)
        {
            myModes = NPCModes.Death;
        }

        fleeingDist = Vector3.Distance(transform.position, testVec);
        
        switch (myModes)
        {
            case NPCModes.Idling:
                
                Idling();
                break;

            case NPCModes.Walking:
                
                Walking();
                break;

            case NPCModes.Fleeing:
                
                Fleeing();
                break;
            case NPCModes.Death:
                
                Death();
                break;
        }


     
        Vector3 pos = lookTarget - transform.position;
        Quaternion newRot = Quaternion.LookRotation(pos);



        if (contact == true && fleeing == false && hasDied == false)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, lookAtSmoothFactor * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {

            if (fleeing == false)
            {

                
                myModes = NPCModes.Fleeing;
            fleeing = true;
          
            testVec = new Vector3( Random.Range(-40f, 40f), transform.position.y, Random.Range(-40,40));
            myAgent.SetDestination(testVec);

            }

          



        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (fleeing == true)
            {
                fleeing = false;
                GoToPoint();
              ;
                myModes = NPCModes.Walking;

            }
        }

        //if (Input.GetKey(KeyCode.U))
        //{
            
        //}
      





    }

    void Idling()
    {
        myAgent.isStopped = true;
        contact = true;
        // transform.LookAt(PlayerLocation);
        myAnim.SetInteger("walking", 0);


       float t = Time.deltaTime;
        int i = 0;
       
        if (t%5 == 0)
        {
            
        }

    }

    void Walking()
    {
        if (myAgent.remainingDistance <= 0.1f && myAgent.hasPath)
        {

            GoToPoint();
        }


        if (gameObject.tag == "Mackey")
        {
            int t;

            if (soundMade == false)
            {
                myAudio.PlayOneShot(IdleSounds[i]);
                i++;
                soundMade = true;

                t = Random.Range(5, 8);
                Invoke("BackToSound", t);
                if (i >= IdleSounds.Length)
                {
                    i = 0;
                }
            }

        }

       

        myAnim.SetInteger("walking", 1);
        myAgent.isStopped = false;

        myAgent.speed = 1.2f;
    }

    void Fleeing()
    {
        myAgent.speed = 5f;
        myAgent.isStopped = false;
        
        if (fleeingDist <= 0.1f)
        {
            myAnim.SetInteger("walking", 3);
        }

        else
        {
            myAnim.SetInteger("walking", 2);
        }

        
    }



    public void GoToPoint()
    {
        if (fleeing == false)
        {

       
        myAgent.SetDestination(patrolPoints[currentPoint].position);
        currentPoint++;
        if (currentPoint >= patrolPoints.Length)
        {
            currentPoint = 0;
        }
        }
    }

    void OnTriggerStay(Collider col)
    {

        if (myModes != NPCModes.Fleeing && hasDied == false)
        {

            myModes = NPCModes.Idling;
            lookTarget = col.gameObject.transform.position;

        }

        else
        {
            myAgent.isStopped = false;
            myAnim.SetInteger("walking", 2);
        }

    }

    void OnTriggerExit(Collider col)
    {

        GoToPoint();

        if (myModes == NPCModes.Idling)
        {
            myModes = NPCModes.Walking;
        }
        contact = false;
    }

    void BackToSound()
    {
        soundMade = false;
    }

    void Death()
    {
        myAgent.isStopped = true;
        if (charHealth.isDead == true && hasDied == false)
        {
            myAnim.SetInteger("walking", 10);
            hasDied = true;
            
        }

        myTrigger.enabled = false;
        
    }
}


