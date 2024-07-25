
using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public event EventHandler OnLevelChanged;
    private int _level;
    public int level
    {
        get => _level;
        set
        {
            _level = value;
            OnLevelChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler OnValueChanged;
    private bool _trigger;
    public bool trigger
    {
        get => _trigger;
        set
        {
            _trigger = value;
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            OnValueChanged.Invoke(this,EventArgs.Empty);
        }
    }
}
