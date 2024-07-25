using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    int _weaponIndex, _moveTypeIndex, _itemIndex;
    //��� ���� ����
    [Header("�÷��̾� �ƹ�Ÿ"), SerializeField]
    GameObject _playerAvatar;
    [Header("�̵� ����"), SerializeField]
    MoveType[] _moveStrategy;
    [Header("���� ����"), SerializeField]
    WeaponType[] _weaponStrategy;
    [Header("������ ����"), SerializeField]
    ItemType[] _itemStrategy;
    [Header("���� ������Ʈ"), SerializeField]
    GameObject _weaponObject;
    [Header("�÷��̾� �ִϸ�����"), SerializeField]
    Animator _playerAnimator;

    [Header("������ ����Ʈ �г�"), SerializeField]
    GameObject _damageEffect;
    
    int _maxHealth;
    public int _currentHealth;

    private void Awake()
    {
        _maxHealth = 100;
        _currentHealth = _maxHealth;
        _weaponIndex = 0;
        _moveTypeIndex = 0;
        _itemIndex = 0;
    }
    

    void Update()
    {
        if(_playerAvatar.transform.position.y<-50)
        {
            _currentHealth = 0;
        }
        if (_currentHealth <=0)
        {
            PlayerDeath();
            return;
        }
        _weaponStrategy[_weaponIndex].ShotWeapon();
        _itemStrategy[_itemIndex].UseItem();

        SetWeaponPos();
        ChangeWeapon();
        ChangeItem();
        _moveStrategy[_moveTypeIndex].InputControl();
        
    }
    private void FixedUpdate()
    {
        if(_currentHealth > 0)
            _moveStrategy[_moveTypeIndex].Move();
    }
    public GameObject GetPlayerAvatar()
    {
        return _playerAvatar;
    }
    void SetWeaponPos()
    {
        
        if(Time.timeScale != 0)
        {
            Vector3 targetDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(_weaponObject.transform.position);
            targetDirection = new Vector3(targetDirection.x, targetDirection.y, 0);
            _weaponObject.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
    }
    
    void ChangeWeapon()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            _weaponStrategy[_weaponIndex].ResetWeapon();
            if(_weaponIndex!=_weaponStrategy.Length-1 && _weaponStrategy[_weaponIndex+1]._isActivated)
            {
                _weaponIndex++;
            }
            else //������ ȹ�� ������ �������� �ʾ��� ��� ���� �ʿ�
            {
                _weaponIndex = 0;
            }
        }
    }

    void PlayerDeath()
    {

        _weaponObject.GetComponent<Collider>().isTrigger = false;   
        _weaponObject.GetComponent<Rigidbody>().isKinematic = false;
        _playerAnimator.SetTrigger("Dead");
    }
    void ChangeItem()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (_itemIndex != _itemStrategy.Length - 1 && _itemStrategy[_itemIndex + 1]._isActivated)
            {
                _itemIndex++;
            }
            else //������ ȹ�� ������ �������� �ʾ��� ��� ���� �ʿ�
            {
                _itemIndex = 0;
            }
        }
    }
    public void AddWeapon(int idx)
    {
        _weaponStrategy[idx]._isActivated = true;
    }
    public void AddItem(int idx)
    {
        _itemStrategy[idx]._isActivated = true;
    }
    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageEffect());
        _currentHealth -= damage;
    }

    IEnumerator DamageEffect()
    {
        _damageEffect.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _damageEffect.gameObject.SetActive(false);
    }

    public void Heal(int heal)
    {
        if(_currentHealth+heal >= _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        else
        {
            _currentHealth += heal;
        }
    }

    public WeaponTypeEnum GetWeaponType()
    {
        return _weaponStrategy[_weaponIndex]._weaponTypeEnum;
    }

    public ItemType GetItemType()
    {
        return _itemStrategy[_itemIndex];
    }
}
