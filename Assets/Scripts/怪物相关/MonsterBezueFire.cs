using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class MonsterBezueFire : MonoBehaviour
{
    ObjectPool objectPool;
    public Transform target;
    public float r = 10;
    private float lasttime;
    private float firetime = 0.02f;
    private GameObject father;
    // Start is called before the first frame update
    void Start()
    {
        objectPool = ObjectPool.Instance;
        father = transform.parent.gameObject;
        lasttime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (father.gameObject.tag == "finding")
        {
            if(lasttime+firetime<Time.time)
            {
                lasttime = Time.time;
                StartFire();
                StartFire();
               
            }
           
        }
    }


    public void StartFire()
    {
        StartCoroutine(Fire());
    }
    public void StopFire()
    {
        StopAllCoroutines();
    }

    
    IEnumerator Fire()
    {
        objectPool.SpawnFromPool("MonsterBullet", transform.position, Quaternion.identity);
        yield return null;

    }

}

    // Update is called once per frame

