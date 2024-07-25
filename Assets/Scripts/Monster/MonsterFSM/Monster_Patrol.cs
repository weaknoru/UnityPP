using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Patrol : FSMSingleton<Monster_Patrol>, IFSMState<MonsterFSMManager>
{
    
    public void Enter(MonsterFSMManager e)
    {
        Debug.Log("Patrol");

        e._patrolType.OnEnter();
    }
    public void Execute(MonsterFSMManager e)
    {
        e._patrolType.Patrol();
    }
    public void Exit(MonsterFSMManager e) 
    {
        e._patrolType.OnExit();
    }
}
