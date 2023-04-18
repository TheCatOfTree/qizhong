using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum FadeStatuss
{
    FadeIn,
    FadeOut
}
public class setManhua : MonoBehaviour
{
    public Image P1;
    private int i;
    private float m_Alpha;
    //淡入淡出状态
    private FadeStatuss m_Statuss;
    //效果更新的速度
    public float m_UpdateTime;
    //中间停滞时间
    private float m_Time;
    // Start is called before the first frame update
    void Start()
    {
        i = 1;
        m_UpdateTime = 0.5f;
        m_Statuss = FadeStatuss.FadeIn;
        Unshow();
        Getimage();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (m_Statuss == FadeStatuss.FadeIn)
        {
            m_Alpha += m_UpdateTime * Time.deltaTime;
            m_Time = 0;
        }
        else if (m_Statuss == FadeStatuss.FadeOut&&m_Time>2)
        {
            m_Alpha -= m_UpdateTime * Time.deltaTime;
        }
        UpdateColorAlpha();
        m_Time+=Time.deltaTime;
    }
    void UpdateColorAlpha()
    {
        //获取到图片的透明值
        Color ss = P1.color;
        ss.a = m_Alpha;
        //将更改过透明值的颜色赋值给图片
        P1.color = ss;
        //透明值等于的1的时候 转换成淡出效果
        if (m_Alpha > 1f)
        {
            m_Alpha = 1f;
            m_Statuss = FadeStatuss.FadeOut;
        }
        if(m_Alpha<0)
        {
            m_Statuss=FadeStatuss.FadeIn;
            i++;
            Getimage();
        }
        if(i==6&&m_Alpha<0)
        {
            SceneManager.LoadScene(2);
        }

    }
    private void Unshow()
    {
        while (i <= 5)
        {
            P1 = GameObject.Find(i.ToString()).GetComponent<Image>();
            Color ss = P1.color;
            ss.a = 0;
            P1.color = ss;
            i++;
        }
        i = 1;
    }
    private void Getimage()
    {
        if(i<6)
        P1 = GameObject.Find(i.ToString()).GetComponent<Image>();
       
    }

}
