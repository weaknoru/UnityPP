using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFSMManager : FSM<MonsterFSMManager>
{
    [Header("몬스터 체력"),SerializeField]
    int _maxHealth;
    public int _currentHealth;
    //float _armor;
    

    public MonsterPatrolType _patrolType;
    public MonsterAttackType _attackType;
    public MonsterChaseType _chaseType;
    public MonsterDeathType _deathType;
    //temp
    [SerializeField]
    public GameObject _targetObject;
    private void Awake()
    {
        _patrolType = GetComponent<MonsterPatrolType>();
        _attackType = GetComponent<MonsterAttackType>();
        _chaseType = GetComponent<MonsterChaseType>();
        _deathType = GetComponent<MonsterDeathType>();
        _currentHealth = _maxHealth;
        InitState(this, Monster_Patrol._Inst);
        
    }

    public void TakeDamage(int  damage)
    {
        _currentHealth -= damage;
    }
    private void Update()
    {
        FSMUpdate();
    }
}
