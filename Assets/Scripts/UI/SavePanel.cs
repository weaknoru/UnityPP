using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePanel : MonoBehaviour
{

    [Header("���� �Ϸ� �г�"), SerializeField]
    GameObject _saveClearPanel;

    private void OnEnable()
    {
        _saveClearPanel.SetActive(false);
    }
   
}
