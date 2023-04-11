using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UI;

public class linshi : MonoBehaviour
{




    private float _a = 0.0f;
    private float _b = 0.0f;
    public float jumpDuration = 1f; //弹起的时间
    public float height = 2; //弹起的高度
    private float _curTime = 0.0f;
    public int count = 1; //弹起的次数
    private Vector3 _homePos = Vector3.zero;
    private Vector3 _tempPos = Vector3.zero;
    void Start()
    {
        _homePos = transform.position;
        _homePos.y = -1.1f;
        CalculateAAndB();
    }


    private void CalculateAAndB()
    {
        _a = -4 * height / Mathf.Pow(jumpDuration, 2);
        _b = _a * (-1) * jumpDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject)
        {
            if (_curTime < jumpDuration)
            {
                _curTime += Time.deltaTime;
                _tempPos = _homePos + (_a * Mathf.Pow(_curTime, 2) + _b * _curTime) * Vector3.up;
                transform.position = _tempPos;
            }
            else if (count > 0)
            {
                count = count - 1;
                _curTime = 0;
                height = 0.5f * height;
                jumpDuration = 0.5f * jumpDuration;
                CalculateAAndB();
            }
        }
    }
}
