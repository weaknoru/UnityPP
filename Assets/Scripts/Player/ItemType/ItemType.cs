using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemType : MonoBehaviour
{
    public ItemTypeEnum _itemTypeEnum;
    public float _cooldown;
    public bool _isCooldown;
    public abstract void UseItem();
    public bool _isActivated;

    private void Awake()
    {
        _isCooldown = false;
    }

    public float GetCooldown()
    {
        return _cooldown;
    }
}
