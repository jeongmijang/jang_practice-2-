using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// NavMeshAgent 컴포넌트가 필요하므로, 이를 요구하는 어트리뷰트를 추가
[RequireComponent (typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{

    // 경로 찾기를 위한 NavMeshAgent를 선언
    NavMeshAgent pathfinder;

    // 추적할 대상을 선언
    Transform target;

    // protected override void Start()는 LivingEntity 클래스의 Start() 메서드를
    // 자식 클래스인 Enemy에서 재정의하여 사용한다는 의미입니다.
    protected override void Start()
    {
        base.Start();

        // 이 오브젝트에 붙어있는 NavMeshAgent 컴포넌트를 가져옴
        pathfinder = GetComponent<NavMeshAgent>();

        // "Player"라는 태그를 가진 오브젝트의 Transform을 target으로 설정
        target = GameObject.FindGameObjectWithTag("Player").transform;

        // 경로 업데이트를 위한 코루틴을 시작
        StartCoroutine(UpdatePath());
    }

    
    void Update()
    {  

    }

    // 경로를 업데이트하는 코루틴
    IEnumerator UpdatePath()
    {
        // 경로 업데이트 주기를 설정. 여기서는 0.25초마다 업데이트하도록 설정
        float refreshRate = .25f;

        // target이 null이 아닐 때까지 루프를 돌게함
        while (target != null)
        {
            // target의 위치를 가져옴. Y축은 0으로 설정하여 높이를 무시
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);

            if (!dead)
            {
                // pathfinder의 목적지를 target의 위치로 설정
                pathfinder.SetDestination(targetPosition);
            }

            // refreshRate만큼 대기한 후 다음 루프를 실행
            yield return new WaitForSeconds(refreshRate);
        }
    }
    // 이 코드는 적 AI가 플레이어를 자동으로 추적하도록 하는 역할을 함.
    // Unity의 NavMeshAgent 컴포넌트를 사용하여 경로를 계산하고, 이를 통해 플레이어를 따라다니도록 함
}
