using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameLoadManager : MonoBehaviour
{
    public bool _isLoad;
    [Header("���̺� �Ϸ� �г�"), SerializeField]
    GameObject _saveFinishPanel;

    struct GameData
    {
        public Vector3 playerPosition;
        public bool[] activeData;
        public bool computerCondition;
        public bool[] weaponData;
        public bool[] itemData;
    }
    // Start is called before the first frame update
    GameData _loadDataJSON;
    GameData _saveDataJSON;
    [Header("�÷��̾� ��Ʈ�ѷ�"), SerializeField]
    PlayerCtrl _playerCtrl;
    [Header("�÷��̾� �ƹ�Ÿ"), SerializeField]
    GameObject _player;
    [Header("���� ��� ������Ʈ �迭"), SerializeField]
    GameObject[] _targetObjects;
    [Header("��ǻ�� ������Ʈ"), SerializeField]
    ComputerCtrl _computer;
    [Header("���� ����Ʈ"), SerializeField]
    GameObject[] _weapons;
    [Header("������ ����Ʈ"), SerializeField]
    GameObject[] _items;
    [Header("���� ������Ʈ"), SerializeField]
    GameObject _weapon;
    string _saveDataFileName = "saveData";
    void Start()
    {
        _saveDataJSON = new GameData();
        _saveDataJSON.activeData = new bool[_targetObjects.Length];
        _saveDataJSON.computerCondition = false;
        _saveDataJSON.weaponData = new bool[_weapons.Length];
        _saveDataJSON.itemData = new bool[_items.Length];
        //JSON�� ���� _loadObjects�� ���� ����
        _isLoad = SceneManagement.Instance._isLoad;
        if (_isLoad)
        {
            LoadGameData();
        }
    }
    private void LateUpdate()
    {
        if(_isLoad)
        {
            _player.transform.position = _loadDataJSON.playerPosition;
            _isLoad = false;
        }
    }

    public void LoadGameData()
    {
        _player = _playerCtrl.GetPlayerAvatar();
        string filePath = Application.persistentDataPath + "/" + _saveDataFileName;
        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            _loadDataJSON = JsonUtility.FromJson<GameData>(FromJsonData);
        }
        else
        {
            Debug.LogError("Save file missing Error");
            return;
        }
        if (_loadDataJSON.activeData.Length != _targetObjects.Length || _loadDataJSON.weaponData.Length != _weapons.Length
            || _loadDataJSON.itemData.Length != _items.Length)
        {
            Debug.LogError("Data Length Error");
            return;
        }
        else
        {
            for (int i = 0; i < _targetObjects.Length; i++)
            {
                if (!_loadDataJSON.activeData[i])
                    Destroy(_targetObjects[i]);
                //_targetObjects[i].SetActive(_loadDataJSON.activeData[i]);
            }
            if (_loadDataJSON.computerCondition)
            {
                _computer.OnComputer();
            }
            for (int i = 0; i < _weapons.Length; i++)
            {
                if (!_loadDataJSON.weaponData[i])
                {
                    _playerCtrl.AddWeapon(i + 1);
                }
            }
            for (int i = 0; i < _items.Length; i++)
            {
                if (!_loadDataJSON.itemData[i])
                {
                    _playerCtrl.AddItem(i);
                }
            }
        }
    }
 
    public void SaveGameData()
    {
        _saveDataJSON.playerPosition = _player.transform.position;
        for (int i = 0; i < _targetObjects.Length; i++)
        {
            if (_targetObjects[i] == null)
            {
                _saveDataJSON.activeData[i] = false;
            }
            else
            {
                _saveDataJSON.activeData[i] = _targetObjects[i].activeSelf;
            }
        }
        _saveDataJSON.computerCondition = _computer._isOn;
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i] == null)
                _saveDataJSON.weaponData[i] = false;
            else
                _saveDataJSON.weaponData[i] = true;
        }
        for (int i = 0; i < _items.Length; i++)
        {
            if (_items[i] == null)
                _saveDataJSON.itemData[i] = false;
            else
                _saveDataJSON.itemData[i] = true;
        }
        //JSON�� ���̺� ���� ����
        string toJsonData = JsonUtility.ToJson(_saveDataJSON, true);
        string filePath = Application.persistentDataPath + "/" + _saveDataFileName;

        File.WriteAllText(filePath, toJsonData);

        _saveFinishPanel.SetActive(true);
    }


    /*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U)) 
        {
            SaveGameData();
        }
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            LoadGameData();
        }
    }*/
}
