using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrolType_Spitter : MonsterPatrolType
{
    bool _isRotating;
    float _detectRange;
    bool _currentSide, _targetSide;

    protected override void Awake()
    {
        base.Awake();
        _speed = 1f;
        _detectRange = 15f;
        _rotateSpeed = 90;
        _currentSide = true;
    }
    public override void OnEnter()
    {
        _isRotating = false;
        _currentSide = true;
        _targetSide = _currentSide;
        _animator.SetTrigger("Idle");
        _rotateSpeed = 90;

    }
    public override void Patrol()
    {
        base.Patrol();
        FindPlayer();
        if (_FSMManager._targetObject != null)
        {
            _FSMManager.ChangeState(Monster_Chase._Inst);
            return;
        }
        if (_currentSide == _targetSide&& !_isRotating)
        {
            StartCoroutine(RotateAvatar());
        }
        
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
                    if (cols.gameObject.transform.position.y >= transform.position.y)
                    {
                        _FSMManager._targetObject = cols.gameObject;
                        return;
                    }
                }
            }
            _FSMManager._targetObject = null;
            return;
        }
    }

    IEnumerator RotateTimer()
    {
        yield return new WaitForSeconds(3f);
        _targetSide = !_currentSide;
    }
    IEnumerator RotateAvatar()
    {
        _isRotating = true;
        while (true)
        {
            if (!_targetSide)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 270, 0), _rotateSpeed * Time.deltaTime);
                if (transform.eulerAngles.y == 270)
                {
                    _currentSide = !_currentSide;
                    break;
                }
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 90, 0), _rotateSpeed * Time.deltaTime);
                if (transform.eulerAngles.y == 90)
                {
                    _currentSide = !_currentSide;
                    break;
                }
            }
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        _targetSide = !_targetSide;
        _isRotating = false;
    }


    public override void OnExit()
    {
        StopAllCoroutines();
    }
}
