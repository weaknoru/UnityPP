using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Mine : ItemType
{
    [Header("�÷��̾� �ִϸ�����"), SerializeField]
    Animator _playerAnimator;
    //IK������� ���� �� ��ġ�� �������� ����
    [Header("�� ������Ʈ"), SerializeField]
    GameObject _playerGun;
    
    private void Awake()
    {
        _itemTypeEnum = ItemTypeEnum.Mine;
        _isActivated = false;
        _cooldown = 5;
    }

    public override void UseItem()
    {
        if (Input.GetMouseButtonDown(1) && !_isCooldown && _isActivated)
        {
            Debug.Log("���� ��ġ");
            StartCoroutine(SetAnimation());
        }
    }

    IEnumerator SetAnimation()
    {
        _isCooldown = true;
        float startTime = Time.time;
        _playerAnimator.SetTrigger("Set");
        while (true)
        {
            if (Time.time - startTime < 0.5f)
                _playerGun.transform.position -= new Vector3(0, 2f * Time.deltaTime, 0);
            else if (Time.time - startTime < 1f)
                _playerGun.transform.position += new Vector3(0, 2f * Time.deltaTime, 0);
            else if (Time.time - startTime >= _cooldown)
                break;
            yield return null;
        } 
        _isCooldown = false;
    }
}
