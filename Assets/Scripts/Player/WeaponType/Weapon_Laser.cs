using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Laser : WeaponType
{
    [Header("레이저 오브젝트"), SerializeField]
    GameObject _laserObject;
    Vector3 _endPoint;
    float _fireDelay = 0.2f;
    bool _isDelay = false;
    int _ignoreLayer;
    [Header("피격지점 이펙트"), SerializeField]
    GameObject _laserEffect;


    private void Awake()
    {
        _weaponTypeEnum = WeaponTypeEnum.Laser;
        _damage = 1;
        _maxBarrel = 1;
        _currentBarrel = 1;
        _ignoreLayer = ~(1 << LayerMask.NameToLayer("Bullet") | (1 << LayerMask.NameToLayer("ReactManager")));
        _isActivated = false;
    }
    public override void SetWeapon()
    {
        //TODO:장착 시 할 일 구현(UI변경, 애니메이션 변경, 수치 변경 등)
        _laserEffect.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _laserObject.SetActive(false);
        }
    }
    public override void ShotWeapon()
    {
        if (Input.GetMouseButton(0)&&_isActivated && Time.timeScale != 0)
        {
            Ray ray;
            Vector3 targetDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(_aimStartPosition.transform.position);
            targetDirection = new Vector3(targetDirection.x, targetDirection.y, 0);
            ray = new Ray(_firePosition.transform.position, targetDirection);
            RaycastHit rayHit;
            Physics.Raycast(ray, out rayHit, 100f, _ignoreLayer);
            if(!_laserObject.activeSelf)
                _laserObject.SetActive(true);
            if (rayHit.collider == null)
            {
                _laserObject.transform.localScale = new Vector3(1, 100, 1);
                _laserEffect.SetActive(true);
            }
            else if (rayHit.collider != null)
            {
                if(rayHit.collider.CompareTag("Computer"))
                {
                    rayHit.collider.gameObject.GetComponent<ComputerCtrl>().OnComputer();
                }
                _endPoint = rayHit.point;
                _laserObject.transform.localScale = new Vector3(1,(_endPoint - _firePosition.transform.position).magnitude,1);
            }
            if (_laserEffect.activeSelf)
            {
                _laserEffect.transform.position = rayHit.point;
                Debug.Log(rayHit.collider.gameObject);
            }
        }
        
        if(Input.GetMouseButtonUp(0)) 
        {
            _laserObject.SetActive(false);
        }
    }
    public override void ResetWeapon()
    {
        _laserObject.SetActive(false);
    }
}
