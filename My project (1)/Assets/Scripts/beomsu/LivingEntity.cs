using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// IDamageable 인터페이스를 구현하는 LivingEntity 클래스입니다.
public class LivingEntity : MonoBehaviour, IDamageable
{
    // 체력을 시작할 때 설정합니다.
    public float startingHealth;

    // 현재 체력을 관리합니다.
    protected float health;

    // 죽었는지 여부를 나타냅니다.
    protected bool dead;

    // 시작할 때 초기 체력을 설정하는 메소드입니다.
    protected virtual void Start()
    {
        // 시작 체력을 현재 체력으로 설정합니다.
        health = startingHealth;
    }

    // 데미지를 받는 메소드입니다.
    public void TakeHit(float damage, RaycastHit hit)
    {
        // 받은 데미지만큼 현재 체력을 감소시킵니다.
        health -= damage;

        // 체력이 0 이하이고 아직 죽지 않았다면 Die 메소드를 호출합니다.
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // 죽는 메소드입니다.
    protected void Die()
    {
        // 죽었음을 표시하고 게임 오브젝트를 파괴합니다.
        dead = true;
        GameObject.Destroy(gameObject);
    }
}
