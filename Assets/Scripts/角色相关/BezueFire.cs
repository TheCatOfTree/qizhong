using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class BezueFire : MonoBehaviour
{
    ObjectPool objectPool;
    public Transform target;
    public float r = 10;
    private float time;
    public float firetime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        objectPool = ObjectPool.Instance;
 
    }

    // Update is called once per frame
    void Update()
    {
        time+=Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            if (time > firetime)
            {
                StartFire();
                StartFire();
                StartFire();
                StartFire();
                StartFire();
                StartFire();
                StartFire();
            time=0; 
            }
            //buttle.transform.position += GameObjectManager._Instance.m_dic[GameObjectName.g_ButtleName][i].transform.forward * buttleSpeed * Time.deltaTime;
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
        objectPool.SpawnFromPool("Bullet", transform.position, Quaternion.identity);
        yield return null;

    }

}

    // Update is called once per frame

