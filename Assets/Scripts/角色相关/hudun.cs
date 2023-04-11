using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static UnityEngine.GraphicsBuffer;

public class hudun : MonoBehaviour 
{
    public AnimationCurve curve;//放大的曲线

    private float value = 0;
    // Update is called once per frame
    void Start()
    {

        

    }
    void OnEnable()
    {
        if (this.gameObject.activeSelf == true)
        {
            StartCoroutine(buttonAnimation());
        }
    }



    IEnumerator buttonAnimation()
    {
        value = 0;
        while (true)
        {
            this.transform.localScale = new Vector3(1.77f, 1.61f, 0.53f) * curve.Evaluate(value += Time.deltaTime * 5); 
            yield return null;
            if (value >= 1)
            {
                break;
            }
        }
    }

}
