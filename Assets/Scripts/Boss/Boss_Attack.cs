using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Attack : FSMSingleton<Boss_Attack>, IFSMState<BossFSMManager>
{
    bool _isTriggerOn;
    bool _attackCooldown,_isShootingMissile;
    [SerializeField]
    Collider _hitboxCollider;

    public void Enter(BossFSMManager e)
    {
        Debug.Log("Attack");

        _attackCooldown = false;
        _isShootingMissile = false;
    }
    public void Execute(BossFSMManager e)
    {
        if (e._currentHealth <= 0)
        {
            StopAllCoroutines();
            e.ChangeState(Boss_Death._Inst);
        }
        else if (e._currentHealth <= 1000 && !e._firstPattern)
        {
            StopAllCoroutines();
            e.PatternStart();
        }
        else if (e._currentHealth <= 500 && !e._secondPattern)
        {
            StopAllCoroutines();
            e.PatternStart();
        }
        else if (!e._missileCooldown&&!_isShootingMissile)
        {
            StartCoroutine(ShootingMissile(e));
        }
        else if(_isShootingMissile)
        {

        }
        else if(_attackCooldown)
        {

        }
        else if((e.transform.position - e._targetTransform.position).sqrMagnitude <= e._attackRange)
        {
            StartCoroutine(NormalAttack(e));
        }
        else if((e.transform.position - e._targetTransform.position).sqrMagnitude > e._attackRange)
        {
            e.ChangeState(Boss_Chase._Inst);
        }
    }
    public void Exit(BossFSMManager e)
    {
        _attackCooldown = false;
        _isShootingMissile = false;
    }
   
    IEnumerator ShootingMissile(BossFSMManager e)
    {
        _isShootingMissile = true;
        Vector3 targetDirection;
        
        for (int i = 0; i < 3; i++)
        {
            if (e._targetTransform.position.x < e.transform.position.x)
                targetDirection = new Vector3(-1, 0, 0);
            else
                targetDirection = new Vector3(1, 0, 0);
            if(e.transform.rotation != Quaternion.LookRotation(targetDirection))
                e._animator.SetTrigger("Walk_Cycle_1");

            while (e.transform.rotation != Quaternion.LookRotation(targetDirection))
            {
                e.gameObject.transform.rotation = Quaternion.RotateTowards(e.transform.rotation, Quaternion.LookRotation(targetDirection), 180f * Time.deltaTime);
                yield return null;
            }
            e._animator.SetTrigger("Attack_5");
            yield return new WaitForSeconds(1.5f);
            
        }
        _isShootingMissile = false;
        e._missileCooldown = true;
        yield return new WaitForSeconds(10f);
        e._missileCooldown = false;
    }
    IEnumerator NormalAttack(BossFSMManager e)
    {
        _attackCooldown = true;
        e._animator.SetTrigger("Attack_2");
        yield return new WaitForSeconds(0.8f);
        e._animator.SetTrigger("Attack_3");
        yield return new WaitForSeconds(1.5f);
        _attackCooldown = false;
    }
}
