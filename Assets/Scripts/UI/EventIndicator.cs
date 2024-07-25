using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventIndicator : MonoBehaviour
{
    [Header("상호작용 텍스트"), SerializeField]
    GameObject _reactText;

    private void Awake()
    {
        _reactText.SetActive(false);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _reactText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _reactText.SetActive(false);
        }
    }
    
}
