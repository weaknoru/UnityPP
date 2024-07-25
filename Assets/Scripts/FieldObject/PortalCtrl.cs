using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCtrl : MonoBehaviour
{
    [Header("�ݴ��� ��Ż"), SerializeField]
    PortalCtrl _exitPortal;
    [Header("�ⱸ ��ġ"), SerializeField]
    public Transform _exitPosition;

    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(_exitPosition.position, new Vector3(0,1,0), out hit);
        if(hit.collider.gameObject.CompareTag("Player")&&Input.GetKeyDown(KeyCode.W))
        {
            hit.collider.gameObject.transform.position = _exitPortal._exitPosition.position;
        }
    }
}
