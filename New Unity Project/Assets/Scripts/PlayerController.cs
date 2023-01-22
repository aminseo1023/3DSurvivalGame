using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;  //걷기 스피드

    [SerializeField]
    private float lookSensitivity; //카메라 민감도

    [SerializeField]
    private float cameraRotationLimit; //카메라 회전 각도 제한. 360도 회전 막기 위함.
    private float currentCameraRotationX = 0;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;



    // Start is called before the first frame update
    void Start()
    {
        //theCamera = FindObjectOfType<Camera>();
        myRigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame  매 프레임마다 호출. 1초에 약 60번
    void Update()
    {
        Move();
        CameraRotation();
        CharacterRotation();
    }

    private void Move() 
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal"); //좌우 이동
        float _moveDirZ = Input.GetAxisRaw("Vertical");  //상하 이동. Y축은 점프

        Vector3 _moveHorizontal = transform.right * _moveDirX; //좌우
        Vector3 _moveVertical = transform.forward * _moveDirZ;  //앞뒤이동

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;  //합이 1이 되도록 정규화. 이렇게 하면 이동 계산 쉬움 ex. (0.5, 0, 0.5) = 1

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CharacterRotation ()
    {
        //좌우 캐릭터 회전
        float _yRotaton = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotaton, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
        Debug.Log(myRigid.rotation);
        Debug.Log(myRigid.rotation.eulerAngles);
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;  // 고개 회전 방향 반전
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
