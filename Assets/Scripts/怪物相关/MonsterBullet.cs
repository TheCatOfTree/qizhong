using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static MonsterBullet;
using static UnityEngine.GraphicsBuffer;

public class MonsterBullet : MonoBehaviour, IPooledObjectMonster
{
    public float speed = 20;
    private float r = 20;
    public GameObject target;
    private float time = 0;
    private float offset = 1f;
    public void OnObjectSpawn()
    {
        target = FindClosestEnemy();
        StartCoroutine(Move(transform.position, GetRandomPoint(r), target.transform));
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

    void OnTriggerEnter(Collider other)//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    {
        int layA = LayerMask.NameToLayer("wall");
        int layB = LayerMask.NameToLayer("HuDun");
        if (other.gameObject.layer == layA || other.gameObject.tag == "Player"||other.gameObject.layer == layB)
        {
            if(other.gameObject.tag == "Player")
            {      /*other.GetComponent<Slider>*/     }
            this.gameObject.SetActive(false);
        }
    }
    public Vector3 GetRandomPoint(float r)
    {
        return transform.position + new Vector3(Random.Range(-r/4, r/4), Random.Range(0, r/4), Random.Range(0, r / 4));
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
    public interface IPooledObjectMonster
    {
        void OnObjectSpawn();
    }


    public GameObject FindClosestEnemy()
    {
        GameObject closest = GameObject.FindWithTag("Player");
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
