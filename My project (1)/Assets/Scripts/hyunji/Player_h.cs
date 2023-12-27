using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;


[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]


public class Player_h : LivingEntity
{
    public float speed; // �÷��̾� �̵� �ӵ�
    public GameObject[] weapons; // ������� ���� �迭 �Լ� ����
    public bool[] hasWeapons; // �÷��̾ ������ �ִ� ���� ���θ� ��Ÿ���� �迭
    float hAxis; // ���� �Է°�
    float vAxis; // ���� �Է°�

    bool wDown; // �ȱ� �Է� ����
    bool iDown; // Ű���� E Ű�� ������Ʈ�� ��ȣ�ۿ� �ϴ� ��찡 ���� �׷��� ������ְ��� ������
    bool sDown1; // ���� ����1 �Է� ����. ���� ���ӿ��� ��� �� ��, 1�� 2�� �� Ű�� ������ ���� ��ü�ϴµ� �� ��� �����ϱ� ����
    bool sDown2; // ���� ����2 �Է� ����

    bool isSwap; // ���� ���� �� ����. ��ü �ð����� ���� �÷��� ������ �ۼ��ϱ� ���� ����

    Vector3 moveVec; // �̵� ����

    GameObject nearObject; // �ֺ��� �ִ� ��ȣ�ۿ� ������ ������Ʈ.  Ʈ���� �� �������� �����ϱ� ���� ���� ����
    GameObject equipWeapon; // ���� ������ ���� ������Ʈ. ������ ������ ���⸦ �����ϴ� ������ �����ϰ� Ȱ���ϱ� ���� ���
    int equipWeaponIndex = -1; // ���� ������ ������ �ε���.  0���� ������ ���, �ظӸ� 0���� �����س��� ������ �ظӸ� �Ծ �ظӸ� �����س� �� ����

    Animator anim; // �÷��̾��� �ִϸ����� ������Ʈ

    Camera viewCamera; // ���� ī�޶�
    PlayerController controller; // �÷��̾� ��Ʈ�ѷ� ������Ʈ
    GunController gunController; // �ѱ� ��Ʈ�ѷ� ������Ʈ

    //���� �÷��̾ ���� ������Ʈ�� �� �����Ͷ�
    //protected override void Start()
    //{
    //    base.Start();
    //    controller = GetComponent<PlayerController>(); // �÷��̾� ��Ʈ�ѷ� ������Ʈ ��������
    //    gunController = GetComponent<GunController>(); // �ѱ� ��Ʈ�ѷ� ������Ʈ ��������
    //    viewCamera = Camera.main; // ���� ī�޶� ��������

    //    void Start()
    //    {
    //        controller = GetComponent<PlayerController>(); // �÷��̾� ��Ʈ�ѷ� ������Ʈ ��������
    //        gunController = GetComponent<GunController>(); // �ѱ� ��Ʈ�ѷ� ������Ʈ ��������
    //        viewCamera = Camera.main; // ���� ī�޶� ��������
    //        anim = GetComponentInChildren<Animator>();
    //    }


    //    void Update()
    //    {
    //        // ������Ʈ �׸��� �ʹ� �þ����ϱ� �׳� �Լ� ���� 
    //        GetInput();
    //        Move();
    //        Interaction();
    //        Swap();
    //    }


    //    //Ű���� ����Ű ������ �ۿ��ϴ� �͵鿡 ���Ͽ� (�̵�, e������ �ݱ�, 1/2���� ���� ����Ű)
    //    void GetInput() // ����Ƽ ��� �ǿ� Edit - Project Settings - Input Manager(���� ��� ��ư�� �����ϴ� ��) - Axes(���� ��) ������ - Size�� ���� ����
    //    {
    //        hAxis = Input.GetAxisRaw("Horizontal"); // ���� �Է°� �޾ƿ���
    //        vAxis = Input.GetAxisRaw("Vertical"); // ���� �Է°� �޾ƿ���
    //        wDown = Input.GetButton("Walk"); // �ȱ� �Է� ���� �޾ƿ��� (shift ������ �ӵ� ��ȭ)
    //        iDown = Input.GetButtonDown("Interaction"); // ��ȣ�ۿ� �Է� ���� �޾ƿ���
    //        sDown1 = Input.GetButtonDown("Swap1"); // ���� ����1 �Է� ���� �޾ƿ���
    //        sDown2 = Input.GetButtonDown("Swap2"); // ���� ����2 �Է� ���� �޾ƿ���
    //        Debug.Log("t");
    //    }





    //    //�̵��� ���Ͽ� (�밢���̵��� �ӵ�����, �ȱ�)
    //    void Move()
    //    {
    //        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // �Է°����� �̵� ���� ����
    //                                                           // normalized�� �밢������ �̵� �ÿ��� ���� �ӵ��� ���� ���� �ۼ�

    //        /*if(isSwap) // �����̸鼭 ���� ������ �� �ϰ� ������ �� ���
    //        {
    //            moveVec = Vector3.zero;
    //        }
    //        */

    //        if (wDown) // �ȱ� �ӵ� ���� (�ٴ°Ŷ� �ȴ°Ŷ� �ӵ� ���� ������ �ȵǴϱ�)
    //            transform.position += moveVec * speed * 0.3f * Time.deltaTime; // �ȱ� ���� ��� �ӵ� ����
    //        else
    //        {
    //            transform.position += moveVec * speed * Time.deltaTime; // �ȱⰡ �ƴ� ��� ���� �ӵ��� �̵�
    //        }

    //        anim.SetBool("isRun", moveVec != Vector3.zero); // �޸��� ���� �ִϸ��̼� ����
    //        anim.SetBool("isWalk", wDown); // �ȱ� ���� �ִϸ��̼� ����

    //        // LookAt : ������ ���͸� ���ؼ� ȸ�������ִ� �Լ�
    //        transform.LookAt(transform.position + moveVec); // �츮�� ���ư��� �������� �ٶ󺸰� ����� (���� ��ġ+���� ����)
    //    }





    //    // ���� ���� �Է� (���콺 ������ �Ѿ˳���)
    //    if (Input.GetMouseButton(0))
    //    {
    //        gunController.Shoot();
    //    }




    //    //���� ������� (��ü�Ҷ�)
    //    void Swap()
    //    {
    //        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) // ���� �ߺ� ��ü, ���� ���� Ȯ���� ���� ���� �߰�
    //            return;
    //        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
    //            return;

    //        int weaponIndex = -1;
    //        if (sDown1) weaponIndex = 0;
    //        if (sDown2) weaponIndex = 1;

    //        if (sDown1 || sDown2) // ����Ű �� �� �ϳ��� ������ �ǵ��� or ���� �ۼ�
    //        {
    //            if (equipWeapon != null)
    //                equipWeapon.SetActive(false); // ������� 'ĳ���Ͱ� �� ���� ���'�� ������� ������ null ������ ���� �� ����
    //                                              // ���� ������ ���� ��Ȱ��ȭ

    //            equipWeaponIndex = weaponIndex;
    //            equipWeapon = weapons[weaponIndex]; // ���ο� ���� �Ҵ�
    //            equipWeapon.SetActive(true); // ���ο� ���� Ȱ��ȭ

    //            anim.SetTrigger("doSwap"); // ���� �ִϸ��̼� ���

    //            isSwap = true;

    //            Invoke("SwapOut", 0.4f); // ���� �� ���� ���� (���� �ð� ��)
    //        }

    //    }

    //    //default �⺻ ���·� ���ƿ��°Ͱ���.
    //    void SwapOut()
    //    {
    //        isSwap = false; // ���� �� ���� ����
    //    }



    //    //eŰ �������� ��ó�� �������� ������ �ݴ°�. �ٵ� �� �������� �̹� �����������̸� ���� ������ �ı�
    //    void Interaction()
    //    {
    //        if (iDown && nearObject != null)
    //        {
    //            if (nearObject.tag == "Weapon")
    //            {
    //                Item item = nearObject.GetComponent<Item>();
    //                int weaponIndex = item.value;
    //                hasWeapons[weaponIndex] = true; // ���⸦ �ֿ����Ƿ� �ش� ���� ���� ���θ� true�� ����

    //                Destroy(nearObject); // �ֿ��� ���� ������Ʈ �ı�
    //            }
    //        }
    //    }




    //    void OnTriggerStay(Collider other)  // 3D �÷��̾�� ������
    //    {
    //        if (other.tag == "Weapon")
    //            nearObject = other.gameObject;

    //        Debug.Log(nearObject.name); // �ֺ��� �ִ� ��ȣ�ۿ� ������ ������Ʈ �̸� �α� ���
    //    }

    //    void OnTriggerExit(Collider other)  // 3D �÷��̾�� ���˳�
    //    {
    //        if (other.tag == "Weapon")
    //            nearObject = null; // �ֺ��� �ִ� ��ȣ�ۿ� ������ ������Ʈ �������� ����

    //    }

    //}
    void Start()
    {
        controller = GetComponent<PlayerController>(); // �÷��̾� ��Ʈ�ѷ� ������Ʈ ��������
        gunController = GetComponent<GunController>(); // �ѱ� ��Ʈ�ѷ� ������Ʈ ��������
        viewCamera = Camera.main; // ���� ī�޶� ��������
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        // ������Ʈ �׸��� �ʹ� �þ����ϱ� �׳� �Լ� ���� 
        GetInput();
        Move();
        Interaction();
        Swap();

        // ���� ���� �Է� (���콺 ������ �Ѿ˳���)
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }


    //Ű���� ����Ű ������ �ۿ��ϴ� �͵鿡 ���Ͽ� (�̵�, e������ �ݱ�, 1/2���� ���� ����Ű)
    void GetInput() // ����Ƽ ��� �ǿ� Edit - Project Settings - Input Manager(���� ��� ��ư�� �����ϴ� ��) - Axes(���� ��) ������ - Size�� ���� ����
    {
        hAxis = Input.GetAxisRaw("Horizontal"); // ���� �Է°� �޾ƿ���
        vAxis = Input.GetAxisRaw("Vertical"); // ���� �Է°� �޾ƿ���
        wDown = Input.GetButton("Walk"); // �ȱ� �Է� ���� �޾ƿ��� (shift ������ �ӵ� ��ȭ)
        iDown = Input.GetButtonDown("Interaction"); // ��ȣ�ۿ� �Է� ���� �޾ƿ���
        sDown1 = Input.GetButtonDown("Swap1"); // ���� ����1 �Է� ���� �޾ƿ���
        sDown2 = Input.GetButtonDown("Swap2"); // ���� ����2 �Է� ���� �޾ƿ���
    }





    //�̵��� ���Ͽ� (�밢���̵��� �ӵ�����, �ȱ�)
    void Move()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized; // �Է°����� �̵� ���� ����
                                                           // normalized�� �밢������ �̵� �ÿ��� ���� �ӵ��� ���� ���� �ۼ�

        /*if(isSwap) // �����̸鼭 ���� ������ �� �ϰ� ������ �� ���
        {
            moveVec = Vector3.zero;
        }
        */

        if (wDown) // �ȱ� �ӵ� ���� (�ٴ°Ŷ� �ȴ°Ŷ� �ӵ� ���� ������ �ȵǴϱ�)
            transform.position += moveVec * speed * 0.3f * Time.deltaTime; // �ȱ� ���� ��� �ӵ� ����
        else
        {
            transform.position += moveVec * speed * Time.deltaTime; // �ȱⰡ �ƴ� ��� ���� �ӵ��� �̵�
        }

        anim.SetBool("isRun", moveVec != Vector3.zero); // �޸��� ���� �ִϸ��̼� ����
        anim.SetBool("isWalk", wDown); // �ȱ� ���� �ִϸ��̼� ����

        // LookAt : ������ ���͸� ���ؼ� ȸ�������ִ� �Լ�
        transform.LookAt(transform.position + moveVec); // �츮�� ���ư��� �������� �ٶ󺸰� ����� (���� ��ġ+���� ����)
    }










    //���� ������� (��ü�Ҷ�)
    void Swap()
    {
        if (sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) // ���� �ߺ� ��ü, ���� ���� Ȯ���� ���� ���� �߰�
            return;
        if (sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1))
            return;

        int weaponIndex = -1;
        if (sDown1) weaponIndex = 0;
        if (sDown2) weaponIndex = 1;

        if (sDown1 || sDown2) // ����Ű �� �� �ϳ��� ������ �ǵ��� or ���� �ۼ�
        {
            if (equipWeapon != null)
                equipWeapon.SetActive(false); // ������� 'ĳ���Ͱ� �� ���� ���'�� ������� ������ null ������ ���� �� ����
                                              // ���� ������ ���� ��Ȱ��ȭ

            equipWeaponIndex = weaponIndex;
            equipWeapon = weapons[weaponIndex]; // ���ο� ���� �Ҵ�
            equipWeapon.SetActive(true); // ���ο� ���� Ȱ��ȭ

            anim.SetTrigger("doSwap"); // ���� �ִϸ��̼� ���

            isSwap = true;

            Invoke("SwapOut", 0.4f); // ���� �� ���� ���� (���� �ð� ��)
        }

    }

    //default �⺻ ���·� ���ƿ��°Ͱ���.
    void SwapOut()
    {
        isSwap = false; // ���� �� ���� ����
    }



    //eŰ �������� ��ó�� �������� ������ �ݴ°�. �ٵ� �� �������� �̹� �����������̸� ���� ������ �ı�
    void Interaction()
    {
        if (iDown && nearObject != null)
        {
            if (nearObject.tag == "Weapon")
            {
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true; // ���⸦ �ֿ����Ƿ� �ش� ���� ���� ���θ� true�� ����

                Destroy(nearObject); // �ֿ��� ���� ������Ʈ �ı�
            }
        }
    }




    void OnTriggerStay(Collider other)  // 3D �÷��̾�� ������
    {
        if (other.tag == "Weapon")
            nearObject = other.gameObject;

        Debug.Log(nearObject.name); // �ֺ��� �ִ� ��ȣ�ۿ� ������ ������Ʈ �̸� �α� ���
    }

    void OnTriggerExit(Collider other)  // 3D �÷��̾�� ���˳�
    {
        if (other.tag == "Weapon")
            nearObject = null; // �ֺ��� �ִ� ��ȣ�ۿ� ������ ������Ʈ �������� ����

    }
}
