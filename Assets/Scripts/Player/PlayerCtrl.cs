using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    int _weaponIndex, _moveTypeIndex, _itemIndex;
    //기능 전략 패턴
    [Header("플레이어 아바타"), SerializeField]
    GameObject _playerAvatar;
    [Header("이동 전략"), SerializeField]
    MoveType[] _moveStrategy;
    [Header("무기 전략"), SerializeField]
    WeaponType[] _weaponStrategy;
    [Header("아이템 전략"), SerializeField]
    ItemType[] _itemStrategy;
    [Header("무기 오브젝트"), SerializeField]
    GameObject _weaponObject;
    [Header("플레이어 애니메이터"), SerializeField]
    Animator _playerAnimator;

    [Header("데미지 이펙트 패널"), SerializeField]
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
            else //아이템 획득 순서가 정해지지 않았을 경우 수정 필요
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
            else //아이템 획득 순서가 정해지지 않았을 경우 수정 필요
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
