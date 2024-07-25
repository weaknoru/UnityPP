using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Chase : FSMSingleton<Monster_Chase>, IFSMState<MonsterFSMManager>
{

    public void Enter(MonsterFSMManager e)
    {
        Debug.Log("Chase");

        e._chaseType.OnEnter();
    }
    public void Execute(MonsterFSMManager e)
    {
        e._chaseType.Chase();
    }
    public void Exit(MonsterFSMManager e)
    {
        e._chaseType.OnExit();
    }
}
