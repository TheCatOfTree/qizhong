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
    private float firetime =3f;
    private GameObject father;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        objectPool = ObjectPool.Instance;
        father = transform.parent.gameObject;
        lasttime = Time.time;
        _animator = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (father.gameObject.tag == "finding")
        {
            if (lasttime + firetime < Time.time)
            {
                lasttime = Time.time;

                _animator.SetBool("attack", true);
                StartFire();
                StartFire();

            }
            else
            {
                _animator.SetBool("attack", false);

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

