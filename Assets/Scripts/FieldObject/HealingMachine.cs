using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealingMachine : MonoBehaviour
{
    float _healDelay = 0.5f;
    int _healAmount;
    bool _isDelay;
    bool _isContact;
    [Header("저장 UI"), SerializeField]
    GameObject _saveUI;
    [Header("저장 패널"), SerializeField]
    GameObject _savePanel;
    [Header("메시지 패널"), SerializeField]
    GameObject _messagePanel;
    PlayerCtrl _targetPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isContact = true;
            _saveUI.SetActive(true);
            _targetPlayer = other.gameObject.transform.root.GetComponent<PlayerCtrl>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _isContact = false;
            _saveUI.SetActive(false);
            _targetPlayer = null;
        }
    }
    private void Awake()
    {
        _isContact = false;
        _isDelay = false;
        _healAmount = 5;
    }
    // Update is called once per frame
    private void Update()
    {
        if(_isContact)
        {
            if(Input.GetKeyDown(KeyCode.F)&&!_messagePanel.activeSelf)
            {
                Time.timeScale = 0f;
                _savePanel.SetActive(true);
            }
            if(_targetPlayer!=null&&!_isDelay)
            {
                _targetPlayer.Heal(_healAmount);
            }
            StartCoroutine(HealDelay());
        }
    }

    IEnumerator HealDelay()
    {
        _isDelay = true;
        yield return new WaitForSeconds(_healDelay);
        _isDelay = false;
    }
}
