using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{

    public float moveSpeed = 5; // 플레이어의 이동 속도

    Camera viewCamera; // 메인 카메라
    PlayerController controller; // 플레이어 컨트롤러 컴포넌트
    GunController gunController; // 총기 컨트롤러 컴포넌트


    //현재 플레이어가 쓰는 컴포넌트를 다 가져와라
    protected override void Start()
    {
        base.Start();
        controller = GetComponent<PlayerController>(); // 플레이어 컨트롤러 컴포넌트 가져오기
        gunController = GetComponent<GunController>(); // 총기 컨트롤러 컴포넌트 가져오기
        viewCamera = Camera.main; // 메인 카메라 가져오기
    }


    //플레이어가 이동하는 것에 대하여. (마우스가 있는곳이 방향/ a,s,d,w가 방향)
    void Update()
    {
        // 이동을 입력받는 곳
       // Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
       // Vector3 moveVelocity = moveInput.normalized * moveSpeed;
       // controller.Move(moveVelocity);
//
        // 바라보는 방향을 입력받는 곳
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            //Debug.DrawRay(ray.origin,ray.direction * 100,Color.red);
            controller.LookAt(point);
        }

        // 무기 조작 입력 (마우스 누르면 총알나옴)
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}
