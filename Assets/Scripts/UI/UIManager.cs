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
    [Header("�̴ϸ� �г�"), SerializeField]
    Canvas _MapCanvas;
    [Header("�޴� �г�"), SerializeField] //TODO : �̱������� ����
    GameObject _menuPanel;
    [Header("���� ����"), SerializeField]
    TextMeshProUGUI _weaponText;
    [Header("������ ����"), SerializeField]
    TextMeshProUGUI _itemText;
    [Header("������ ��ٿ�"), SerializeField]
    TextMeshProUGUI _itemCooldown;
    [Header("�޴� ��ư"), SerializeField]
    Button _menuButton;
    [Header("������ ����Ʈ �г�"), SerializeField]
    GameObject _damageEffectPanel;
    [Header("ü�� ������"), SerializeField]
    Scrollbar _healthBar;
    [Header("��� �г�"), SerializeField]
    GameObject _deathPanel;
    [Header("���� �г�"), SerializeField]
    GameObject _savePanel;

    [Header("�÷��̾� ��Ʈ�ѷ�"), SerializeField]
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
        // �÷��̾��� ������ �ٲ� �� �����ϰ� ���� ����
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
