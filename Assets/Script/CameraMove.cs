using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    //Playable Characters
    GameObject[] Players;

    //Players에서 카메라가 따라다닐 캐릭터의 GameObject가 있는 인덱스
    //Index of selected PC
    int CameraFollowIndex;

    //ture = 마우스로 카메라 회전, false = 키보드로 카메라 회전
    //ture = rotate camera with mouse, false = rotate camera with keyboard
    public bool MouseLock = false;

    //Input Properties
    float MouseX = 0f;
    float MouseY = 0f;
    float Horizontal = 0f;
    float Vertical = 0f;
    float RotateSpeed = 2f;
    float Yaw = 0f;
    float Pitch = 45f;

	// Use this for initialization
	void Start () {
        Players = GameObject.FindGameObjectsWithTag("Player");

        //기본적으로 카메라가 따라다닐 캐릭터는 Knight
        //Default selected PC is Knight
        for (int i = 0; i < 3; ++i)
        {
            if (Players[i].name.Contains("Knight"))
            {
                CameraFollowIndex = i;
                break;
            }
            else
                CameraFollowIndex = 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
        FollowPC();
        Inputs();
        RotateCameraWithMouse();
        RoateCameraWithKeyboard();
    }

    //선택된 PC를 따라다닌다
    //Follow selected PC
    void FollowPC()
    {
        transform.position = Players[CameraFollowIndex].transform.position;
    }

    void Inputs()
    {
        //마우스
        //Mouse
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        //키보드
        //Keyboard
        Vertical = Input.GetAxis("Yaw");
    }

    //마우스로 카메라 회전
    //Rotate camera with mouse
    void RotateCameraWithMouse()
    {
        if (!MouseLock)
            return;

        Yaw += MouseX * RotateSpeed;
        //Pitch -= MouseY * RotateSpeed;

        transform.eulerAngles = new Vector3(Pitch, Yaw, 0f);
    }

    //키보드로 카메라 회전
    //Rotate camera with keyboard
    void RoateCameraWithKeyboard()
    {
        if (MouseLock)
            return;

        Yaw += Vertical * RotateSpeed;
        //Pitch += Horizontal * RotateSpeed;

        transform.eulerAngles = new Vector3(Pitch, Yaw, 0f);
    }
}
