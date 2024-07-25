using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingPanelCtrl : MonoBehaviour
{

    [Header("¸¶½ºÅÍ º¼·ý"), SerializeField]
    Slider _masterSlider;
    [Header("ÀÌÆåÆ® º¼·ý"), SerializeField]
    Slider _effectSlider;
    [Header("¿ÀºêÁ§Æ® º¼·ý"), SerializeField]
    Slider _objectSlider;
    [Header("¹è°æÀ½¾Ç º¼·ý"), SerializeField]
    Slider _BGMSlider;

    [Header("´Ý±â ¹öÆ°"), SerializeField]
    Button _closeButton;
    [Header("¿Àµð¿À ¹Í¼­"),SerializeField]
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
