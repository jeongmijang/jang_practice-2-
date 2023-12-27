using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

    public LayerMask collisionMask;
    float speed = 10; // 발사 속도
    float damage = 1; // 데미지

    // 속도 설정하는 메서드
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    // 충돌을 확인하는 메서드
    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // 충돌이 감지되면 OnHitObject 메서드를 호출함
        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    // 충돌한 객체에 대한 처리를 하는 메서드
    void OnHitObject(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeHit(damage, hit);
        }
        GameObject.Destroy(gameObject);
    }
}
