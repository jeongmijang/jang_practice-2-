using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;


[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]


public class Player_h : LivingEntity
{
    public float speed; // 플레이어 이동 속도
    public GameObject[] weapons; // 무기들을 담을 배열 함수 선언
    public bool[] hasWeapons; // 플레이어가 가지고 있는 무기 여부를 나타내는 배열
    float hAxis; // 수평 입력값
    float vAxis; // 수직 입력값

    bool wDown; // 걷기 입력 여부
    bool iDown; // 키보드 E 키로 오브젝트와 상호작용 하는 경우가 많아 그렇게 만들어주고자 설정함
    bool sDown1; // 무기 스왑1 입력 여부. 슈팅 게임에서 장비 고를 때, 1번 2번 등 키를 눌러서 무기 교체하는데 그 기능 설정하기 위함
    bool sDown2; // 무기 스왑2 입력 여부

    bool isSwap; // 무기 스왑 중 여부. 교체 시간차를 위한 플래그 로직을 작성하기 위해 선언

    Vector3 moveVec; // 이동 벡터

    GameObject nearObject; // 주변에 있는 상호작용 가능한 오브젝트.  트리거 된 아이템을 저장하기 위한 변수 선언
    GameObject equipWeapon; // 현재 장착한 무기 오브젝트. 기존에 장착된 무기를 저장하는 변수를 선언하고 활용하기 위해 사용
    int equipWeaponIndex = -1; // 현재 장착한 무기의 인덱스.  0으로 설정할 경우, 해머를 0으로 설정해놨기 때문에 해머를 먹어도 해머를 생성해낼 수 없음

    Animator anim; // 플레이어의 애니메이터 컴포넌트

    Camera viewCamera; // 메인 카메라
    PlayerController controller; // 플레이어 컨트롤러 컴포넌트
    GunController gunController; // 총기 컨트롤러 컴포넌트

    //현재 플레이어가 쓰는 컴포넌트를 다 가져와라
    //protected override void Start()
    //{
    //    base.Start();
    //    controller = GetComponent<PlayerController>(); // 플레이어 컨트롤러 컴포넌트 가져오기
    //    gunController = GetComponent<GunController>(); // 총기 컨트롤러 컴포넌트 가져오기
    //    viewCamera = Camera.main; // 메인 카메라 가져오기

    //    void Start()
    //    {
    //        controller = GetComponent<PlayerController>(); // 플레이어 컨트롤러 컴포넌트 가져오기
    //        gunController = GetComponent<GunController>(); // 총기 컨트롤러 컴포넌트 가져오기
    //        viewCamera = Camera.main; // 메인 카메라 가져오기
    //        anim = GetComponentInChildren<Animator>();
    //    }


    //    void Update()
    //    {
    //        // 업데이트 항목이 너무 늘어지니까 그냥 함수 선언 
    //        GetInput();
    //        Move();
    //        Interaction();
    //        Swap();
    //    }


    //    //키보드 단축키 눌러서 작용하는 것들에 관하여 (이동, e누르면 줍기, 1/2번은 무기 단축키)
    //    void GetInput() // 유니티 상단 탭에 Edit - Project Settings - Input Manager(가상 축과 버튼을 관리하는 곳) - Axes(가상 축) 누르고 - Size가 갯수 조절
    //    {
    //        hAxis = Input.GetAxisRaw("Horizontal"); // 수평 입력값 받아오기
    //        vAxis = Input.GetAxisRaw("Vertical"); // 수직 입력값 받아오기
    //        wDown = Input.GetButton("Walk"); // 걷기 입력 여부 받아오기 (shift 누르면 속도 완화)
    //        iDown = Input.GetButtonDown("Interaction"); // 상호작용 입력 여부 받아오기
    //        sDown1 = Input.GetButtonDown("Swap1"); // 무기 스왑1 입력 여부 받아오기
    //        sDown2 = Input.GetButtonDown("Swap2"); // 무기 스왑2 입력 여부 받아오기
    //        Debug.Log("t");
    //    }





    //    //이동에 관하여 (대각선이동시 속도관련, 걷기)
    //    void Move()
    //    {
    //        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // 입력값으로 이동 벡터 설정
    //                                                           // normalized는 대각선으로 이동 시에도 같은 속도를 내기 위해 작성

    //        /*if(isSwap) // 움직이면서 무기 스왑을 못 하게 설정할 때 사용
    //        {
    //            moveVec = Vector3.zero;
    //        }
    //        */

    //        if (wDown) // 걷기 속도 조절 (뛰는거랑 걷는거랑 속도 차이 없으면 안되니까)
    //            transform.position += moveVec * speed * 0.3f * Time.deltaTime; // 걷기 중인 경우 속도 감소
    //        else
    //        {
    //            transform.position += moveVec * speed * Time.deltaTime; // 걷기가 아닌 경우 정상 속도로 이동
    //        }

    //        anim.SetBool("isRun", moveVec != Vector3.zero); // 달리는 상태 애니메이션 설정
    //        anim.SetBool("isWalk", wDown); // 걷기 상태 애니메이션 설정

    //        // LookAt : 지정된 벡터를 향해서 회전시켜주는 함수
    //        transform.LookAt(transform.position + moveVec); // 우리가 나아가는 방향으로 바라보게 만들기 (현재 위치+가는 방향)
    //    }





    //    // 무기 조작 입력 (마우스 누르면 총알나옴)
    //    if (Input.GetMouseButton(0))
    //    {
    //        gunController.Shoot();
    //    }




    //    //무기 변경관련 (교체할때)
    //    void Swap()
    //    {
    //        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) // 무기 중복 교체, 없는 무기 확인을 위한 조건 추가
    //            return;
    //        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
    //            return;

    //        int weaponIndex = -1;
    //        if (sDown1) weaponIndex = 0;
    //        if (sDown2) weaponIndex = 1;

    //        if (sDown1 || sDown2) // 단축키 둘 중 하나만 눌러도 되도록 or 조건 작성
    //        {
    //            if (equipWeapon != null)
    //                equipWeapon.SetActive(false); // 여기부터 '캐릭터가 빈 손일 경우'를 고려하지 않으면 null 오류가 생길 수 있음
    //                                              // 현재 장착한 무기 비활성화

    //            equipWeaponIndex = weaponIndex;
    //            equipWeapon = weapons[weaponIndex]; // 새로운 무기 할당
    //            equipWeapon.SetActive(true); // 새로운 무기 활성화

    //            anim.SetTrigger("doSwap"); // 스왑 애니메이션 재생

    //            isSwap = true;

    //            Invoke("SwapOut", 0.4f); // 스왑 중 상태 변경 (일정 시간 후)
    //        }

    //    }

    //    //default 기본 상태로 돌아오는것같아.
    //    void SwapOut()
    //    {
    //        isSwap = false; // 스왑 중 상태 해제
    //    }



    //    //e키 눌렀을때 근처에 아이템이 있으면 줍는거. 근데 그 아이템이 이미 보유아이템이면 기존 아이템 파괴
    //    void Interaction()
    //    {
    //        if (iDown && nearObject != null)
    //        {
    //            if (nearObject.tag == "Weapon")
    //            {
    //                Item item = nearObject.GetComponent<Item>();
    //                int weaponIndex = item.value;
    //                hasWeapons[weaponIndex] = true; // 무기를 주웠으므로 해당 무기 보유 여부를 true로 설정

    //                Destroy(nearObject); // 주웠던 무기 오브젝트 파괴
    //            }
    //        }
    //    }




    //    void OnTriggerStay(Collider other)  // 3D 플레이어와 접촉중
    //    {
    //        if (other.tag == "Weapon")
    //            nearObject = other.gameObject;

    //        Debug.Log(nearObject.name); // 주변에 있는 상호작용 가능한 오브젝트 이름 로그 출력
    //    }

    //    void OnTriggerExit(Collider other)  // 3D 플레이어와 접촉끝
    //    {
    //        if (other.tag == "Weapon")
    //            nearObject = null; // 주변에 있는 상호작용 가능한 오브젝트 없음으로 설정

    //    }

    //}
    void Start()
    {
        controller = GetComponent<PlayerController>(); // 플레이어 컨트롤러 컴포넌트 가져오기
        gunController = GetComponent<GunController>(); // 총기 컨트롤러 컴포넌트 가져오기
        viewCamera = Camera.main; // 메인 카메라 가져오기
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        // 업데이트 항목이 너무 늘어지니까 그냥 함수 선언 
        GetInput();
        Move();
        Interaction();
        Swap();

        // 무기 조작 입력 (마우스 누르면 총알나옴)
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }


    //키보드 단축키 눌러서 작용하는 것들에 관하여 (이동, e누르면 줍기, 1/2번은 무기 단축키)
    void GetInput() // 유니티 상단 탭에 Edit - Project Settings - Input Manager(가상 축과 버튼을 관리하는 곳) - Axes(가상 축) 누르고 - Size가 갯수 조절
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // 수평 입력값 받아오기
        vAxis = Input.GetAxisRaw("Vertical"); // 수직 입력값 받아오기
        wDown = Input.GetButton("Walk"); // 걷기 입력 여부 받아오기 (shift 누르면 속도 완화)
        iDown = Input.GetButtonDown("Interaction"); // 상호작용 입력 여부 받아오기
        sDown1 = Input.GetButtonDown("Swap1"); // 무기 스왑1 입력 여부 받아오기
        sDown2 = Input.GetButtonDown("Swap2"); // 무기 스왑2 입력 여부 받아오기
    }





    //이동에 관하여 (대각선이동시 속도관련, 걷기)
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // 입력값으로 이동 벡터 설정
                                                           // normalized는 대각선으로 이동 시에도 같은 속도를 내기 위해 작성

        /*if(isSwap) // 움직이면서 무기 스왑을 못 하게 설정할 때 사용
        {
            moveVec = Vector3.zero;
        }
        */

        if (wDown) // 걷기 속도 조절 (뛰는거랑 걷는거랑 속도 차이 없으면 안되니까)
            transform.position += moveVec * speed * 0.3f * Time.deltaTime; // 걷기 중인 경우 속도 감소
        else
        {
            transform.position += moveVec * speed * Time.deltaTime; // 걷기가 아닌 경우 정상 속도로 이동
        }

        anim.SetBool("isRun", moveVec != Vector3.zero); // 달리는 상태 애니메이션 설정
        anim.SetBool("isWalk", wDown); // 걷기 상태 애니메이션 설정

        // LookAt : 지정된 벡터를 향해서 회전시켜주는 함수
        transform.LookAt(transform.position + moveVec); // 우리가 나아가는 방향으로 바라보게 만들기 (현재 위치+가는 방향)
    }










    //무기 변경관련 (교체할때)
    void Swap()
    {
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) // 무기 중복 교체, 없는 무기 확인을 위한 조건 추가
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;

        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;

        if (sDown1 || sDown2) // 단축키 둘 중 하나만 눌러도 되도록 or 조건 작성
        {
            if (equipWeapon != null)
                equipWeapon.SetActive(false); // 여기부터 '캐릭터가 빈 손일 경우'를 고려하지 않으면 null 오류가 생길 수 있음
                                              // 현재 장착한 무기 비활성화

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex]; // 새로운 무기 할당
            equipWeapon.SetActive(true); // 새로운 무기 활성화

            anim.SetTrigger("doSwap"); // 스왑 애니메이션 재생

            isSwap = true;

            Invoke("SwapOut", 0.4f); // 스왑 중 상태 변경 (일정 시간 후)
        }

    }

    //default 기본 상태로 돌아오는것같아.
    void SwapOut()
    {
        isSwap = false; // 스왑 중 상태 해제
    }



    //e키 눌렀을때 근처에 아이템이 있으면 줍는거. 근데 그 아이템이 이미 보유아이템이면 기존 아이템 파괴
    void Interaction()
    {
        if (iDown && nearObject != null)
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true; // 무기를 주웠으므로 해당 무기 보유 여부를 true로 설정

                Destroy(nearObject); // 주웠던 무기 오브젝트 파괴
            }
        }
    }




    void OnTriggerStay(Collider other)  // 3D 플레이어와 접촉중
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;

        Debug.Log(nearObject.name); // 주변에 있는 상호작용 가능한 오브젝트 이름 로그 출력
    }

    void OnTriggerExit(Collider other)  // 3D 플레이어와 접촉끝
    {
        if (other.tag == "Weapon")
            nearObject = null; // 주변에 있는 상호작용 가능한 오브젝트 없음으로 설정

    }
}
