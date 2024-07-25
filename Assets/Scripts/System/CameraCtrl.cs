using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField]
    GameObject _targetObject;
    Vector3 _startOffset;
    [Header("플레이어블 디렉터"),SerializeField]
    PlayableDirector _director;
    // Start is called before the first frame update
    void Start()
    {
        _startOffset = transform.position-_targetObject.transform.position;
    }
    public void PlayCamerawork(bool on)
    {
        if(on)
            _director.Play();
        if(!on)
            _director.Stop();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,_targetObject.transform.position+_startOffset,10f*Time.deltaTime);
    }
}
