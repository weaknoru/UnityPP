using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System;
//using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [Header("미니맵 패널"), SerializeField]
    Canvas _MapCanvas;
    [Header("메뉴 패널"), SerializeField] //TODO : 싱글턴으로 구현
    GameObject _menuPanel;
    [Header("무기 정보"), SerializeField]
    TextMeshProUGUI _weaponText;
    [Header("아이템 정보"), SerializeField]
    TextMeshProUGUI _itemText;
    [Header("아이템 쿨다운"), SerializeField]
    TextMeshProUGUI _itemCooldown;
    [Header("메뉴 버튼"), SerializeField]
    Button _menuButton;
    [Header("데미지 이펙트 패널"), SerializeField]
    GameObject _damageEffectPanel;
    [Header("체력 게이지"), SerializeField]
    Scrollbar _healthBar;
    [Header("사망 패널"), SerializeField]
    GameObject _deathPanel;
    [Header("저장 패널"), SerializeField]
    GameObject _savePanel;

    [Header("플레이어 컨트롤러"), SerializeField]
    PlayerCtrl _playerCtrl;

    public int intTest;
    public bool boolTest;
    public EventManager _eventManager;


    private void Awake()
    {
        _eventManager = GetComponent<EventManager>();   
        _eventManager.level = 0;
        _eventManager.OnLevelChanged += Instance_OnLevelChanged;
        _eventManager.OnValueChanged += Instance_OnBoolChanged;
        _eventManager.trigger = boolTest;
    }
    private void Instance_OnLevelChanged(object sender, System.EventArgs e)
    {
        // 플레이어의 레벨이 바뀔 때 실행하고 싶은 내용
        Debug.Log("Good");
    }
    private void Instance_OnBoolChanged(object sender, System.EventArgs e)
    {
        Debug.Log("boolChanged");
    }
    bool _isCooldown = false;
    // Update is called once per frame
    void Update()
    {
        if (_playerCtrl._currentHealth<=0)
        {
            if(_deathPanel.activeSelf == false)
                _deathPanel.SetActive(true);
            return;
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            _MapCanvas.gameObject.SetActive(true);
        }
        else
        {
            _MapCanvas.gameObject.SetActive(false);
        }
        _healthBar.size = _playerCtrl._currentHealth / 100f;


        WeaponUIControl();
        ItemUIControl();
        MenuPanelControl();
    }

    public void WeaponUIControl()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _weaponText.text = _playerCtrl.GetWeaponType().ToString();
        }
    }
    public void CloseSavePanel()
    {
        _savePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void ItemUIControl()
    {
        ItemType tempItem = _playerCtrl.GetItemType();
        if (!tempItem._isActivated)
        {
            _itemText.text = "None";
            return;
        }
        else
        {
            _itemText.text = tempItem._itemTypeEnum.ToString();
            if (!tempItem._isCooldown)
            {
                _itemCooldown.gameObject.SetActive(false);
            }
            else
            {
                _itemCooldown.gameObject.SetActive(true);
                if(!_isCooldown)
                {
                    StartCoroutine(CooldownUI(tempItem));
                }
            }
        }
    }

    IEnumerator CooldownUI(ItemType itemType)
    {
        _isCooldown = true;
        float time = itemType._cooldown + Time.time;
        while(time - Time.time >=0)
        {
            _itemCooldown.text = ((int)(time - Time.time)).ToString();
            yield return null;
        }
        _isCooldown = false;
    }
    public void MenuButton()
    {
        if (_menuPanel.activeSelf == false)
        {
            _menuPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    void MenuPanelControl()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            if(_menuPanel.activeSelf == false)
            {
                _menuPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
