using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChaseType_Crawler : MonsterChaseType
{
    bool _isRoatating;
    float _attackRange;
    protected override void Awake()
    {
        base.Awake();
        _attackRange = 8f;
        _speed = 2f;
        _rotateSpeed = 180;
    }
    public override void OnEnter()
    {
        _isRoatating = false;
        _animator.SetTrigger("Walk");
    }
    public override void Chase()
    {
        base.Chase();
        FindPlayer();
        ReleasePlayer();
        if(_FSMManager._targetObject == null)
        {
            _FSMManager.ChangeState(Monster_Patrol._Inst);
            return;
        }

       if( !_isRoatating && GetTargetDirection()== true && transform.eulerAngles.y != 90) 
       {
           StartCoroutine(RotateAvatar());
       }
       else if (!_isRoatating && GetTargetDirection() == false && transform.eulerAngles.y != 270)
       {
           StartCoroutine(RotateAvatar());
       }
       else if(!_isRoatating && ((GetTargetDirection() == true && transform.eulerAngles.y == 90)||(GetTargetDirection() == false && transform.eulerAngles.y == 270)))
       {
            Vector3 targetPosition = new Vector3(_FSMManager._targetObject.transform.position.x, transform.position.y, transform.position.z);
            if ((_FSMManager._targetObject.transform.position-transform.position).sqrMagnitude > _attackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
            }
            else
            {
                //TODO:공격 state 전환, 멀어지면 순찰 전환
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
        _isRoatating = true;
        while (true)
        {
            if (GetTargetDirection())
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), _rotateSpeed * Time.deltaTime);
                if (transform.eulerAngles == new Vector3(0, 90, 0))
                    break;
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 270, 0)), _rotateSpeed * Time.deltaTime);
                if (transform.eulerAngles == new Vector3(0, 270, 0))
                    break;
            }
            yield return null;
        }
        _isRoatating = false;
    }

    void FindPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 15f);
        if (hits.Length > 0)
        {
            foreach (Collider cols in hits)
            {
                if (cols.CompareTag("Player"))
                {
                    if (cols.gameObject.transform.position.y >= transform.position.y-1)
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
    public override void OnExit()
    {
    }
}
