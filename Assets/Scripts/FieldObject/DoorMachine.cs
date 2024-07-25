using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMachine : MonoBehaviour
{
    RaycastHit _hit;
    [Header("제어할 문"), SerializeField]
    DoorCtrl _door;
    [Header("센서 감지 지점"), SerializeField]
    GameObject _sensorPosition;
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.F)&&other.CompareTag("Player"))
        {
            _door.UnlockDoor();
        }
    }
    private void Update()
    {
        Physics.Raycast(_sensorPosition.transform.position, _sensorPosition.transform.up, out _hit, 10f);
        if (_hit.collider != null && _hit.collider.CompareTag("Player"))
        {
            //TODO:문 잠겼다고 설명하는 스토리 진행
            _door.LockDoor();
        }
    }
}
