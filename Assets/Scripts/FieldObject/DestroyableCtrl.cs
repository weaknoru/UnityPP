using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableCtrl : MonoBehaviour
{
    [Header("대응 무기 타입"),SerializeField]
    WeaponTypeEnum _weaponType;
    int _durability;
    private void Awake()
    {
        _durability = 10;
    }
    public void TakeDamage(WeaponTypeEnum weaponType, int damage)
    {
        if (_weaponType == weaponType) 
        {
            _durability -= damage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_durability <= 0)
        {
            Destroy(gameObject);
        }
    }
}
