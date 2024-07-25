using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Death : FSMSingleton<Monster_Death>, IFSMState<MonsterFSMManager>
{


    public void Enter(MonsterFSMManager e)
    {
        Debug.Log("Death");
        e._deathType.OnEnter();

    }
    public void Execute(MonsterFSMManager e)
    {
        e._deathType.Death();

    }
    public void Exit(MonsterFSMManager e) 
    {
        e._deathType.OnExit();
    }
}
