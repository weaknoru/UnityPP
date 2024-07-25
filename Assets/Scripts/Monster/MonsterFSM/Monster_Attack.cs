using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Attack : FSMSingleton<Monster_Attack>, IFSMState<MonsterFSMManager>
{

    public void Enter(MonsterFSMManager e)
    {
        Debug.Log("Attack");

        e._attackType.OnEnter();
    }
    public void Execute(MonsterFSMManager e)
    {
        e._attackType.Attack();
    }
    public void Exit(MonsterFSMManager e)
    {
        e._attackType.OnExit();
    }
}
