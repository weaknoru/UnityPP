using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMineEvent : MonoBehaviour
{
    [Header("���� ������"), SerializeField]
    GameObject _minePrefab;
    [Header("���� ��ġ��ġ"), SerializeField]
    Transform _minePosition;
    public void SetMine()
    {
        GameObject temp = Instantiate(_minePrefab);
        temp.transform.position = _minePosition.position;
    }
}
