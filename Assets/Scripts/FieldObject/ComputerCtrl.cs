using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerCtrl : MonoBehaviour
{
    [Header("컴퓨터 매터리얼"), SerializeField]
    Renderer _computerRenderer;
    public bool _isOn;
    RaycastHit _hit;
    [Header("센서 감지 지점"), SerializeField]
    GameObject _sensorPosition;
    [Header("잠긴 문"), SerializeField]
    DoorCtrl _lockedDoor;
    [Header("변경 텍스트"), SerializeField]
    string[] _newText1, _newText2;
    [Header("변경할 텍스트 센더"),SerializeField]
    TextSender[] _senders;


    private void Awake()
    {
        _isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(_sensorPosition.transform.position, _sensorPosition.transform.up, out _hit, 10f);
        if (_hit.collider != null && _hit.collider.CompareTag("Player")&&_isOn)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("문 열림");
                _lockedDoor.UnlockDoor();
            }
        }
    }

    public void OnComputer()
    {
        if(!_isOn)
            StartCoroutine(TurnOn());
    }

    IEnumerator TurnOn()
    {
        _isOn = true;
        float startTime = Time.time;
        while(Time.time - startTime <2f)
        {
            float emissiveIntensity = 1.008f;
            Color emissiveColor = _computerRenderer.material.GetColor("_EmissionColor");
            _computerRenderer.material.SetColor("_EmissionColor", emissiveColor * emissiveIntensity);
            yield return null;
        }

        _senders[0].ChangeText(_newText1);
        _senders[1].ChangeText(_newText2);
    }
}
