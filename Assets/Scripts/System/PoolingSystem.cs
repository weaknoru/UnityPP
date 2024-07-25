
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[AddComponentMenu("Custom/PoolingSystem")]

public sealed class PoolingSystem : MonoBehaviour
{
    [System.Serializable]
    public class PoolingUnit
    {
        public string name;

        public GameObject _prefObj;

        public int _amount;


        int _curAmount;
        public int CurAmount
        {
            get { return _curAmount; }
            set { _curAmount = value; }
        }

    }
    public static PoolingSystem _instance;

    public PoolingUnit[] _poolingUnits;

    public List<GameObject>[] _pooledUnitsList;

    public int _defPoolAmount = 10;
    public bool _canPoolExpand = true;
    void Awake()
    {
        _instance = this;

        _pooledUnitsList = new List<GameObject>[_poolingUnits.Length];

        for (int i = 0; i < _poolingUnits.Length; i++)
        {
            _pooledUnitsList[i] = new List<GameObject>();

            if (_poolingUnits[i]._amount > 0)
                _poolingUnits[i].CurAmount = _poolingUnits[i]._amount;
            else
                _poolingUnits[i].CurAmount = _defPoolAmount;

            int idx = 0;
            for (int j = 0; j < _poolingUnits[i].CurAmount; j++)
            {
                GameObject newItem = (GameObject)Instantiate(_poolingUnits[i]._prefObj);
                string suffix = "_" + idx;
                AddToPooledUnitList(i, newItem, suffix);

                ++idx;

            }

        }

    }
    GameObject GetPooledItem(string pooledObjName)
    {
        for (int unitIdx = 0; unitIdx < _poolingUnits.Length; ++unitIdx)
        {

            if (_poolingUnits[unitIdx]._prefObj.name == pooledObjName)
            {
                int listIdx = 0;
                for (listIdx = 0; listIdx < _pooledUnitsList[unitIdx].Count; ++listIdx)
                {
       
                    if (_pooledUnitsList[unitIdx][listIdx] == null)
                        return null;

                    if (_pooledUnitsList[unitIdx][listIdx].activeInHierarchy == false)
                        return _pooledUnitsList[unitIdx][listIdx];

                }
                if (_canPoolExpand)
                {

                    GameObject tmpObj = (GameObject)Instantiate(_poolingUnits[unitIdx]._prefObj);
                    string suffix = "_" + listIdx.ToString() + "( " + (listIdx - _poolingUnits[unitIdx].CurAmount + 1).ToString() + " )";

                    AddToPooledUnitList(unitIdx, tmpObj, suffix);

                    return tmpObj;

                }

                break;

            }
        }

        return null;

    }
    void AddToPooledUnitList(int idx, GameObject newItem, string suffix)
    {
        newItem.name += suffix;

        newItem.SetActive(false);


        newItem.transform.parent = transform;

        _pooledUnitsList[idx].Add(newItem);

    }
    public GameObject InstantiateAPS(
        int idx,
        GameObject parent = null)
    {
        string pooledObjName = _poolingUnits[idx].name;

        GameObject tmp = InstantiateAPS(pooledObjName, Vector3.zero,
                                        _poolingUnits[idx]._prefObj.transform.rotation,
                                        _poolingUnits[idx]._prefObj.transform.localScale,
                                        parent);

        return tmp;

    }
    public GameObject InstantiateAPS(
        int idx,
        Vector3 pos,
        Quaternion rot,
        Vector3 scale,
        GameObject parent = null)
    {
        string pooledObjName = _poolingUnits[idx].name;

        GameObject tmp = InstantiateAPS(pooledObjName, pos, rot, scale, parent);

        return tmp;

    }
    public GameObject InstantiateAPS(
        string pooledObjName,
        GameObject parent = null)
    {
        GameObject tmpObj = GetPooledItem(pooledObjName);

        tmpObj.SetActive(true);

        return tmpObj;

    }
    public GameObject InstantiateAPS(
        string pooledObjName,
        Vector3 pos,
        Quaternion rot,
        Vector3 scale,
        GameObject parent = null)
    {
        GameObject tmpObj = GetPooledItem(pooledObjName);

        if (tmpObj != null)
        {
            if (parent != null)
                tmpObj.transform.parent = parent.transform;

            tmpObj.transform.position = pos;
            tmpObj.transform.rotation = rot;
            tmpObj.transform.localScale = scale;
            tmpObj.SetActive(true);

        }

        return tmpObj;

    }
    public List<GameObject> GetActivatePooledItem()
    {
        List<GameObject> tmps = new List<GameObject>();

        for (int unitIdx = 0; unitIdx < _poolingUnits.Length; ++unitIdx)
        {
            for (int listIdx = 0; listIdx < _pooledUnitsList[unitIdx].Count; ++listIdx)
            {
                if (_pooledUnitsList[unitIdx][listIdx].activeInHierarchy)
                    tmps.Add(_pooledUnitsList[unitIdx][listIdx]);

            }
        }

        return tmps;
    }

    public static void DestroyAPS(GameObject obj) { obj.SetActive(false); }



    public static void PlayEffect(ParticleSystem particleSystem)
    {
        if (particleSystem == null)
            return;

        particleSystem.gameObject.SetActive(true);

        particleSystem.Play();

    }
    public static void PlayEffect(GameObject effObj)
    {
        ParticleSystem tmp = effObj.GetComponent<ParticleSystem>();

        PlayEffect(tmp);

    }
    public static void PlayEffect(ParticleSystem particleSystem, int emitCount)
    {
        if (particleSystem == null)
            return;

        particleSystem.gameObject.SetActive(true);

        particleSystem.Emit(emitCount);

    }
    public static void PlayEffect(GameObject effObj, int emitCount)
    {
        ParticleSystem tmp = effObj.GetComponent<ParticleSystem>();

        PlayEffect(tmp, emitCount);

    }
    public static void PlaySoundRepeatedly(
        GameObject soundObj,
        float volume = 1.0f)
    {
        AudioSource tmp = soundObj.GetComponent<AudioSource>();

        if (tmp.isPlaying)
            return;

        if (tmp)
        {
            tmp.Play();
            tmp.loop = true;
            tmp.volume = volume;

        }

    }
    public static void PlaySound(
        GameObject soundObj,
        float volume = 1.0f)
    {
        AudioSource tmp = soundObj.GetComponent<AudioSource>();

        if (tmp)
        {
            tmp.PlayOneShot(tmp.clip);
            tmp.volume = volume;

        }
    }
    public static void StopSound(GameObject soundObj)
    {
        AudioSource tmp = soundObj.GetComponent<AudioSource>();

        if (tmp)
            tmp.Stop();

    }

}
public static class PoolingSystemExtensions
{
    public static void DestroyAPS(this GameObject obj)
    { PoolingSystem.DestroyAPS(obj); }
    public static void PlaySoundRepeatedly(this GameObject soundObj, float volume = 1.0f)
    { PoolingSystem.PlaySoundRepeatedly(soundObj, volume); }
    public static void PlaySound(this GameObject soundObj, float volume = 1.0f)
    { PoolingSystem.PlaySound(soundObj, volume); }
    public static void StopSound(this GameObject soundObj)
    { PoolingSystem.StopSound(soundObj); }
    public static void PlayEffect(this GameObject effObj, int emitCount) { PoolingSystem.PlayEffect(effObj, emitCount); }
    public static void PlayEffect(this GameObject effObj) { PoolingSystem.PlayEffect(effObj); }
}