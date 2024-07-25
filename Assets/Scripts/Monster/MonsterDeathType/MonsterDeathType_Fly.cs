using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeathType_Fly : MonsterDeathType
{
    [Header("������ٵ�"), SerializeField]
    Rigidbody _rigidbody;
    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
    }
    public override void OnEnter()
    {
        base.OnEnter();
        _rigidbody.constraints = RigidbodyConstraints.None;
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }
    public override void Death()
    {
        base.Death();
    }

    public override void OnExit()
    {
    }
}
