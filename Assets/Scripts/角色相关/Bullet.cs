using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Bullet;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour, IPooledObject
{
    public float speed = 20;
    private float r = 20;
    public GameObject target;
    private float time = 0;
    private float offset = 1f;
    public void OnObjectSpawn()
    {
        target = FindClosestEnemy();
        transform.position = GameObject.Find("Shoot Root").transform.position;
        StartCoroutine(Move(transform.position, GetRandomPoint(r), target.transform ));
    }
    // Use this for initialization
    void Start()
    {
         
    }
    // Update is called once per frame
    void Update()
    {
        timek();
    }

    void OnTriggerEnter(Collider other)
    {
        int layA = LayerMask.NameToLayer("wall");
        if (other.gameObject.layer == layA || other.gameObject.tag == "finding")
        {if(other.gameObject.tag == "finding")
            {                                   }
            this.gameObject.SetActive(false);
        }
    }
    public Vector3 GetRandomPoint(float r)
    {
        return transform.position + new Vector3(Random.Range(-r/2, r/2), Random.Range(0, r/2), Random.Range(-r / 2, r / 2));
    }
    public IEnumerator Move(Vector3 start,Vector3 midPoint,Transform target)
    {
        for(float i=0;i<=1; i+=Time.deltaTime)
        {
            Vector3 p1=Vector3.Lerp(start,midPoint,i);
            Vector3 p2=Vector3.Lerp(midPoint,target.position + new Vector3(0, offset, 0), i);
            Vector3 p = Vector3.Lerp(p1, p2, i);

            yield return StartCoroutine(MoveToPoint(p));
        }
        yield return StartCoroutine(MoveToObejct(target));
    }

    IEnumerator MoveToPoint(Vector3 p)
    {
        yield return null;
        while(Vector3.Distance(transform.position,p)>0.1f)
        {
            Vector3 dir =p-transform.position;
            transform.up = dir;
            transform.position = Vector3.MoveTowards(transform.position, p, Time.deltaTime * speed);
            yield return null;
        }
    }

    IEnumerator MoveToObejct(Transform target)
    {yield return null;
        while (Vector3.Distance(transform.position, target.position + new Vector3(0, offset, 0)) > 0.1f)
        {
            Vector3 dir = target.position + new Vector3(0, offset, 0) - transform.position;
            transform.up = dir;
            transform.position = Vector3.MoveTowards(transform.position, target.position + new Vector3(0, offset, 0), Time.deltaTime * speed);
            yield return null;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    public interface IPooledObject
    {
        void OnObjectSpawn();
    }


    public GameObject FindClosestEnemy()
    {

        //查找标签为finding的全部游戏物体

        GameObject closest = GameObject.FindWithTag("finding");

        if (GameObject.FindGameObjectsWithTag("finding") != null)
        {

            GameObject[] gos;

            gos = GameObject.FindGameObjectsWithTag("finding");

            var distance = Mathf.Infinity;

            var position = transform.position;

            foreach (GameObject go in gos)
            {

                var diff = (go.transform.position - position); //计算player与Enemy的向量距离差

                var curDistance = diff.sqrMagnitude; //将向量距离平方(防止有负数产生)

                if (curDistance < distance)
                { //找出最近距离
                    closest = go; //更新最近距离敌人
                    distance = curDistance; //更新最近距离

                }
            }
        }
        return closest;
    }
    private void timek()
    {
        time += Time.deltaTime;
        if(time>5f)
        {
            this.gameObject.SetActive(false);
            time = 0;
        }
    }
}
