using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{

    public Transform playerLocation;
    public Vector3 bulletTarget;
    Rigidbody myRig;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
       
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();


        foreach (Pool pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {

       
        
            GameObject objectToSpawn = poolDictionary[tag].Dequeue();
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
        objectToSpawn.transform.LookAt(playerLocation);
        objectToSpawn.transform.localEulerAngles = new Vector3(0, objectToSpawn.transform.localEulerAngles.y, objectToSpawn.transform.localEulerAngles.z);



        //myRig = objectToSpawn.GetComponent<Rigidbody>();
        //myRig.AddForce(bulletTarget * 300);


        poolDictionary[tag].Enqueue(objectToSpawn);
        

        return objectToSpawn;

    }


}
