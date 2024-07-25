using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponType : MonoBehaviour
{
    protected int _damage;
    protected int _currentBarrel;
    protected int _maxBarrel;
    [Header("발사 위치"), SerializeField]
    protected GameObject _firePosition;
    [Header("조준 기준 위치"), SerializeField]
    protected GameObject _aimStartPosition;
    public bool _isActivated;
    public WeaponTypeEnum _weaponTypeEnum;

    //무기 교체 시 처리할 일
    public abstract void SetWeapon();
    //발사 기능
    public abstract void ShotWeapon();
    //무기 교체 시 초기화할 일
    public abstract void ResetWeapon();

    //조준 위치 설정
    protected Vector3 GetAimDirection()
    {
        Vector3 tempVector = Input.mousePosition - Camera.main.WorldToScreenPoint(_aimStartPosition.transform.position);
        tempVector = new Vector3(tempVector.x, tempVector.y, 0);
        return tempVector;
    }
}
