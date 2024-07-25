using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Default : FSMSingleton<Boss_Default>, IFSMState<BossFSMManager>
{
    bool _isTriggerOn;
    bool _isEncounter;
    public void Enter(BossFSMManager e)
    {
        Debug.Log("Default");

        e._targetTransform = e._defaultPosition;
        e._animator.SetTrigger("Walk_Cycle_1");
        _isTriggerOn = false;
        _isEncounter = false;
    }
    public void Execute(BossFSMManager e)
    {
        if ((e.transform.position - e._targetTransform.position).sqrMagnitude > 1f && !_isEncounter)
        {
            e.Move();
        }
        else if (!_isTriggerOn&& !_isEncounter)
        {
            _isTriggerOn = true;
            e._animator.SetTrigger("Eat_Cycle_1");
        }
        if(e._targetTransform != e._defaultPosition&&!_isEncounter)
        {
            StartCoroutine(Encounter(e));
        }
    }
    public void Exit(BossFSMManager e) { }

    IEnumerator Encounter(BossFSMManager e)
    {
        _isEncounter = true;
        e._animator.SetTrigger("Intimidate_1");
        yield return new WaitForSeconds(2f);
        e.ChangeState(Boss_Chase._Inst);
    }


}
