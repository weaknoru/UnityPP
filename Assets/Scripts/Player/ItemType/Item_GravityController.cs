using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_GravityController : ItemType
{
    [Header("�÷��̾� ConstantForce"), SerializeField]
    GravityInRigidbody _playerForce;
    [Header("���� �ð�"), SerializeField]
    float _effectTime;
    [Header("ȿ�� ����Ʈ"), SerializeField]
    Light _light;
    [Header("������ ����Ʈ"), SerializeField]
    GameObject _boostPrefab;
    [Header("���� ����Ʈ ���� ��ġ"), SerializeField]
    Transform _boostTransform;
    private void Awake()
    {
        _itemTypeEnum = ItemTypeEnum.GravityController;
        _cooldown = 20;
    }

    public override void UseItem()
    {
        if(Input.GetMouseButtonDown(1) && !_isCooldown && _isActivated)
        {
            Debug.Log("�߷� ����");
            StartCoroutine(ReduceGravity());
        } 
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_boostPrefab,_boostTransform);
        }
    }

    IEnumerator ReduceGravity()
    {
        _isCooldown = true;
        _light.intensity = 10;
        float StartTime = Time.time;
        _playerForce.SetConstantForce(new Vector3(0, 0, 0));
        yield return new WaitForSeconds(_effectTime-3f);
        for(int i=0;i<3;i++)
        {
            _light.intensity = 0;
            yield return new WaitForSeconds(0.5f);
            _light.intensity = 10;
            yield return new WaitForSeconds(0.5f);
        }
        _light.intensity = 0;
        _playerForce.SetConstantForce(new Vector3(0, -20, 0));
        yield return new WaitForSeconds(_cooldown - _effectTime);
        _isCooldown = false;
    }
}
