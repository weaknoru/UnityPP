using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackType_Hornet : MonsterAttackType
{
    private PoolingSystem _poolingSystem;
    bool _isAttack;
    float _attackRange;
    float _attackDelay;
    bool _isRotating;
    [Header("투사체 프리팹"), SerializeField]
    GameObject _missilePrefab;
    [Header("투사체 발사 위치"), SerializeField]
    public Transform _missilePosition;
    protected override void Awake()
    {
        base.Awake();
        _isRotating = false;
        _attackRange = 120f;
        _attackDelay = 1f;
    }
    private void Start()
    {
        _poolingSystem = PoolingSystem._instance;
    }
    public override void OnEnter()
    {
        _isAttack = false;
        _isRotating = false;
        _animator.SetTrigger("Idle");
    }
    public override void Attack()
    {
        base.Attack();
        ReleasePlayer();
        if (!_isAttack && (_FSMManager._targetObject == null || (_FSMManager._targetObject.transform.position - transform.position).sqrMagnitude > _attackRange))
        {
            _FSMManager.ChangeState(Monster_Chase._Inst);
            return;
        }
        if (!_isRotating && GetTargetDirection() == true && transform.eulerAngles.y != 270)
        {
            StartCoroutine(RotateAvatar());
        }
        else if (!_isRotating && GetTargetDirection() == false && transform.eulerAngles.y != 90)
        {
            StartCoroutine(RotateAvatar());
        }
        else if (!_isRotating && ((GetTargetDirection() == true && transform.eulerAngles.y == 270) || (GetTargetDirection() == false && transform.eulerAngles.y == 90)))
        {
            if (!_isAttack)
            {
                StartCoroutine(DoAttack());
            }
        }
    }

    IEnumerator RotateAvatar()
    {
        _isRotating = true;
        while (true)
        {
            if (GetTargetDirection())
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 270, 0)), _rotateSpeed * Time.deltaTime);
                if (transform.eulerAngles == new Vector3(0, 270, 0))
                    break;
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), _rotateSpeed * Time.deltaTime);
                if (transform.eulerAngles == new Vector3(0, 90, 0))
                    break;
            }
            yield return null;
        }
        _isRotating = false;
    }
    IEnumerator DoAttack()
    {
        _isAttack = true;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(_attackDelay);
        _animator.SetTrigger("Idle");
        _isAttack = false;
    }

    public void AttackNiddle()
    {
        if (_missilePrefab != null&&_FSMManager._targetObject!=null)
        {
            GameObject temp = _poolingSystem.InstantiateAPS("NiddleMissile", _missilePosition.position, Quaternion.identity, Vector3.one);

            temp.transform.rotation = Quaternion.LookRotation(_FSMManager._targetObject.transform.position + new Vector3(0, 2, 0) - temp.transform.position);
        }
    }

    bool GetTargetDirection()
    {
        if (_FSMManager._targetObject != null)
        {
            if (_FSMManager._targetObject.transform.position.x < transform.position.x)
                return true;
            else
                return false;
        }
        else
            return true;
    }
    public override void OnExit()
    {
        StopAllCoroutines();
    }
}
