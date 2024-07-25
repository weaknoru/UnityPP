using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IconCtrl : MonoBehaviour
{
    [Header("Ÿ�� ������Ʈ"), SerializeField]
    GameObject _targetObject;
    [Header("���� �� ������ ��ǥ"), SerializeField]
    Transform[] _sides;
    [Header("�� �̹��� ũ��"), SerializeField]
    RectTransform _mapTransform;
    

    RectTransform _iconTransform;
    float _mapWidth;
    float _mapHeight;
    float _gameWidth;
    float _gameHeight;

    private void Awake()
    {
        _iconTransform = GetComponent<RectTransform>();
        _mapHeight = _mapTransform.sizeDelta.y;
        _mapWidth = _mapTransform.sizeDelta.x;
        //_gameHeight = _sides[2].position.y - _sides[0].position.y;
        _gameWidth = _sides[1].position.x - _sides[0].position.x;
        _gameHeight = _gameWidth * _mapHeight / _mapWidth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_targetObject == null)
        {
            Destroy(gameObject);
            return;
        }
        _iconTransform.anchoredPosition = 
            new Vector3((_targetObject.transform.position.x-_sides[0].position.x) * _mapWidth / _gameWidth, 
            (_targetObject.transform.position.y-_sides[0].transform.position.y) * _mapHeight / _gameHeight, 0);
    }

    
}
