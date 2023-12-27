using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// NavMeshAgent ������Ʈ�� �ʿ��ϹǷ�, �̸� �䱸�ϴ� ��Ʈ����Ʈ�� �߰�
[RequireComponent (typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{

    // ��� ã�⸦ ���� NavMeshAgent�� ����
    NavMeshAgent pathfinder;

    // ������ ����� ����
    Transform target;

    // protected override void Start()�� LivingEntity Ŭ������ Start() �޼��带
    // �ڽ� Ŭ������ Enemy���� �������Ͽ� ����Ѵٴ� �ǹ��Դϴ�.
    protected override void Start()
    {
        base.Start();

        // �� ������Ʈ�� �پ��ִ� NavMeshAgent ������Ʈ�� ������
        pathfinder = GetComponent<NavMeshAgent>();

        // "Player"��� �±׸� ���� ������Ʈ�� Transform�� target���� ����
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // ��� ������Ʈ�� ���� �ڷ�ƾ�� ����
        StartCoroutine(UpdatePath());
    }

    
    void Update()
    {  

    }

    // ��θ� ������Ʈ�ϴ� �ڷ�ƾ
    IEnumerator UpdatePath()
    {
        // ��� ������Ʈ �ֱ⸦ ����. ���⼭�� 0.25�ʸ��� ������Ʈ�ϵ��� ����
        float refreshRate = .25f;

        // target�� null�� �ƴ� ������ ������ ������
        while (target != null)
        {
            // target�� ��ġ�� ������. Y���� 0���� �����Ͽ� ���̸� ����
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);

            if (!dead)
            {
                // pathfinder�� �������� target�� ��ġ�� ����
                pathfinder.SetDestination(targetPosition);
            }

            // refreshRate��ŭ ����� �� ���� ������ ����
            yield return new WaitForSeconds(refreshRate);
        }
    }
    // �� �ڵ�� �� AI�� �÷��̾ �ڵ����� �����ϵ��� �ϴ� ������ ��.
    // Unity�� NavMeshAgent ������Ʈ�� ����Ͽ� ��θ� ����ϰ�, �̸� ���� �÷��̾ ����ٴϵ��� ��
}
