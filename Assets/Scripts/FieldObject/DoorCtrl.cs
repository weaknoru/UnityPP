using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCtrl : MonoBehaviour
{
    public WeaponTypeEnum _keyWeaponType;
    public bool _isUnlocked;
    Vector3 _startPosition;
    bool _isOpened;
    float _openSpeed;
    [Header("문 중앙 위치"), SerializeField]
    Transform _doorCenterPosition;
    [Header("문 오브젝트"), SerializeField]
    GameObject _doorObject;
    Light _lockLight;
    [Header("문 색상 렌더러"), SerializeField]
    MeshRenderer _doorRenderer;
    [Header("해제 시 색상"), SerializeField]
    Color _unlockedColor;
    [Header("해제 시 매터리얼"), SerializeField]
    Material _unlockedMaterial;
    [Header("잠금 시 색상"), SerializeField]
    Color _lockedColor;
    [Header("잠금 시 매터리얼"), SerializeField]
    Material _lockedMaterial;


    int _machineLayer;
    private void Awake()
    {
        _isOpened = false;
        _isUnlocked = false;
        _openSpeed = 5f;
        _lockLight = GetComponent<Light>();
        _machineLayer = ~(1 << LayerMask.NameToLayer("DoorMachine"));
        _startPosition = transform.position;
    }
    public void UnlockDoor(WeaponTypeEnum weaponType)
    {
        if(weaponType == _keyWeaponType)
        {
            _isUnlocked = true;
            _lockLight.color = _unlockedColor;
            _doorRenderer.material = _unlockedMaterial;
        }
    }
    public void UnlockDoor()
    {
        _isUnlocked = true;
        _lockLight.color = _unlockedColor;
        _doorRenderer.material = _unlockedMaterial;
    }
    public void LockDoor()
    {
        _isUnlocked = false;
        _lockLight.color = _lockedColor;
        _doorRenderer.material = _lockedMaterial;
        StopAllCoroutines();
        StartCoroutine(CloseDoor());
    }
    void Update()
    {
        if(CheckPlayer()&& !_isOpened && _isUnlocked)
        {
            StartCoroutine(OpenDoor());
        }
        if (Input.GetKeyDown(KeyCode.O))
            UnlockDoor();
    }
    bool CheckPlayer()
    {
        bool playerDetected = false;
        RaycastHit hit;
        Physics.Raycast(_doorCenterPosition.position, new Vector3(1, 0, 0), out hit, 5f,_machineLayer);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
            playerDetected = true;
        Physics.Raycast(_doorCenterPosition.position, new Vector3(-1, 0, 0), out hit,5f,_machineLayer);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
            playerDetected = true;

        return playerDetected;
    }
    IEnumerator CloseDoor()
    {
        _isOpened = false;
        while (transform.position != _startPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position,_startPosition,_openSpeed*Time.deltaTime);
            yield return null;
        }
    }
    IEnumerator OpenDoor()
    {
        float startTime = Time.time;
        _isOpened = true;
        while(Time.time-startTime < 1f)
        {
            transform.position += new Vector3(0, 1, 0) * _openSpeed * Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(5f);
        while (CheckPlayer())
        {
            yield return null;
        }
        startTime = Time.time;
        while (Time.time - startTime < 1f)
        {
            transform.position += new Vector3(0, -1, 0) * _openSpeed * Time.deltaTime;
            yield return null;
        }
        _isOpened = false;
    }
   
}
