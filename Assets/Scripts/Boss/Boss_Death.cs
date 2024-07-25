using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Death : FSMSingleton<Boss_Death>, IFSMState<BossFSMManager>
{
    public void Enter(BossFSMManager e)
    {
        Debug.Log("Death");
        
        e._animator.SetTrigger("Die");
        e.gameObject.GetComponent<BossFSMManager>().enabled = false;
    }
    public void Execute(BossFSMManager e)
    {

    }
    public void Exit(BossFSMManager e) { }
}
