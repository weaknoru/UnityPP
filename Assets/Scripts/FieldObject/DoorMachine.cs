using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMachine : MonoBehaviour
{
    RaycastHit _hit;
    [Header("������ ��"), SerializeField]
    DoorCtrl _door;
    [Header("���� ���� ����"), SerializeField]
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
            //TODO:�� ���ٰ� �����ϴ� ���丮 ����
            _door.LockDoor();
        }
    }
}
