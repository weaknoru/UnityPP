using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BossFSMManager : FSM<BossFSMManager>
{
    float _maxHealth;
    [Header("보스 현재 체력"),SerializeField]
    public float _currentHealth;
    [HideInInspector]
    public Animator _animator;
    float _headArmor, _chestArmor, _bodyArmor;
    float _moveSpeed;
    [HideInInspector]
    public float _attackRange;
    [HideInInspector]
    public bool _isExhausted,_missileCooldown;
    [HideInInspector]
    public bool _firstPattern, _secondPattern;
    [Header("투사체 프리팹"), SerializeField]
    GameObject _missilePrefab;
    [Header("투사체 발사 위치"), SerializeField]
    public Transform _missilePosition;
    [Header("기본 위치"), SerializeField]
    public Transform _defaultPosition;
    [SerializeField]
    public Transform _targetTransform;
    [Header("공격 기준점"), SerializeField]
    Transform _meleeAttackPosition;
    private PoolingSystem _poolingSystem;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _maxHealth = 1500;
        _currentHealth = _maxHealth;
        _isExhausted = false;
        _headArmor = 2;
        _chestArmor = 0;
        _bodyArmor = 1;
        _targetTransform = _defaultPosition;
        _firstPattern = false;
        _secondPattern = false;
        _moveSpeed = 8;
        _attackRange = 60;
        InitState(this, Boss_Default._Inst);
        //처음 플레이어로 타겟 설정되면 미사일 쿨 돌고시작하게
    }

    private void Start()
    {
        _poolingSystem = PoolingSystem._instance;
    }
    private void Update()
    {
        FSMUpdate();
        //TEMP : 보스 체력 임의 감소 로직
        if(Input.GetKeyDown(KeyCode.Return)) { _currentHealth -= 500; }
    }
    public void Move()
    {
        Vector3 targetPosition;
        if (_targetTransform.position.x < transform.position.x)
            targetPosition = new Vector3(-1, 0, 0);
        else
            targetPosition = new Vector3(1, 0, 0);
        if (transform.rotation != Quaternion.LookRotation(targetPosition))
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetPosition), 180f * Time.deltaTime);
            return;
        }
        else
            transform.position += targetPosition.normalized * _moveSpeed * Time.deltaTime;
    }
    
   
    public void PatternStart()
    {
        if(!_firstPattern)
        {
            _firstPattern = true;
            ChangeState(Boss_Dash._Inst);
        }
        else if(!_secondPattern)
        {
            _secondPattern = true;
            ChangeState(Boss_Dash._Inst);
        }
    }
    public IEnumerator ShowTarget()
    {
        Vector3 targetDirection;
        if (_targetTransform.position.x < transform.position.x)
            targetDirection = new Vector3(-1, 0, 0);
        else
            targetDirection = new Vector3(1, 0, 0);

        while (transform.rotation != Quaternion.LookRotation(transform.position-targetDirection))
        {
            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(transform.position-targetDirection), 10f * Time.deltaTime);
            yield return null;
        }
    }
    public void SetTarget(GameObject target)
    {
        _targetTransform = target.transform;
    }
    public bool IsDead()
    {
        if (_currentHealth <= 0)
            return true;
        else
            return false;
    }

    //애니메이터 이벤트용(필요 시 코드 분리)
    public void MissileAttack()
    {
        GameObject tempMissile = _poolingSystem.InstantiateAPS("BoneMissile", _missilePosition.transform.position, _missilePosition.transform.rotation, 2*Vector3.one);
    }
    public void MeleeAttack()
    {
        Collider[] hits = Physics.OverlapSphere(_meleeAttackPosition.position, 10);
        foreach(Collider cols in hits)
        {
            if(cols.CompareTag("Player"))
            {
                cols.gameObject.transform.root.GetComponent<PlayerCtrl>().TakeDamage(8);
            }
        }
    }
    public void DashAttack()
    {
        Collider[] hits = Physics.OverlapSphere(_meleeAttackPosition.position, 10);
        foreach (Collider cols in hits)
        {
            if (cols.CompareTag("Player"))
            {
                cols.gameObject.transform.root.GetComponent<PlayerCtrl>().TakeDamage(30);
            }
        }
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }
    public void TakeDamage(int damage, int layerNumber, WeaponTypeEnum type)
    {
        float totalDamage;
        if (layerNumber == 10)
        {
            totalDamage = damage - _headArmor;
            if (_isExhausted)
                totalDamage *= 1.3f;
            if (type == WeaponTypeEnum.Missile)
                totalDamage *= 1.2f;
        }
        else if (layerNumber == 11)
        {
            totalDamage = damage - _chestArmor;
            if (_isExhausted)
                totalDamage *= 1.1f;
            if (type == WeaponTypeEnum.Normal)
                totalDamage *= 1.1f;
        }
        else if (layerNumber == 12)
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
}
