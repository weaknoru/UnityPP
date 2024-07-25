using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Boss_Dash : FSMSingleton<Boss_Dash>, IFSMState<BossFSMManager>
{
    //int _ignoreLayermask;
    float _dashSpeed;
    public void Enter(BossFSMManager e)
    {
        Debug.Log("Dash");
        e._animator.SetTrigger("Walk_Cycle_1");
        //_ignoreLayermask = ~(1 << LayerMask.NameToLayer("Ignore"));
        _dashSpeed = 20;
        StartCoroutine(DashAttack(e));
    }
    public void Execute(BossFSMManager e)
    {
        if(e._currentHealth<=0)
        {
            StopAllCoroutines();
            e.ChangeState(Boss_Death._Inst);
        }
    }
    public void Exit(BossFSMManager e) 
    {
        e._missileCooldown = false;
    }

    IEnumerator DashAttack(BossFSMManager e)
    {
        GameObject currentTarget = e._targetTransform.gameObject;
        e._targetTransform = e._defaultPosition;
        e._animator.SetTrigger("Walk_Cycle_1");
        while ((e.transform.position - e._targetTransform.position).sqrMagnitude > 0.1f)
        {
            e.Move();
            yield return null;
        }
        e._targetTransform = currentTarget.transform;
        Vector3 targetDirection;
        if (e._targetTransform.position.x < e.transform.position.x)
            targetDirection = new Vector3(-1, 0, 0);
        else
            targetDirection = new Vector3(1, 0, 0);

        while (e.transform.rotation != Quaternion.LookRotation(targetDirection))
        {
            e.gameObject.transform.rotation = Quaternion.RotateTowards(e.transform.rotation, Quaternion.LookRotation(targetDirection), 180f * Time.deltaTime);
            yield return null;
        }
        if (e._targetTransform.position.x < e.transform.position.x)
            targetDirection = new Vector3(-1, 0, 0);
        else
            targetDirection = new Vector3(1, 0, 0);
        e._animator.SetTrigger("Intimidate_2");
        yield return new WaitForSeconds(2f);
        e._animator.SetTrigger("Walk_Cycle_1");
        RaycastHit hit = new RaycastHit();
        
        while (true)
        {
            Physics.Raycast(e._missilePosition.position, targetDirection, out hit);//, _ignoreLayermask);
            e._animator.SetFloat("MoveSpeed", 3);
            e.gameObject.transform.position += targetDirection * _dashSpeed * Time.deltaTime;
            if (hit.collider != null)
            {

                if (hit.distance < 2f)
                {
                    e._animator.SetTrigger("Attack_1");
                    yield return new WaitForSeconds(1.2f);
                    break;
                }
            }
            yield return null;
        }
        e._isExhausted = true;
        e._animator.SetTrigger("Sleep");
        e._animator.SetFloat("MoveSpeed", 1);
        yield return new WaitForSeconds(3f);
        e._isExhausted = false;
        e._animator.SetTrigger("Rest_1");

        e.ChangeState(Boss_Chase._Inst);
    }
}
