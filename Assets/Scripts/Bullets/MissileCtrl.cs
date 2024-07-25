using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCtrl : MonoBehaviour
{
    public float _lifeTime = 3;
    private float _bornTime;
    int _damage = 12;
    float _speed = 5;
    [Header("폭발 이펙트"), SerializeField]
    GameObject _explodeEffect;

    void OnEnable()
    {
        _bornTime = Time.time;
        _speed = 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boss"))
        {
            other.gameObject.transform.root.GetComponent<BossFSMManager>().TakeDamage(_damage, other.gameObject.layer, WeaponTypeEnum.Missile);
            Explode();
        }
        else if (!other.CompareTag("Player") && !other.CompareTag("Gun")) 
        {
            Debug.Log(other.tag);
            Explode();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;
        _speed += 30f * Time.deltaTime;
        if (Time.time - _bornTime > _lifeTime)
            DestroyObject();
    }
    void Explode()
    {
        GameObject temp = Instantiate(_explodeEffect);
        temp.transform.position = transform.position;
        Collider[] hits = Physics.OverlapSphere(transform.position, 3f);
        foreach(Collider col in hits)
        {
            if(col.CompareTag("Enemy"))
            {
                col.gameObject.GetComponent<MonsterFSMManager>().TakeDamage(_damage);
            }
            else if (col.CompareTag("Door"))
            {
                col.gameObject.GetComponent<DoorCtrl>().UnlockDoor(WeaponTypeEnum.Missile);
            }
            else if (col.CompareTag("Destroyable"))
            {
                col.gameObject.GetComponent<DestroyableCtrl>().TakeDamage(WeaponTypeEnum.Missile, 10);
            }
            else if(col.CompareTag("Player"))
            {
                col.gameObject.transform.parent.GetComponent<PlayerCtrl>().TakeDamage(2);
            }
        }
        DestroyObject();
    }
    void DestroyObject()
    {
        //파괴 시 할 일(풀링 활용 예정)
        gameObject.DestroyAPS();

    }
}
