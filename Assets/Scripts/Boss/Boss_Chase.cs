using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Chase : FSMSingleton<Boss_Chase>, IFSMState<BossFSMManager>
{
    public void Enter(BossFSMManager e)
    {
        Debug.Log("Chase");
        e._animator.SetTrigger("Walk_Cycle_1");
    }
    public void Execute(BossFSMManager e)
    {
        if (e._currentHealth <= 0)
        {
            StopAllCoroutines();
            e.ChangeState(Boss_Death._Inst);
        }
        else if (e._currentHealth<=1000&&!e._firstPattern)
        {
            e.PatternStart();
        }
        else if(e._currentHealth<=500&&!e._secondPattern)
        {
            e.PatternStart();
        }
        else if(!e._missileCooldown)
        {
            e.ChangeState(Boss_Attack._Inst);
        }
        else if ((e.transform.position - e._targetTransform.position).sqrMagnitude > e._attackRange)
        {
            e.Move();
        }
        else
        {
            e.ChangeState(Boss_Attack._Inst);
        }
    }
    public void Exit(BossFSMManager e) { }
}
