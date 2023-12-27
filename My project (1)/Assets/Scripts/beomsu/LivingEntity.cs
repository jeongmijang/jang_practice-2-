using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// IDamageable �������̽��� �����ϴ� LivingEntity Ŭ�����Դϴ�.
public class LivingEntity : MonoBehaviour, IDamageable
{
    // ü���� ������ �� �����մϴ�.
    public float startingHealth;

    // ���� ü���� �����մϴ�.
    protected float health;

    // �׾����� ���θ� ��Ÿ���ϴ�.
    protected bool dead;

    // ������ �� �ʱ� ü���� �����ϴ� �޼ҵ��Դϴ�.
    protected virtual void Start()
    {
        // ���� ü���� ���� ü������ �����մϴ�.
        health = startingHealth;
    }

    // �������� �޴� �޼ҵ��Դϴ�.
    public void TakeHit(float damage, RaycastHit hit)
    {
        // ���� ��������ŭ ���� ü���� ���ҽ�ŵ�ϴ�.
        health -= damage;

        // ü���� 0 �����̰� ���� ���� �ʾҴٸ� Die �޼ҵ带 ȣ���մϴ�.
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // �״� �޼ҵ��Դϴ�.
    protected void Die()
    {
        // �׾����� ǥ���ϰ� ���� ������Ʈ�� �ı��մϴ�.
        dead = true;
        GameObject.Destroy(gameObject);
    }
}
