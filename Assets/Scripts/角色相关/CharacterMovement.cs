using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float jumpForce = 1600f;
    public Vector3 CurrentInput { get; private set; }
    public float MaxWalkSpeed = 5;
    private int jumpLimit = 1;
    private int turnspeed = 5;
    public Animator Animation;
    Vector3 move;
    // Start is called before the first frame update
    private void Start()
    {
        Animation = GameObject.Find("abaofinal").GetComponent<Animator>();
    }
    private void Awake()
    {
        //GameObject.Find("abao1").GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        if (CurrentInput.magnitude != 0)
        {

            Quaternion quaDir = Quaternion.LookRotation(CurrentInput, Vector3.up);
            //缓慢转动到目标点
            transform.rotation = Quaternion.Lerp(transform.rotation, quaDir, Time.fixedDeltaTime * turnspeed);
        }
        _rigidbody.MovePosition(_rigidbody.position + CurrentInput * MaxWalkSpeed * Time.fixedDeltaTime);



    }

    private void Update()
    {
        if (IsGrounded())                       //如果接触地面，则恢复可跳跃次数
        {
            jumpLimit = 1;
            Animation.SetBool("jump", false);
        }
        Jump();

    }
    public void SetMovementInput(Vector3 input)
    {

        CurrentInput = Vector3.ClampMagnitude(input, 1);
        if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
        {
            Animation.SetBool("MaxWalkSpeed", true);

        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) { Animation.SetBool("MaxWalkSpeed", false); }
        //
        //这里写运动
    }
    bool IsGrounded()                   //通过射线检测角色是在地面或者物体(角色的零点需要设置在脚底处)
    {
       
        return Physics.Raycast(transform.position, -Vector3.up, 0.25f);
    }

    void Jump()
    {


        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
           
            if (jumpLimit > 0)
            {
                Animation.SetBool("jump", true);
                Animation.SetBool("MaxWalkSpeed", false);
                //transform.position = new Vector3(transform.position.x, 0.6f+transform.position.y, transform.position.z);
                GetComponent<Rigidbody>().AddRelativeForce(transform.up * 12f * jumpForce);
                  
                //Animation.SetBool("jumping", true);//这行代码改成跳跃
                jumpLimit--;

            }

        }





    }

}
