using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCtrl : MonoBehaviour
{
    Light _light;
    [Header("»≠ø∞ ø¿∫Í¡ß∆Æ «¡∏Æ∆’"), SerializeField]
    GameObject _firePrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        _light = GetComponent<Light>();
        StartCoroutine(Setted());
    }

    IEnumerator Setted()
    {
        for(int i=0;i<2;i++)
        {
            _light.intensity = 5;
            yield return new WaitForSeconds(0.5f);
            _light.intensity = 1;
            yield return new WaitForSeconds(0.5f);
        }
        for (int i = 0; i < 6; i++)
        {
            _light.intensity = 5;
            yield return new WaitForSeconds(0.25f);
            _light.intensity = 1;
            yield return new WaitForSeconds(0.25f);
        }

        GameObject temp = Instantiate(_firePrefab);
        temp.transform.position = transform.position;
        Destroy(gameObject);
    }
}
