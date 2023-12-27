using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Coin, Ammo, Grenade, Heart, Weapon }; // 변수가 아닌 하나의 타입이기 때문에 변수는 아래에 따로 생성
    public Type type; // 이 변수는 어떤 유형 또는 종류를 나타낸다. 예를 들어, 아이템의 유형을 나타내거나 다양한 종류의 정보를 담을 수 있다.
    public int value; // 이 변수는 주로 숫자 값으로 특정 유형이나 종류를 식별할 때 사용된다. 예를 들어, 아이템의 고유한 식별자나 특정 값을 나타낼 수 있다.

    /* 만약 무기가 계속 회전하는 형태로 화면상에 나타나기를 원한다면 Rotate() 함수를 사용해준다. 
    private void Update()
    {
        transform.Rotate(Vecter3.up * 10(속도라서 보고 수치 조절하면 됨) * Time.deltaTime
    }
    */
}
