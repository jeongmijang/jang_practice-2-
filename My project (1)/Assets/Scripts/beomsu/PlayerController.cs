using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    Vector3 velocity; // 플레이어의 이동 속도
    Rigidbody myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // 이동 속도를 설정하는 메서드
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // 주어진 위치를 바라보도록 하는 메서드
    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    void FixedUpdate()
    {

        // Rigidbody의 위치를 이동 속도에 따라 변경
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);

    }
}
