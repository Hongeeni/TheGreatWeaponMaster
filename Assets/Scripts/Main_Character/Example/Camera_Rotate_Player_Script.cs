using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Rotate_Player_Script : MonoBehaviour
{
    //카메라 중심 축 객체.
    public GameObject playerCameraAngle;

    //플레이어 X축 카메라 회전 속도.
    private float playerXRotateSpeed = 2.5f;
    //플레이어 Y축 카메라 회전 속도.
    private float playerYRotateSpeed = 7.0f;

    //플레이어 카메라 회전 각도.
    private float playerYCameraRotate = 0f;
    //플레이어 현재 각도.
    private float playerCurrentCameraRotate = 0f;
    //카메라 회전 각도 한계.
    private float playerRotateLimit = 60.0f;

    private void Awake()
    {
        //커서를 화면 중앙에 고정.
        Cursor.lockState = CursorLockMode.Locked;
        //커서가 화면에 보이지 않게 설정.
        Cursor.visible = false;
    }

    private void Update()
    {
        //게임의 시간이 흘러가고 있는지 확인.
        if (Time.timeScale != 0)
        {
            //GetAxis를 이용하여 마우스의 X축 변화량을 받아 playerXRotateSpeed값에 곱해 객체의 회전 각도를 저장
            float xRot = Input.GetAxis("Mouse X") * playerXRotateSpeed;
            //카메라의 각도 값을 현재 각도 값에 곱하여 설정
            this.transform.localRotation *= Quaternion.Euler(0f, xRot, 0f);

            //카메라의 Y축 회전 각도 값을 playerYCameraRotate에 저장.
            playerYCameraRotate = Input.GetAxis("Mouse Y") * playerYRotateSpeed;
        }
    }

    private void FixedUpdate()
    {
        //PreformRotation함수를 통해 카메라의 각도를 업데이트.
        PreformRotation();
    }

    //플레이어 카메라의 각도를 설정하는 함수.
    private void PreformRotation()
    {
        //카메라의 회전 각도를 current_camera_rotate에 저장.
        playerCurrentCameraRotate -= playerYCameraRotate;
        //Mathf를 이용하여 회전 각도가 한계 값 범위 안에 있는지 확인하고 실제 변경 각도를 playerCurrentCameraRotate에 저장
        playerCurrentCameraRotate = Mathf.Clamp(playerCurrentCameraRotate, -playerRotateLimit, playerRotateLimit);
        //카메라의 각도 값을 변경
        playerCameraAngle.transform.localEulerAngles = new Vector3(playerCurrentCameraRotate, 0f, 0f);
    }
}
