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
    private bool isDown;
    public Transform target;
    public float r = 10;
    private float time;
    public float firetime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        objectPool = ObjectPool.Instance;
 isDown = false;
    }
    void Timer()
    {if(Input.GetMouseButtonDown(0))
            isDown = true;
    if(Input.GetMouseButtonUp(0))
            isDown=false;
        if (isDown) //按下时开始计时
        {
            time += Time.deltaTime;
        }
        else if (!isDown && time > 2f)   //蓄力完成&蓄力2秒以上
        {
            Debug.Log("本次蓄力时长：" + time);
            time = time > 3f ? time = 3 : time;  //限制有效蓄力为2—3秒

            StartFire();
            StartFire();
            StartFire();
            time = 0;
        }   
        else if (!isDown && time > 0 && time <= 0.5) //蓄力小于0.5秒
        {
            Debug.Log("本次蓄力时长：" + time);
            Debug.Log("释放轻击");
            time = 0;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //time+=Time.deltaTime;
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (time > firetime)
        //    {
        //        StartFire();
        //        StartFire();
        //        StartFire();
        //        StartFire();
        //        StartFire();
        //        StartFire();
        //        StartFire();
        //    time=0; 
        //    }
        //    //buttle.transform.position += GameObjectManager._Instance.m_dic[GameObjectName.g_ButtleName][i].transform.forward * buttleSpeed * Time.deltaTime;
        //}
        Timer();
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

