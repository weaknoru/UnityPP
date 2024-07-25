using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    [Header("텍스트 오브젝트 리스트"), SerializeField]
    TextSender[] _textSenders;
    [Header("텍스트 창"), SerializeField]
    GameObject _textWindow;
    [Header("텍스트 칸"), SerializeField]
    TextMeshProUGUI _text;
    public TextSender _curSender;

    private void Awake()
    {
        _textWindow.SetActive(false);
        for(int i=0;i<_textSenders.Length;i++) 
        {
            _textSenders[i]._textManager = this;
        }
    }
   public IEnumerator SetText(string[] texts)
    {
        int curIndex = 0;
        //_textWindow.SetActive(true);
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                Time.timeScale = 0f;
                if (curIndex != texts.Length)
                {
                    if (!_textWindow.activeSelf)
                        _textWindow.SetActive(true);
                    _text.text = texts[curIndex];
                }
                else
                {
                    curIndex = -1;
                    Time.timeScale = 1f;
                    _textWindow.SetActive(false);
                }
                curIndex++;
            }
            yield return null;
        }
    }

    public IEnumerator EventText(string[] texts)
    {
        int curIndex = 0;
        Time.timeScale = 0f;
        _textWindow.SetActive(true);
        _text.text = texts[curIndex];
        curIndex++;
        //_textWindow.SetActive(true);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Time.timeScale = 0f;
                if (curIndex < texts.Length)
                {
                    if (!_textWindow.activeSelf)
                        _textWindow.SetActive(true);
                    _text.text = texts[curIndex];
                }
                else
                {
                    Time.timeScale = 1f;
                    _textWindow.SetActive(false);
                    break;
                }
                curIndex++;
            }
            yield return null;
        }
        _curSender.gameObject.SetActive(false);
    }

    public void OpenText(string text)
    {
        _textWindow.SetActive(true);
        _text.text = text;
    }
    public void CloseText()
    {
        _textWindow.SetActive(false);
    }

}
