using System.Collections;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    public float Pitch { get; private set; }
    public float Yaw { get; private set; }
    public float MouseSensitityvity=5;
    public float cameraRotatingSpeed = 40;
    public float cameraYSpeed = 5;
    private Transform _target;
    private Transform _camera;
    [SerializeField]
    public AnimationCurve _armLengthCurve;
    private void Start()
    {

    }
    private void Awake()
    {
        _camera = transform.GetChild(0);
    }
    public void InitCamera(Transform target)
    {
        _target = target;
        transform.position = _target.position;
    }
    void Update()
    {
        if(Cursor.lockState != CursorLockMode.None)UpdateRotation();
         UpdatePosition();
         UpdateArmLength();
    }
    private void UpdateRotation()
    {
       Yaw+= Input.GetAxis("Mouse X")*MouseSensitityvity;
        Yaw += Input.GetAxis("Camera Rate X")*cameraRotatingSpeed*Time.deltaTime;
        Pitch += Input.GetAxis("Mouse Y") * MouseSensitityvity;
        Pitch+= Input.GetAxis("Camera Rate Y") * cameraRotatingSpeed * Time.deltaTime;
        Pitch = Mathf.Clamp(Pitch, -90, 90);
        transform.rotation = Quaternion.Euler(Pitch, Yaw, 0);
    }
    private void UpdatePosition()
    {
        Vector3 position = _target.position;
        float newY=Mathf.Lerp(transform.position.y,position.y, Time.deltaTime*cameraYSpeed);
        transform.position = new Vector3(position.x, newY,position.z);
    }
    private void UpdateArmLength()
    {
        _camera.localPosition = new Vector3(0, 0, _armLengthCurve.Evaluate(Pitch)*-1);
    }
}
