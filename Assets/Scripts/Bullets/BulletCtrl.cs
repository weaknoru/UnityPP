using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    int _damage = 3;
    float _speed = 20;
    public float _lifeTime = 2;
    private float _bornTime;
    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Gun")||other.CompareTag("Player"))
        {
            return;
        }
        else if(other.CompareTag("Door"))
        {
            other.gameObject.GetComponent<DoorCtrl>().UnlockDoor(WeaponTypeEnum.Normal);
            //DestroyObject();
        }
        else if(other.CompareTag("Enemy"))
        {
            //적 명중 시 처리
            other.gameObject.GetComponent<MonsterFSMManager>().TakeDamage(_damage);
            //DestroyObject();
        }
        else if(other.CompareTag("Boss"))
        {
            other.gameObject.transform.root.GetComponent<BossFSMManager>().TakeDamage(_damage, other.gameObject.layer, WeaponTypeEnum.Normal);
            //DestroyObject();
        }
        else if(other.CompareTag("Destroyable"))
        {
            other.gameObject.GetComponent<DestroyableCtrl>().TakeDamage(WeaponTypeEnum.Normal,2);
            //DestroyObject();
        }
        DestroyObject();

    }

    void OnEnable()
    {
        _bornTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward*_speed * Time.deltaTime;
        if (Time.time -_bornTime > _lifeTime)
            DestroyObject();
           
    }

    void DestroyObject()
    {
        gameObject.DestroyAPS();
    }
}
