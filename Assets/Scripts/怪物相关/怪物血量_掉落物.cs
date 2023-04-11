using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class 怪物血量_掉落物 : MonoBehaviour
{
    ObjectPool objectPool;
    public Slider _slider;
    public GameObject _gameObject;
    public float time=0;
    public Animation _anima;
    public Animator _animator;
    public GameObject Head;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = ObjectPool.Instance;
        _slider.value = 1;
        _gameObject = GameObject.Find("FollowCamera");
        _anima = this.GetComponent<Animation>();
        _animator=this.GetComponent<Animator>();
        
    }
    void OnTriggerEnter(Collider other)
    {
        
            if (other.gameObject.tag == "子弹")
            { _slider.value -= 0.2f; }

    }
    // Update is called once per frame
    void Update()
    {
        updateHp();
        dead();
    }
    private void updateHp()
    {
        _slider.gameObject.transform.position = Head.transform.position;
        _slider.gameObject.transform.LookAt(_gameObject.transform.position);
    }
    private void dead()
    {
        if (_slider.value < 0.05f)
        {
            _slider.gameObject.SetActive(false);
            time += Time.deltaTime;
            if (_anima != null)
            {
                _anima.Play("death1");
            
            }
            else
            {
                _animator.SetBool("death", true);
            }
            while (time>0.9f)
            {
                this.gameObject.SetActive(false);
                objectPool.SpawnFromPool("linshi", transform.position, Quaternion.identity);
                time = 0;
                if (_animator != null)
                    _animator.SetBool("death", false);
            }
        }
    }
}
