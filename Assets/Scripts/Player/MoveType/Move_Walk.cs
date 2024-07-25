using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Move_Walk : MoveType
{
    float _jumpVal=900;
    bool _isJump = false;
    bool _jumpDelay = false;
    bool _doJump = false;
    
    public override void Move()
    {
        //������� �����ϴ� ��Ȳ ����
        _isJump = true;
        if(!_jumpDelay) 
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit))
            {
                if (hit.distance < 1f)
                {
                    _isJump = false;
                }
            }
        }
        
        //�̵� �� �������� ȸ�� ����
        if(Input.GetAxis("Horizontal")<0)
        {
            _playerAvatar.transform.position += new Vector3(-_speed, 0, 0) * Time.deltaTime;
            _animator.SetFloat("Speed", _speed);
        }
        else if(Input.GetAxis("Horizontal")>0)
        {
            _playerAvatar.transform.position += new Vector3(_speed, 0, 0) * Time.deltaTime;
            _animator.SetFloat("Speed", _speed);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
        //����
        if (_doJump&& !_isJump)
        {
            _isJump = true;
            StartCoroutine(JumpDelay());
            _rigidBody.AddForce(new Vector3(0, _jumpVal, 0), ForceMode.Force);
        }

        RotateAvatar();
    }

    public override void InputControl()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isJump)
        {
            _doJump = true;
        }
        
    }
    //���� ���� �ٷ� ���� ��� ����
    IEnumerator JumpDelay()
    {
        _jumpDelay = true;
        yield return new WaitForSeconds(0.5f);
        _jumpDelay = false;
        _doJump=false;
    }
}
