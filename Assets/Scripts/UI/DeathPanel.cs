using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class DeathPanel : MonoBehaviour
{
    [Header("로비 전환 텍스트"), SerializeField]
    TextMeshProUGUI _lobbyText;
    [Header("패널 색"), SerializeField]
    Image _panelImage;

    bool _reactable;
    private void Update()
    {
        if( _reactable )
        {
            if(Input.GetMouseButtonDown(0)) 
            {
                SceneManagement.instance.GotoTitleScene();
            }
        }
    }
    private void OnEnable()
    {
        _reactable = false;
        _lobbyText.gameObject.SetActive(false);
        StartCoroutine(DeathEffect());
    }
    IEnumerator DeathEffect()
    {
        float alphaVal=0;
        while(alphaVal<0.8f)
        {
            _panelImage.color = new Color(0, 0, 0, alphaVal);
            alphaVal+= 0.002f;
            yield return null;
        }
        _lobbyText.gameObject.SetActive(true);
        _reactable = true;
    }
}
