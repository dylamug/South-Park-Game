using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooter : MonoBehaviour
{

    public Transform playerLocation;
    Transform bulletTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireGun()
    {
        ObjectPooler.Instance.SpawnFromPool("Bullet", transform.position, Quaternion.identity);
    }
}
