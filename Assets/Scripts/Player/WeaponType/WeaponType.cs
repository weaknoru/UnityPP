using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponType : MonoBehaviour
{
    protected int _damage;
    protected int _currentBarrel;
    protected int _maxBarrel;
    [Header("�߻� ��ġ"), SerializeField]
    protected GameObject _firePosition;
    [Header("���� ���� ��ġ"), SerializeField]
    protected GameObject _aimStartPosition;
    public bool _isActivated;
    public WeaponTypeEnum _weaponTypeEnum;

    //���� ��ü �� ó���� ��
    public abstract void SetWeapon();
    //�߻� ���
    public abstract void ShotWeapon();
    //���� ��ü �� �ʱ�ȭ�� ��
    public abstract void ResetWeapon();

    //���� ��ġ ����
    protected Vector3 GetAimDirection()
    {
        Vector3 tempVector = Input.mousePosition - Camera.main.WorldToScreenPoint(_aimStartPosition.transform.position);
        tempVector = new Vector3(tempVector.x, tempVector.y, 0);
        return tempVector;
    }
}
