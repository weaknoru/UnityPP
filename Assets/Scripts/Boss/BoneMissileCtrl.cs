using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneMissileCtrl : MonoBehaviour
{
    public float _lifeTime = 3;
    private float _bornTime;
    [Header("투사체 데미지"),SerializeField]
    int _damage = 5;
    float _speed = 15;
    void OnEnable()
    {
        _bornTime = Time.time;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Map") || other.CompareTag("Floor") || other.CompareTag("Door"))
        {
            DestroyObject();
        }
        else if (other.CompareTag("Player"))
        {
            other.gameObject.transform.parent.GetComponent<PlayerCtrl>().TakeDamage(_damage);
            DestroyObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

        if (Time.time - _bornTime > _lifeTime)
            DestroyObject();
    }

    void DestroyObject()
    {
        gameObject.DestroyAPS();
    }
}
