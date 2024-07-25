using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour
{
    float _startTime;
    float _damageDelay = 0.5f;
    bool _isDelay;
    float _lifeTime = 5f;
    // Start is called before the first frame update
    void Awake()
    {
        _startTime = Time.time;
        _isDelay = false;
        StartCoroutine(DamageDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - _startTime >= _lifeTime)
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Bush"))
        {
            other.gameObject.GetComponent<BushCtrl>().BurnEffect();
        }
        if(!_isDelay)
        {
            if(other.CompareTag("Player"))
            {
                other.gameObject.transform.parent.GetComponent<PlayerCtrl>().TakeDamage(2);
            }
            else if(other.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<MonsterFSMManager>().TakeDamage(5);
            }
            else if (other.CompareTag("Boss"))
            {
                other.gameObject.GetComponent<BossFSMManager>().TakeDamage(5);
            }
        }
    }

    IEnumerator DamageDelay()
    {
        while(true)
        {
            _isDelay = true;
            yield return new WaitForSeconds(_damageDelay);
            _isDelay = false;
            yield return null;
        }
    }
}
