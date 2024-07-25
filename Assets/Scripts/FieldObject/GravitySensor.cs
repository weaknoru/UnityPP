using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySensor : MonoBehaviour
{
    RaycastHit _hit;
    [Header("센서 모드"),SerializeField]
    bool _sensorMode;
    [Header("센서 감지 지점"), SerializeField]
    GameObject _sensorPosition;

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(_sensorPosition.transform.position,_sensorPosition.transform.up,out _hit,10f);
        if(_hit.collider!= null && _hit.collider.CompareTag("Player"))
        {
            if(_sensorMode)
                _hit.collider.GetComponent<GravityInRigidbody>().SetConstantForce(new Vector3(0, 0, 0));
            else
                _hit.collider.GetComponent<GravityInRigidbody>().SetConstantForce(new Vector3(0, -20, 0));
        }
    }
}
