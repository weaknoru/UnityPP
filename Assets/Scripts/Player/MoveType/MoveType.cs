using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveType :MonoBehaviour
{
    //�̵� ���� ����
    protected float _speed;
    protected Animator _animator;
    protected float _rotateSpeed;
    protected float _rotateOffset=1;
    protected Rigidbody _rigidBody;
    [Header("�ƹ�Ÿ"),SerializeField]
    protected GameObject _playerAvatar;

    public abstract void Move();
    public abstract void InputControl();
   
    private void Awake()
    {
        _speed = 10;
        _rotateSpeed = 10;
        _animator=transform.parent.GetComponent<Animator>();
        _rigidBody = _playerAvatar.GetComponent<Rigidbody>();
    }
    //�������� �۵��ϴ� ȸ�� �Լ�
    protected void RotateAvatar()
    {
        Vector3 mouseDirection = Input.mousePosition - Camera.main.WorldToScreenPoint(_playerAvatar.transform.position);
        if (mouseDirection.x < 0)
        {
            if (Mathf.Abs(_playerAvatar.transform.eulerAngles.y + 90) > _rotateOffset)
            {
                _playerAvatar.transform.rotation = Quaternion.Lerp(_playerAvatar.transform.rotation, Quaternion.Euler(new Vector3(0, -90, 0)), _rotateSpeed * Time.deltaTime);
            }
        }
        else if (mouseDirection.x > 0)
        {
            if (Mathf.Abs(_playerAvatar.transform.eulerAngles.y - 90) > _rotateOffset)
            {
                _playerAvatar.transform.rotation = Quaternion.Lerp(_playerAvatar.transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), _rotateSpeed * Time.deltaTime);
            }
        }
    }
}
