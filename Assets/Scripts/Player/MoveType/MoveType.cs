using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveType :MonoBehaviour
{
    //이동 전략 패턴
    protected float _speed;
    protected Animator _animator;
    protected float _rotateSpeed;
    protected float _rotateOffset=1;
    protected Rigidbody _rigidBody;
    [Header("아바타"),SerializeField]
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
    //공통으로 작동하는 회전 함수
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
