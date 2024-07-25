using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossroomCtrl : MonoBehaviour
{
    RaycastHit _hit;
    [Header("플레이어 감지 지점"), SerializeField]
    GameObject _sensorPosition;
    [Header("보스 매니저"), SerializeField]
    BossFSMManager _bossFSMManager;
    [Header("출입구"), SerializeField]
    DoorCtrl[] _doors;
    [Header("카메라 컨트롤러 "),SerializeField]
    CameraCtrl _cameraCtrl;
    bool _playEncounter = true;
    public bool _bossDeath;

    private void Start()
    {
        _bossDeath = _bossFSMManager.IsDead();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (_playEncounter)
            {
                _cameraCtrl.PlayCamerawork(true);
                //_cameraCtrl.PlayCamerawork(false);
                _playEncounter = false;
            }
            _bossFSMManager.SetTarget(other.gameObject);
            for (int i = 0; i < _doors.Length; i++)
            {
                _doors[i].LockDoor();
            }
        }
    }
    void Update()
    {
        /*
        Physics.Raycast(_sensorPosition.transform.position, _sensorPosition.transform.up, out _hit, 10f);
        if (_hit.collider != null && _hit.collider.CompareTag("Player"))
        {
            if(_playEncounter)
            {
                _cameraCtrl.PlayCamerawork();
            }
            _bossFSMManager.SetTarget(_hit.collider.gameObject);
            for(int i=0;i<_doors.Length;i++) 
            {
                _doors[i].LockDoor();
            }
        }*/
        if(_bossFSMManager.IsDead()&&!_bossDeath)
        {
            _bossDeath = true;
            for (int i = 0; i < _doors.Length; i++)
            {
                _doors[i].UnlockDoor();
            }
            _cameraCtrl.PlayCamerawork(true);
        }
    }
}
