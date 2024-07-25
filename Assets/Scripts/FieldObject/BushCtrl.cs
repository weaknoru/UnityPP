using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushCtrl : MonoBehaviour
{
    [Header("���� ������"), SerializeField]
    Renderer[] _bushRenderers;
    [Header("ź ���� ���͸���"), SerializeField]
    Material _burnedMatarials;
    [Header("�ұ� ����Ʈ"), SerializeField]
    GameObject _fireEffect;
    [Header("���� ���� �ؽ�Ʈ"), SerializeField]
    TextSender _bushTextEvent;

    [Header("���� �� �ؽ�Ʈ"), SerializeField]
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
