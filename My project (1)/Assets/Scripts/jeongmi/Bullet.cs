using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;

    //���̾��Ű�� �Ѿ� ����
    //�Ѿ��� ����, �߷�(������ٵ�), �ݶ��̴��� ������ ����
    //�������� ���̾��Ű���� �����
    //�Ʒ� ��ũ��Ʈ ����


    //�Ѿ��� �߻�, ź�ǰ� �ٴڿ� �������� ��ũ��Ʈ


    //�Ѿ˰� ź���� ����
    private void OnCollisionEnter(Collision collision)  //�浹�Ҷ� ���� �Լ�
    {
        if(collision.gameObject.tag == "Floor")  //���࿡ ���ӿ�����Ʈ�� �浹�� ��밡 �±� Floor�� (ź���� ���)
            Destroy(gameObject, 3);  //���ӿ�����Ʈ. �� �ڱ��ڽ��� 3�ʵڿ� ��������ϰڴ�

        else if (collision.gameObject.tag == "Wall")  //���࿡ ���ӿ�����Ʈ�� �浹�� ��밡 �±� Wall�̴� (�Ѿ��� ���)
            Destroy(gameObject);  //�Ѿ��� �����̾��� �ٷ� ������ڴ�
    }
    
}
