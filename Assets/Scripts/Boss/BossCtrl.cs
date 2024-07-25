using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    float _maxHealth, _currentHealth;
    Animator _animator;
    float _headArmor, _chestArmor, _bodyArmor;
    float _normalAttackDamage, _dashDamage, _missileDamage;
    float _moveSpeed, _dashSpeed;
    bool _isExhausted, _isDash, _missileCooldown,_isShootingMissile,_attackCooldown;
    [Header("투사체 프리팹"), SerializeField]
    GameObject _missilePrefab;
    [Header("투사체 발사 위치"), SerializeField]
    Transform _missilePosition;
    [Header("기본 위치"), SerializeField]
    Transform _defaultPosition;
    //temp
    [SerializeField]
    Transform _targetTransform;
    int _bodyLayermask;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _maxHealth = 1500;
        _currentHealth = _maxHealth;
        _isExhausted = false;
        _headArmor = 5;
        _chestArmor = 2;
        _bodyArmor = 3;
        _targetTransform = _defaultPosition;
        _isDash = false;
        _moveSpeed = 5;
        _dashSpeed = 10;
        _bodyLayermask = ~(1 << LayerMask.NameToLayer("Boss Body"));
        _isShootingMissile = false;
        _attackCooldown = false;
        StartCoroutine(MissileCooldown());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(NormalAttack());
        }
        if (Input.GetMouseButtonDown(1))
        {
            _animator.SetTrigger("Attack_5");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DashAttack());
        }
        if (_currentHealth <= 0)
        {
            StopAllCoroutines();
            _animator.SetTrigger("Die");
            enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _currentHealth -= 200;
        }
        if(_targetTransform != _defaultPosition)
        {
            if (!_isDash)
            {
                if (!_missileCooldown && !_isShootingMissile)
                {
                    StartCoroutine(ShootingMissile());
                }
                else if((_targetTransform.position-transform.position).sqrMagnitude>0.1f&&!_missileCooldown)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_targetTransform.position - transform.position), 5f * Time.deltaTime);
                    transform.position += (_targetTransform.position - transform.position).normalized * _moveSpeed * Time.deltaTime;
                }
                else if(!_attackCooldown&&!_missileCooldown)
                {
                    StartCoroutine(NormalAttack());
                }
            }
            else
            {
                StartCoroutine(DashAttack());
            }
        }
    }
    
    public void TakeDamage(int damage, int layerNumber, WeaponTypeEnum type)
    {
        float totalDamage = 0;
        if (layerNumber == 6)
        {
            totalDamage = damage - _headArmor;
            if (_isExhausted)
                totalDamage *= 1.3f;
            if (type == WeaponTypeEnum.Missile)
                totalDamage *= 1.2f;
        }
        else if (layerNumber == 7)
        {
            totalDamage = damage - _chestArmor;
            if (_isExhausted)
                totalDamage *= 1.1f;
            if (type == WeaponTypeEnum.Normal)
                totalDamage *= 1.1f;
        }
        else if (layerNumber == 8)
        {
            totalDamage = damage - _bodyArmor;
            if (_isExhausted)
                totalDamage *= 1.1f;
            if (type == WeaponTypeEnum.Missile)
                totalDamage *= 1.1f;
        }
        else
        {
            Debug.LogError("레이어 이상");
            return;
        }
        _currentHealth -= totalDamage;
    }

    IEnumerator NormalAttack()
    {
        _attackCooldown = true;
        _animator.SetTrigger("Attack_2");
        yield return new WaitForSeconds(0.8f);
        _animator.SetTrigger("Attack_3");
        yield return new WaitForSeconds(0.7f);
        _attackCooldown = false;
    }

    public void MissileAttack()
    {
        GameObject tempMissile = Instantiate(_missilePrefab);
        tempMissile.transform.position = _missilePosition.transform.position;
        tempMissile.transform.rotation = _missilePosition.transform.rotation;
        //TODO:미사일 데미지 설정 여기서할지 미사일에서 할지
    }

    IEnumerator DashAttack()
    {
        _targetTransform = _defaultPosition;
        _isDash = true;
        _animator.SetTrigger("Walk_Cycle_1");
        while ((transform.position - _targetTransform.position).sqrMagnitude > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_targetTransform.position - transform.position), 5f * Time.deltaTime);
            transform.position += (_targetTransform.position - transform.position).normalized * _moveSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(ShowTarget());
        _animator.SetTrigger("Intimidate_2");
        yield return new WaitForSeconds(2f);
        _animator.SetTrigger("Walk_Cycle_1");
        RaycastHit hit = new RaycastHit();
        while (true)
        {
            Physics.Raycast(transform.position, transform.forward, out hit, 100f, _bodyLayermask);
            _animator.SetFloat("MoveSpeed", 3);
            transform.position += transform.forward * _dashSpeed * Time.deltaTime;
            if (hit.collider != null)
            {

                if (hit.distance < 2.5f)
                {
                    _animator.SetTrigger("Attack_1");
                    yield return new WaitForSeconds(1f);
                    break;
                }
            }
            yield return null;
        }
        _isExhausted = true;
        _animator.SetTrigger("Sleep");
        _animator.SetFloat("MoveSpeed", 1);
        yield return new WaitForSeconds(3f);
        _isExhausted = false;
        _animator.SetTrigger("Rest_1");
    }

    IEnumerator MissileCooldown()
    {
        _missileCooldown = true;
        yield return new WaitForSeconds(10f);
        _missileCooldown = false;
    }
   
    IEnumerator ShootingMissile()
    {
        _isShootingMissile = true;
        for(int i=0;i<3;i++)
        {
            StartCoroutine(ShowTarget());
            _animator.SetTrigger("Attack_5");
            if(i !=2)
                yield return new WaitForSeconds(1.5f);
        }
        _isShootingMissile = false;
        StartCoroutine(MissileCooldown());
        _animator.SetTrigger("Rest_1");
    }

    IEnumerator ShowTarget()
    {
        while (transform.rotation != Quaternion.LookRotation(_targetTransform.position - transform.position))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_targetTransform.position - transform.position), 5f * Time.deltaTime);
            yield return null;
        }
    }
}
