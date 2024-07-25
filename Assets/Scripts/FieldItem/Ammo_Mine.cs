using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Mine : MonoBehaviour
{
    [Header("플레이어 매니저"), SerializeField]
    PlayerCtrl _player;
    float _rotateSpeed;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.AddItem(0);
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        _rotateSpeed = 30f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, _rotateSpeed * Time.deltaTime, 0));
    }
}
