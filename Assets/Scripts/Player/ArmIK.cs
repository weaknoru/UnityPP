using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class ArmIK : MonoBehaviour
{
    [Header("플레이어 컨트롤러"),SerializeField]
    PlayerCtrl _playerCtrl;
    Animator _animator;
    [Header("왼손 기준점"), SerializeField]
    public Transform _leftHandPosition;
    [Header("오른손 기준점"), SerializeField]
    public Transform _rightHandPosition;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void OnAnimatorIK(int layerIndex)
    {
        if (_playerCtrl._currentHealth<=0)
        {
            return;
        }

        if (_leftHandPosition != null )
        {
            _animator.SetIKPositionWeight(
            AvatarIKGoal.LeftHand, 1f);

            _animator.SetIKRotationWeight(
                AvatarIKGoal.LeftHand, 1f);

            _animator.SetIKPosition(
                AvatarIKGoal.LeftHand,
                _leftHandPosition.position);
            _animator.SetIKRotation(
                AvatarIKGoal.LeftHand,
                _leftHandPosition.rotation);
        }
        
        if(_rightHandPosition != null )
        {
            _animator.SetIKPositionWeight(
            AvatarIKGoal.RightHand, 1f);

            _animator.SetIKRotationWeight(
                AvatarIKGoal.RightHand, 1f);

            _animator.SetIKPosition(
                AvatarIKGoal.RightHand,
                _rightHandPosition.position);

            _animator.SetIKRotation(
                AvatarIKGoal.RightHand,
                _rightHandPosition.rotation);
        }
    }
}
