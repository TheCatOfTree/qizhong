using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Bullet;
public class PlayCharacter : MonoBehaviour
{
    private CharacterMovement _characterMovement;

    [SerializeField]
    public Camerafollow _camerafollow;
    // Start is called before the first frame update
    [SerializeField]
    private Transform _followingTarget;
    private GameObject HuDun;
    private void Awake()
    {
        _characterMovement= GetComponent<CharacterMovement>();
        _camerafollow.InitCamera(_followingTarget);
        Cursor.lockState = CursorLockMode.Locked;
        HuDun = GameObject.Find("Cube").gameObject;


    }
    private void Start()
    {
        HuDun.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Cursor.lockState != CursorLockMode.None) UpdateMovementInput();
        ShowMouse();
    }
    private void UpdateMovementInput()
    {
        Quaternion rot=Quaternion.Euler(0,_camerafollow.Yaw,0);
        
        _characterMovement.SetMovementInput(rot * Vector3.forward * Input.GetAxis("Vertical") +
                                            rot * Vector3.right * Input.GetAxis("Horizontal"));
    }
    public void ShowMouse()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            HuDun.SetActive(true);
            
        } else if (Input.GetMouseButtonUp(1))
        {
            HuDun.SetActive(false);
        }
        
    }

}
