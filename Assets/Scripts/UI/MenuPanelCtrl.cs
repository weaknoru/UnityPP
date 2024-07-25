using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuPanelCtrl : MonoBehaviour
{
    [Header("현재 선택 표시"), SerializeField]
    GameObject _targetIndicator;
    [Header("버튼 리스트"), SerializeField]
    List<Button> _buttonList;
    [Header("세팅 패널"), SerializeField]
    GameObject _settingPanel;

    int _targetIndex;

    private void Awake()
    {
        _targetIndex = 0;
    }
    private void OnEnable()
    {
        _targetIndex = 0;
        _settingPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.W))
        {
            if(_targetIndex == 0) 
            {
                _targetIndex = _buttonList.Count - 1;
            }
            else
            {
                _targetIndex--;
            }
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            if (_targetIndex == _buttonList.Count -1)
            {
                _targetIndex = 0;
            }
            else
            {
                _targetIndex++;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _buttonList[_targetIndex].onClick.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(_settingPanel.activeSelf == false)
                ResumeButton();
        }
        _targetIndicator.transform.localPosition = _buttonList[_targetIndex].transform.localPosition;
    }
    

    public void LobbyButton()
    {
        _targetIndex = 0;
        SceneManagement.instance.GotoTitleScene();
    }
    public void SettingButton()
    {
        _targetIndex = 1;
        if(_settingPanel.activeSelf == false)
        {
            _settingPanel.SetActive(true);
        }
    }
    public void ResumeButton()
    {
        _targetIndex = 2;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
