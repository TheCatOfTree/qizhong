using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AINavigate : MonoBehaviour
{
    private Transform Player;
    private new Animation animation;
    public Slider _slider;

    private new Rigidbody rigidbody;
    private float Radius;
    private float speed;
    private bool i = false;

    private Vector3 speed2;

    void Start()
    {

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animation = GetComponent<Animation>();
        rigidbody = GetComponent<Rigidbody>();
        Radius = 1000f;
        speed = 1f * 200;
    }

    void Update()
    {
        if (_slider.value > 0.1f)
        {
            if (gameObject.tag == "finding")
                MoveToPlayer();
            if (gameObject.tag == "unfinding")
                Stop();
        }
    }

    void MoveToPlayer()
    {
        if (gameObject.CompareTag("finding"))
        {
            if (Player != null)
            {
                float distance = (transform.position - Player.position).sqrMagnitude;
                if (distance < Radius)
                {
                    animation.Play("fly");
                    i = true;
                    transform.LookAt(Player.position);
                    if (transform.position.y < 3.5)
                    {
                        Vector3 move = (Player.position - transform.position).normalized;
                        rigidbody.velocity = move * speed * Time.deltaTime;
                    }
                }
            }
        }

    }

    private void Stop()
    {
        if (i)
            animation.Play("landing");
        i = false;
        rigidbody.velocity = Vector3.zero;
    }
}
