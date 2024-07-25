using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMineEvent : MonoBehaviour
{
    [Header("Áö·Ú ÇÁ¸®ÆÕ"), SerializeField]
    GameObject _minePrefab;
    [Header("Áö·Ú ¼³Ä¡À§Ä¡"), SerializeField]
    Transform _minePosition;
    public void SetMine()
    {
        GameObject temp = Instantiate(_minePrefab);
        temp.transform.position = _minePosition.position;
    }
}
