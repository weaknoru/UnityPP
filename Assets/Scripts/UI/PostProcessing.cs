using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;



public class PostProcessing : MonoBehaviour
{
    [Header("����Ʈ ���μ��� ����"), SerializeField]
    PostProcessVolume _volume;
    [Header("�÷��̾�"),SerializeField]
    PlayerCtrl _player;

    Vignette _vignette;
    // Start is called before the first frame update
    void Start()
    {
        _vignette = ScriptableObject.CreateInstance<Vignette>();
        _vignette.enabled.Override(true);
        _vignette.intensity.Override(1f);
        _volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _vignette);
    }

    // Update is called once per frame
    void Update()
    {
        _vignette.intensity.value = 0.8f - (_player._currentHealth) * 0.01f;
    }
}
