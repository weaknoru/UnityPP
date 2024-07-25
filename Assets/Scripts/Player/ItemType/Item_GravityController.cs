using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_GravityController : ItemType
{
    [Header("플레이어 ConstantForce"), SerializeField]
    GravityInRigidbody _playerForce;
    [Header("지속 시간"), SerializeField]
    float _effectTime;
    [Header("효과 라이트"), SerializeField]
    Light _light;
    [Header("추진기 이펙트"), SerializeField]
    GameObject _boostPrefab;
    [Header("추진 이펙트 생성 위치"), SerializeField]
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
            Debug.Log("중력 감소");
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
