using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSender : MonoBehaviour
{
    [Header("1회용 이벤트 여부"), SerializeField]
    bool _isOnce;
    public TextManager _textManager;
    [Header("출력 텍스트"), SerializeField]
    string[] _texts;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(_isOnce)
            {
                _textManager._curSender = this;
                StartCoroutine(_textManager.EventText(_texts));
            }
            else
            {
                _textManager._curSender = this;
                StartCoroutine(_textManager.SetText(_texts));
            }
        }
     }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 1f;
            StopAllCoroutines();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        /*
        if(other.CompareTag("Player")&&Input.GetKeyDown(KeyCode.F))
        {
            if(_curTextIndex<=_texts.Length-1)
            {
                _textManager.OpenText(_texts[_curTextIndex]);
                _curTextIndex++;
            }
            else
            {
                _curTextIndex = 0;
                _textManager.CloseText();
            }
        }
        //*/
    }
    public void ChangeText(string[] newText)
    {
        _texts = newText;
    }
}
