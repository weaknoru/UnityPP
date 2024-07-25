using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Missile : WeaponType
{
    private PoolingSystem _poolingSystem;
    [Header("�̻��� ������"), SerializeField]
    GameObject _bulletPrefab;

    
    float _fireDelay = 1f;
    bool _isDelay = false;

    private void Awake()
    {
        _weaponTypeEnum = WeaponTypeEnum.Missile;
        _damage = 1;
        _maxBarrel = 1;
        _currentBarrel = 1;
        _isActivated = false;
    }
    private void Start()
    {
        _poolingSystem = PoolingSystem._instance;
    }
    public override void SetWeapon()
    {
        //TODO:���� �� �� �� ����(UI����, �ִϸ��̼� ����, ��ġ ���� ��)
    }
    

    public override void ShotWeapon()
    {
        if (!_isDelay && Input.GetMouseButton(0) && _isActivated && Time.timeScale != 0)
        {
            if (!_isDelay && Input.GetMouseButton(0) && _isActivated && Time.timeScale != 0)
            {
                GameObject bullet = _poolingSystem.InstantiateAPS("Missile Prefab", _firePosition.transform.position, Quaternion.LookRotation(GetAimDirection()), new Vector3(0.05f, 0.05f, 0.03f));
                /*GameObject bullet = Instantiate(_bulletPrefab);
                bullet.transform.position = _firePosition.transform.position;
                bullet.transform.rotation = Quaternion.LookRotation(GetAimDirection());*/
                StartCoroutine(WeaponDelay(_fireDelay));
            }
            
        }
    }
    //�߻� ��Ÿ��
    IEnumerator WeaponDelay(float time)
    {
        _isDelay = true;
        yield return new WaitForSeconds(time);
        _isDelay = false;
    }

    public override void ResetWeapon()
    {

    }
}
