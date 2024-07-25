using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Mine : ItemType
{
    [Header("플레이어 애니메이터"), SerializeField]
    Animator _playerAnimator;
    //IK사용으로 인해 총 위치를 수동으로 조정
    [Header("총 오브젝트"), SerializeField]
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
            Debug.Log("지뢰 설치");
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
