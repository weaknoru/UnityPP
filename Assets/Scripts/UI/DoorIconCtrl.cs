using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorIconCtrl : MonoBehaviour
{
    [Header("잠김 이미지 스프라이트"), SerializeField]
    Sprite _lockSprite;
    [Header("열림 이미지 스프라이트"), SerializeField]
    Sprite _openSprite;
    [Header("이미지"), SerializeField]
    Image _image;
    [Header("담당 문"),SerializeField]
    DoorCtrl _doorCtrl;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        if(_doorCtrl._isUnlocked)
        {
            _image.sprite = _openSprite;
        }
        else
        {
            _image.sprite = _lockSprite;
        }
    }
    private void Update()
    {
        if (_doorCtrl._isUnlocked)
        {
            _image.sprite = _openSprite;
        }
        else
        {
            _image.sprite = _lockSprite;
        }
    }
}
