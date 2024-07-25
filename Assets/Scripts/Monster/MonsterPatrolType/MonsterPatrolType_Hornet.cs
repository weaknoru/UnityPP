using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrolType_Hornet : MonsterPatrolType
{

    bool _isRoatating;
    float _detectRange;
    [Header("ÀÌµ¿ ÁÂÇ¥"), SerializeField]
    GameObject[] _patrolPositions;
    GameObject _patrolPositionObject;

    protected override void Awake()
    {
        base.Awake();
        _speed = 3f;
        _detectRange = 20;
        _rotateSpeed = 180;
    }
    public override void OnEnter()
    {
        _isRoatating = false;
        _patrolPositionObject = _patrolPositions[1];
        _animator.SetTrigger("Idle");
    }
    public override void Patrol()
    {
        base.Patrol();
        FindPlayer();
        ReleasePlayer();
        if (_FSMManager._targetObject != null)
        {
            _FSMManager.ChangeState(Monster_Chase._Inst);
            return;
        }
        if (transform.position == _patrolPositionObject.transform.position)
        {
            if (_patrolPositionObject == _patrolPositions[0])
                _patrolPositionObject = _patrolPositions[1];
            else
                _patrolPositionObject = _patrolPositions[0];
        }
        if (!_isRoatating && GetTargetDirection() == true && transform.eulerAngles.y != 90)
        {
            StartCoroutine(RotateAvatar());
        }
        else if (!_isRoatating && GetTargetDirection() == false && transform.eulerAngles.y != 270)
        {
            StartCoroutine(RotateAvatar());
        }
        else if (!_isRoatating && transform.position != _patrolPositionObject.transform.position)
        {
            if (_patrolPositionObject.transform.position != transform.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, _patrolPositionObject.transform.position, _speed * Time.deltaTime);
            }
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
                    _FSMManager._targetObject = cols.gameObject;
                    return;
                }
            }
            _FSMManager._targetObject = null;
            return;
        }
    }

    bool GetTargetDirection()
    {
        if (_patrolPositionObject.transform.position.x > transform.position.x)
            return true;
        else
            return false;
    }
    IEnumerator RotateAvatar()
    {
        _isRoatating = true;
        while (true)
        {
            if (GetTargetDirection())
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 90, 0), _rotateSpeed * Time.deltaTime);
                if (transform.eulerAngles.y == 90)
                    break;
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 270, 0), _rotateSpeed * Time.deltaTime);
                if (transform.eulerAngles.y == 270)
                    break;
            }
            /*
            if (_targetPositionObject == _patrolPositions[0])
            {
                _monsterAvatar.transform.rotation = Quaternion.RotateTowards(_monsterAvatar.transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), _rotateSpeed * Time.deltaTime);
                if (_monsterAvatar.transform.eulerAngles == new Vector3(0, 90, 0))
                    break;
            }
            else
            {
                _monsterAvatar.transform.rotation = Quaternion.RotateTowards(_monsterAvatar.transform.rotation, Quaternion.Euler(new Vector3(0, 270, 0)), _rotateSpeed * Time.deltaTime);
                if (_monsterAvatar.transform.eulerAngles == new Vector3(0, 270, 0))
                    break;
            }*/
            yield return null;
        }

        _isRoatating = false;
    }


    public override void OnExit()
    {
        StopAllCoroutines();
    }
}
