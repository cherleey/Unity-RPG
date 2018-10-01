using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour {

    //CameraPoint
    public GameObject Camera;

    //true = PC, false = NPC
    public bool AIEnable = false;

    //ture = mouse move enable, false = mouse move disable
    public bool MouseMoveEnable = true;

    //Input properties
    float Horizontal = 0f;
    float Vertical = 0f;
    float Speed = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Inputs();
        MovePCWithKeyboard();
        MovePCWithMouse();
        MoveNPC();
	}

    void Inputs()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
    }

    //키보드로 PC 이동
    //Move PC with keyboard
    void MovePCWithKeyboard()
    {
        if (AIEnable || MouseMoveEnable)
            return;

        //키보드 이동 시 forward 벡터를 카메라의 forward 벡터와 일치시킨다
        //Change the forward vector to the forward vector of camera when move the character with keyboard
        //if (Horizontal != 0f || Vertical != 0f)
        //    transform.forward = new Vector3(Camera.transform.forward.x, 0f, Camera.transform.forward.z);
        if (Horizontal != 0f || Vertical != 0f)
        {
            Quaternion NewDir = Quaternion.Lerp(transform.rotation, new Quaternion(0f, Camera.transform.rotation.y, 0f, Camera.transform.rotation.w), 0.3f);
            transform.rotation = NewDir;
        }

        transform.Translate(Horizontal * Speed, 0f, Vertical * Speed);
    }

    //마우스로 PC 이동
    //Move PC with mouse
    void MovePCWithMouse()
    {
        if (AIEnable || !MouseMoveEnable)
            return;
    }

    //NPC 이동
    //Move NPC
    void MoveNPC()
    {
        if (!AIEnable)
            return;
    }
}
