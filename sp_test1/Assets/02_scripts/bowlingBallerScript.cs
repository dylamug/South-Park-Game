using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowlingBallerScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bowlingBall;
    public GameObject clone;
    public Transform bowlingBaller;
    private Rigidbody myRig;
    
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject ballPrefab;
        public int size;

    }

    public static bowlingBallerScript Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<Pool> pools; 
    public Dictionary<string, Queue<GameObject>> bowlingPoolDictionary;
    void Start()
    {
        bowlingPoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject ball = Instantiate(pool.ballPrefab);
                ball.SetActive(false);
                objectPool.Enqueue(ball);
            }

            bowlingPoolDictionary.Add(pool.tag, objectPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
      

    }



    public void instantiateBowlingBall()
    {
        bowlingBallerScript.Instance.SpawnFromPool("Ball");
       
    }

    public GameObject SpawnFromPool (string tag)
    {

        if (!bowlingPoolDictionary.ContainsKey(tag))
        {
            Debug.Log("warning! warning!");
            return null;
        }
       GameObject clone = bowlingPoolDictionary[tag].Dequeue();

        clone.SetActive(true);
        clone.transform.position = transform.position;
        clone.transform.rotation = transform.rotation;
        myRig = clone.GetComponent<Rigidbody>();
        myRig.AddForce(transform.forward * 1000);

        bowlingPoolDictionary[tag].Enqueue(clone);

        return clone;

    }
}
