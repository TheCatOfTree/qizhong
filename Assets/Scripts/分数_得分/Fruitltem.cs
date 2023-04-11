using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruitltem : MonoBehaviour
{
    public int score = 1;

    private BoxCollider _boxcollider;
    // Start is called before the first frame update
    void Start()
    {
        _boxcollider=GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            _boxcollider.enabled = false;
            GameController.instance.totalScore += score;
            this.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
