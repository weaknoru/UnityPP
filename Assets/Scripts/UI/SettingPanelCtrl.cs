using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingPanelCtrl : MonoBehaviour
{

    [Header("������ ����"), SerializeField]
    Slider _masterSlider;
    [Header("����Ʈ ����"), SerializeField]
    Slider _effectSlider;
    [Header("������Ʈ ����"), SerializeField]
    Slider _objectSlider;
    [Header("������� ����"), SerializeField]
    Slider _BGMSlider;

    [Header("�ݱ� ��ư"), SerializeField]
    Button _closeButton;
    [Header("����� �ͼ�"),SerializeField]
    AudioMixer _audioMixer;

    private void Awake()
    {
        _masterSlider.onValueChanged.AddListener(SetMasterVolume);
        _effectSlider.onValueChanged.AddListener(SetEffectVolume);
        _objectSlider.onValueChanged.AddListener(SetObjectVolume);
        _BGMSlider.onValueChanged.AddListener(SetBGMVolume);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _closeButton.onClick.Invoke();
        }

    }

    public void CloseButton()
    {
        gameObject.SetActive(false);   
    }

    public void SetMasterVolume(float volume)
    {
        _audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
    public void SetEffectVolume(float volume)
    {
        _audioMixer.SetFloat("Effect", Mathf.Log10(volume) * 20);
    }
    public void SetObjectVolume(float volume)
    {
        _audioMixer.SetFloat("Object", Mathf.Log10(volume) * 20);
    }
    public void SetBGMVolume(float volume)
    {
        _audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }
}
