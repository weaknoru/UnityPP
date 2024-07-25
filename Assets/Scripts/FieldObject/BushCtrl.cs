using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushCtrl : MonoBehaviour
{
    [Header("덤불 렌더러"), SerializeField]
    Renderer[] _bushRenderers;
    [Header("탄 덤불 매터리얼"), SerializeField]
    Material _burnedMatarials;
    [Header("불길 이펙트"), SerializeField]
    GameObject _fireEffect;
    [Header("덤불 상태 텍스트"), SerializeField]
    TextSender _bushTextEvent;

    [Header("변경 후 텍스트"), SerializeField]
    string[] _newText;

    public void BurnEffect()
    {
        for(int i=0;i<_bushRenderers.Length;i++) 
        {
            _bushRenderers[i].material = _burnedMatarials;
            _fireEffect.SetActive(true);
            StartCoroutine(DestroyObject());
        }
    }

    IEnumerator DestroyObject()
    {
        float startTime = Time.time;
        Vector3 color = new Vector3(1, 1, 1);
        while (Time.time - startTime < 5f)
        { 

            color -= new Vector3(Time.deltaTime, Time.deltaTime, 10*Time.deltaTime);
            _burnedMatarials.color = new Color(color.x,color.y,color.z);
            yield return null;
        }
        _bushTextEvent.ChangeText(_newText);
        Destroy(gameObject);
    }
}
