using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMove : MonoBehaviour {

    //Options
    public GameObject Options;

    //CameraPoint
    public GameObject CameraPoint;

    //Navmeshagent
    NavMeshAgent Agent;

    //Animator
    Animator Anim;

    //Input properties
    float Horizontal = 0f;
    float Vertical = 0f;
    float Speed = 0.2f;

    //true -> PC, false -> NPC
    public bool AIEnable;

    //NPC의 타겟 캐릭터
    //Target character for NPC
    GameObject NPCTarget = null;

    // Use this for initialization
    void Start () {
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Inputs();
        MovePCWithKeyboard();
        MovePCWithMouse();
        MoveNPC();
        AnimationControl();
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
        if (AIEnable || Options.GetComponent<Options>().MouseMoveEnable)
            return;

        //키보드 이동 시 forward 벡터를 카메라의 forward 벡터와 일치시킨다
        //Change the forward vector to the forward vector of camera when move the character with keyboard
        //if (Horizontal != 0f || Vertical != 0f)
        //    transform.forward = new Vector3(Camera.transform.forward.x, 0f, Camera.transform.forward.z);
        if (Horizontal != 0f || Vertical != 0f)
        {
            Quaternion NewDir = Quaternion.Lerp(transform.rotation, new Quaternion(0f, CameraPoint.transform.rotation.y, 0f, CameraPoint.transform.rotation.w), 0.3f);
            transform.rotation = NewDir;
        }

        transform.Translate(Horizontal * Speed, 0f, Vertical * Speed);
    }

    //마우스로 PC 이동
    //Move PC with mouse
    void MovePCWithMouse()
    {
        if (AIEnable || !Options.GetComponent<Options>().MouseMoveEnable)
            return;

        Vector3 ClickedPoint;

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit Hit;
            Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(Ray, out Hit))
                ClickedPoint = Hit.point;

            Agent.destination = Hit.point;
        }
    }

    //NPC 이동
    //Move NPC
    void MoveNPC()
    {
        if (!AIEnable)
            return;
    }

    //NPC AI
    void NPCAI()
    {
        if (!AIEnable)
            return;

        if(NPCTarget != null)
        {
            Agent.destination = NPCTarget.transform.position;
        }
    }

    //애니메이션 전환
    //Change animation
    void AnimationControl()
    {
        //Run
        if (Agent.velocity.magnitude != 0)
            Anim.SetBool("Moving", true);
        else
            Anim.SetBool("Moving", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(NPCTarget == null && other.tag == "Player")
            NPCTarget = other.gameObject;


    }
}
