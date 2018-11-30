using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController _myGC;

    public Button _button;
    public Image _lifeBar;
    public int _LifeBarRapprt;
    public Transform _canvas;
    public List<Transform> _roads = new List<Transform>();
    public Text _gold;
    public Text _life;
    public Text _waveIndic;
    public GameObject _win;
    public GameObject _loose;

    public int _hpPlayer;
    public int _money;

    private SoundControler _soundControler;
    private GameManager _manager;
    private Animator _animCamera;

    private float _timer;
    public int _curentWave { get; private set; }
    private int _currentEnemy = 0;
    private int _compte = 100000;
    private List<GameObject> _loupSpawn = new List<GameObject>(); 


    public List<Wave> _waves = new List<Wave>();

    public List<List<Transform>> _roadsTab = new List<List<Transform>>();
    private void Awake()
    {
        _myGC = this;
        _manager = GameManager._gameManager;
        _soundControler = SoundControler._soundControler;
        _animCamera = GetComponentInParent<Animator>();
        AddMoney(0);
        _life.text = _hpPlayer.ToString();
        _curentWave = -1;
        _roads.Insert(0, new GameObject().transform);
        int i = 0;
        foreach (var road in _roads)
        {
            _roadsTab.Add(new List<Transform>());
            road.GetComponentsInChildren<Transform>(_roadsTab[i]);
            _roadsTab[i].Remove(road);
            i++;
        }
    }

    // Update is called once per frame
    void Update () {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        if(_curentWave>=0) Spawn();
	}

    private void Spawn()
    {
        int _currentRoad;
        if (_waves[_curentWave]._road == 0 || _waves[_curentWave]._road >= _roadsTab.Count)
        {
            _currentRoad = Random.Range(1, _roadsTab.Count);
        }
        else _currentRoad = _waves[_curentWave]._road;
        if (_timer <= 0 && _compte < _waves[_curentWave]._enemys.Count)
        {
            _timer = Random.Range(_waves[_curentWave]._range.x, _waves[_curentWave]._range.y);
            var loup = Instantiate(_waves[_curentWave]._enemys[_currentEnemy], _roadsTab[_currentRoad][0].transform.position, new Quaternion());
            _loupSpawn.Add(loup);
            var lifeBar = Instantiate(_lifeBar, _canvas);
            Enemy enemy = loup.GetComponent<Enemy>();
            enemy.SetupEnemy(_roadsTab[_currentRoad],lifeBar,_LifeBarRapprt);

            _currentEnemy++;
            _compte++;
        }
        if (_loupSpawn.Count <= 0 && _compte <= _waves[_curentWave]._enemys.Count) {
            if (_curentWave == _waves.Count-1)
            {
                _soundControler.PlaySound(_soundControler._victory);
                _manager.Pause(true);
                _win.SetActive(true);
            }
            _button.interactable = true;
        }
   
    }

    public void Degat(int amont)
    {
        _hpPlayer -= amont;
        _life.text = _hpPlayer.ToString();
        _animCamera.SetTrigger("Shake");
        if(_hpPlayer <= 0)
        {
            _soundControler.PlaySound(_soundControler._gameOver);
            _manager.Pause(true);
            _loose.SetActive(true);
        }
    }


    public void NextWave()
    {
        _curentWave++;
        _currentEnemy = 0;
        _compte = 0;
        _button.interactable = false;
        _waveIndic.text = "Wave : " + (_curentWave + 1).ToString();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void AddMoney(int amont)
    {
        _money += amont;
        _gold.text = _money.ToString();
    }

    private void OnDestroy()
    {
        _myGC = null;
    }

    public void RemoveLoup(GameObject loup)
    {
        _loupSpawn.Remove(loup);
    }
}
