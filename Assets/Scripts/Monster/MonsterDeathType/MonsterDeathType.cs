using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterDeathType : MonoBehaviour
{
    [Header("몬스터 애니메이터"), SerializeField]
    protected Animator _animator;
    [Header("FSM 매니저"), SerializeField]
    protected MonsterFSMManager _FSMManager;
    float _startTime;
    public virtual void Death()
    {
        if(Time.time-_startTime >3f)
        {
            Destroy(gameObject);
            _FSMManager.enabled = false;
        }
    }
    
    public virtual void OnEnter()
    {
        _animator.SetTrigger("Die");
        _startTime = Time.time;
    }
    public abstract void OnExit();

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _FSMManager = GetComponent<MonsterFSMManager>();
    }
  
}
