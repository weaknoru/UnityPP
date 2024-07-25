using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterDeathType : MonoBehaviour
{
    [Header("���� �ִϸ�����"), SerializeField]
    protected Animator _animator;
    [Header("FSM �Ŵ���"), SerializeField]
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
