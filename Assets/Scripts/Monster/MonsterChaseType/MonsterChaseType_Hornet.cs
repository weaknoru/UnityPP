using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseType_Hornet : MonsterChaseType
{
    bool _isRotating;
    float _attackRange;
    float _detectRange;
    protected override void Awake()
    {
        base.Awake();
        _attackRange = 120f;
        _detectRange = 20f;
        _speed = 4f;
        _rotateSpeed = 180;
    }
    public override void OnEnter()
    {
        _isRotating = false;
        _animator.SetTrigger("Idle");
    }
    public override void Chase()
    {
        base.Chase();
        FindPlayer();
        ReleasePlayer();
        if (_FSMManager._targetObject == null)
        {
            _FSMManager.ChangeState(Monster_Patrol._Inst);
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
            if ((_FSMManager._targetObject.transform.position -transform.position).sqrMagnitude > _attackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, _FSMManager._targetObject.transform.position, _speed * Time.deltaTime);
            }
            else
            {
                _FSMManager.ChangeState(Monster_Attack._Inst);
            }
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

    void FindPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _detectRange);
        if (hits.Length > 0)
        {
            foreach (Collider cols in hits)
            {
                if (cols.CompareTag("Player"))
                {
                    _FSMManager._targetObject = cols.gameObject;
                    return;
                }
            }
            _FSMManager._targetObject = null;
            return;
        }
    }
    public override void OnExit()
    {
        StopAllCoroutines();
    }
}
