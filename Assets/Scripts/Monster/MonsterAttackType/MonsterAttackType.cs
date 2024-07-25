using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterAttackType : MonoBehaviour
{
    //이동 전략 패턴
    protected float _speed;
    [Header("몬스터 애니메이터"), SerializeField]
    protected Animator _animator;
    protected float _rotateSpeed;
    protected float _rotateOffset = 1;
    [Header("FSM 매니저"), SerializeField]
    protected MonsterFSMManager _FSMManager;
    [Header("플레이어 추적 경계"),SerializeField]
    float _leftBoundary,_rightBoundary;
    public virtual void Attack()
    {
        if (_FSMManager._currentHealth <= 0)
        {
            StopAllCoroutines();
            _FSMManager.ChangeState(Monster_Death._Inst);
            return;
        }
    }
    public abstract void OnEnter();
    public abstract void OnExit();

    protected virtual void Awake()
    {
        _speed = 10;
        _rotateSpeed = 90;
        _animator = GetComponent<Animator>();
        _FSMManager = GetComponent<MonsterFSMManager>();
    }

    protected void ReleasePlayer()
    {
        if(_FSMManager._targetObject !=null && 
            (_FSMManager._targetObject.transform.position.x < _leftBoundary || _FSMManager._targetObject.transform.position.x > _rightBoundary))
        {
            _FSMManager._targetObject = null;
        }
    }
}
