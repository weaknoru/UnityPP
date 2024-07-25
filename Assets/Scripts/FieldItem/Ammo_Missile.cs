using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Missile : MonoBehaviour
{
    [Header("�÷��̾� �Ŵ���"), SerializeField]
    PlayerCtrl _player;
    float _rotateSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _player.AddWeapon(1);
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        _rotateSpeed = 90f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0, _rotateSpeed * Time.deltaTime, 0);
    }
}
