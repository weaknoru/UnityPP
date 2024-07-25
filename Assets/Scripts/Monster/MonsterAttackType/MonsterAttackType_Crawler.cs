using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackType_Crawler : MonsterAttackType
{
    [Header("공격 기준점"), SerializeField]
    Transform _meleeAttackPosition;

    bool _isAttack;
    float _attackRange;
    float _attackDelay;
    protected override void Awake()
    {
        base.Awake();
        _attackRange = 8f;
        _attackDelay = 2f;
    }
    public override void OnEnter()
    {
        _isAttack = false;
        _animator.SetTrigger("Idle");
    }
    public override void Attack()
    {
        base.Attack();
        ReleasePlayer();
        if (!_isAttack &&(_FSMManager._targetObject == null || (_FSMManager._targetObject.transform.position-transform.position).sqrMagnitude>_attackRange))
        {
            _FSMManager.ChangeState(Monster_Chase._Inst);
            return;
        }

        if(!_isAttack)
        {
            StartCoroutine(DoAttack());
        }
    }

    public void MeleeAttack()
    {
        Collider[] hits = Physics.OverlapSphere(_meleeAttackPosition.position, 4);
        foreach (Collider cols in hits)
        {
            if (cols.CompareTag("Player"))
            {
                cols.gameObject.transform.root.GetComponent<PlayerCtrl>().TakeDamage(3);
            }
        }
    }

    IEnumerator DoAttack()
    {
        _isAttack = true;
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(_attackDelay);
        _animator.SetTrigger("Idle");
        _isAttack = false;
    }
    public override void OnExit()
    {
        StopAllCoroutines();
    }
}
