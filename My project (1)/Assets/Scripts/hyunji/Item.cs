using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { Coin, Ammo, Grenade, Heart, Weapon }; // ������ �ƴ� �ϳ��� Ÿ���̱� ������ ������ �Ʒ��� ���� ����
    public Type type; // �� ������ � ���� �Ǵ� ������ ��Ÿ����. ���� ���, �������� ������ ��Ÿ���ų� �پ��� ������ ������ ���� �� �ִ�.
    public int value; // �� ������ �ַ� ���� ������ Ư�� �����̳� ������ �ĺ��� �� ���ȴ�. ���� ���, �������� ������ �ĺ��ڳ� Ư�� ���� ��Ÿ�� �� �ִ�.

    /* ���� ���Ⱑ ��� ȸ���ϴ� ���·� ȭ��� ��Ÿ���⸦ ���Ѵٸ� Rotate() �Լ��� ������ش�. 
    private void Update()
    {
        transform.Rotate(Vecter3.up * 10(�ӵ��� ���� ��ġ �����ϸ� ��) * Time.deltaTime
    }
    */
}
