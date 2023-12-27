using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    //하이어라키에 총알 제작
    //총알은 색상, 중력(리지드바디), 콜라이더를 가지고 있음
    //위에까지 하이어라키에서 만들고
    //아래 스크립트 제작


    //총알이 발사, 탄피가 바닥에 떨어지는 스크립트


    //총알과 탄피의 로직
    private void OnCollisionEnter(Collision collision)  //충돌할때 쓰는 함수
    {
        if(collision.gameObject.tag == "Floor")  //만약에 게임오브젝트가 충돌할 상대가 태그 Floor다 (탄피인 경우)
            Destroy(gameObject, 3);  //게임오브젝트. 즉 자기자신을 3초뒤에 사라지게하겠다

        else if (collision.gameObject.tag == "Wall")  //만약에 게임오브젝트가 충돌할 상대가 태그 Wall이다 (총알인 경우)
            Destroy(gameObject);  //총알은 딜레이없이 바로 사라지겠다
    }
    
}
