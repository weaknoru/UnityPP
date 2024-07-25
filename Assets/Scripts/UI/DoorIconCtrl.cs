using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorIconCtrl : MonoBehaviour
{
    [Header("��� �̹��� ��������Ʈ"), SerializeField]
    Sprite _lockSprite;
    [Header("���� �̹��� ��������Ʈ"), SerializeField]
    Sprite _openSprite;
    [Header("�̹���"), SerializeField]
    Image _image;
    [Header("��� ��"),SerializeField]
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
