using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicController : MonoBehaviour
{


    float vertSpeed = 3;
    float runSpeed = 7;
    float rotSpeed = 100;
    float translation;
    float rotation;
    float moveSpeed;
   public bool isAttacking = false;
    public woodenSwordScript swordScript;
    public GameObject bballer;
    private bowlingBallerScript b_script;

    
   
    private float swordAttackCooldown = 1;
    private float t;


    public GameObject woodenSword;
    public GameObject joint;


    public enum Weapons {Sword, Joint, Flamethrower, BowlingBall };
    public Weapons myWeapons;

    Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        myWeapons = Weapons.Sword;
        b_script = bballer.GetComponent<bowlingBallerScript>();

        t = 0;

        
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       
        

        switch (myWeapons)
        {
            case Weapons.Sword:
                woodenSword.SetActive(true);
                joint.SetActive(false);
                break;

            case Weapons.Joint:
                woodenSword.SetActive(false);
                joint.SetActive(true);
                break;

            case Weapons.Flamethrower:
               
                break;
            case Weapons.BowlingBall:
                
                woodenSword.SetActive(false);
                joint.SetActive(false);


                break;
        }

      

        choosingWeapons();

        combatManager();

        movement();

    }

    void movement()
    {
        translation = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;



        rotation = Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;





        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
        if (isAttacking)
        {
            moveSpeed = 0;

            rotation = 0;
            Debug.Log("dongs");
        }
        else
        {



            if (Input.GetKey(KeyCode.W))
            {

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    translation = Input.GetAxis("Vertical") * runSpeed * Time.deltaTime;
                    myAnim.SetInteger("Walking", 2);
                    moveSpeed = runSpeed;
                }


                else
                {
                    myAnim.SetInteger("Walking", 1);
                   translation = Input.GetAxis("Vertical") * vertSpeed * Time.deltaTime;
                    moveSpeed = vertSpeed;
                }
            }

            else if (Input.GetKey(KeyCode.S))
            {
                myAnim.SetInteger("Walking", -1);
            }


            else
            {
                myAnim.SetInteger("Walking", 0);
            }

            if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
            {
                myAnim.SetTrigger("Dodge");
            }
        }
    }

    void finishedAttacking()
    {
        isAttacking = false;
        myAnim.SetInteger("Walking", 0);
        swordScript.turnOffCollider();
        
    }

    void startAttacking()
    {
        swordScript.turnOnCollider();
    }

    void callBowlingScript()
    {


        b_script.instantiateBowlingBall();
     
    
    }

    void choosingWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myWeapons = Weapons.Sword;

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            myWeapons = Weapons.Joint;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            myWeapons = Weapons.BowlingBall;
        }
    }

    void combatManager()
    {
        Debug.Log(t);

        if (Input.GetKeyDown(KeyCode.Mouse0) && t <= Time.time)
        {


            t = 0.7f + Time.time;

            isAttacking = true;

            if (myWeapons == Weapons.Sword)
            {

                myAnim.SetTrigger("SwordAttack");
                

            


            }

            if (myWeapons == Weapons.Joint)
            {
                myAnim.SetInteger("Walking", 4);
            }

            if (myWeapons == Weapons.BowlingBall)
            {
                myAnim.SetInteger("Walking", 8);
            }

        }

    }

    
}
