using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCtrl : MonoBehaviour
{
    float _laserDelay=0.5f;
    int _damage = 6;
    bool _isDelay;

    private void OnEnable()
    {
        _isDelay = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Map") || other.CompareTag("Floor") || other.CompareTag("Bush"))
        {

        }
        else if (other.CompareTag("Door"))
        {
            other.gameObject.GetComponent<DoorCtrl>().UnlockDoor(WeaponTypeEnum.Laser);
        }
        else if (other.CompareTag("Enemy"))
        {
            //利 疙吝 矫 贸府
            if(!_isDelay) 
            {
                StartCoroutine(StartDelay());
                other.GetComponent<MonsterFSMManager>().TakeDamage(_damage);
            }
        }
        else if (other.CompareTag("Boss"))
        {
            //利 疙吝 矫 贸府
            if (!_isDelay)
            {
                StartCoroutine(StartDelay());
                other.gameObject.transform.root.GetComponent<BossFSMManager>().TakeDamage(_damage, other.gameObject.layer, WeaponTypeEnum.Laser);
            }
        }
        else if (other.CompareTag("Destroyable"))
        {
            other.gameObject.GetComponent<DestroyableCtrl>().TakeDamage(WeaponTypeEnum.Laser, 2);
        }
        else if(other.CompareTag("Computer"))
        {
            other.gameObject.GetComponent<ComputerCtrl>().OnComputer();
        }
    }
    IEnumerator StartDelay()
    {
        _isDelay = true;
        yield return new WaitForSeconds(_laserDelay);
        _isDelay = false;
    }
}
