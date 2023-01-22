using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;  //�ȱ� ���ǵ�

    [SerializeField]
    private float lookSensitivity; //ī�޶� �ΰ���

    [SerializeField]
    private float cameraRotationLimit; //ī�޶� ȸ�� ���� ����. 360�� ȸ�� ���� ����.
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

    // Update is called once per frame  �� �����Ӹ��� ȣ��. 1�ʿ� �� 60��
    void Update()
    {
        Move();
        CameraRotation();
        CharacterRotation();
    }

    private void Move() 
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal"); //�¿� �̵�
        float _moveDirZ = Input.GetAxisRaw("Vertical");  //���� �̵�. Y���� ����

        Vector3 _moveHorizontal = transform.right * _moveDirX; //�¿�
        Vector3 _moveVertical = transform.forward * _moveDirZ;  //�յ��̵�

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;  //���� 1�� �ǵ��� ����ȭ. �̷��� �ϸ� �̵� ��� ���� ex. (0.5, 0, 0.5) = 1

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void CharacterRotation ()
    {
        //�¿� ĳ���� ȸ��
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
        currentCameraRotationX -= _cameraRotationX;  // �� ȸ�� ���� ����
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
