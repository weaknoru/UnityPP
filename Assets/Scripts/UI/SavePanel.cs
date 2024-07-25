using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePanel : MonoBehaviour
{

    [Header("저장 완료 패널"), SerializeField]
    GameObject _saveClearPanel;

    private void OnEnable()
    {
        _saveClearPanel.SetActive(false);
    }
   
}
