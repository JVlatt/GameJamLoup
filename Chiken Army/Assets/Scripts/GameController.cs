using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController _myGC;
    public Button _button;
    public GameObject _loup;
    public Image _lifeBar;
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
    private float _timer;
    public int _curentWave { get; private set; } 
    private int _currentEnemy = 0;
    private int _compte = 100000;

    //public int _nbLoupW1;
    //public Vector2 _rangeW1;

    //public int _nbLoupW2;
    //public Vector2 _rangeW2;

    //public int _nbLoupW3;
    //public Vector2 _rangeW3;

    public List<Wave> _waves = new List<Wave>();

    public List<List<Transform>> _roadsTab = new List<List<Transform>>();
    private void Start()
    {
        _myGC = this;
        AddMoney(0);
        Degat(0);
        _manager = GameManager._gameManager;
        _soundControler = SoundControler._soundControler;
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
            _waves[_curentWave]._enemys[_currentEnemy] = loup;
            var lifeBar = Instantiate(_lifeBar, _canvas);
            Enemy enemy = loup.GetComponent<Enemy>();
            enemy.SetWaypoints(_roadsTab[_currentRoad]);
            enemy._lifeBar = lifeBar;

            _currentEnemy++;
            _compte++;
        }
        if (_waves[_curentWave]._enemys.Count <= 0) {
            if (_curentWave == _waves.Count-1)
            {
                _soundControler.PlaySound(_soundControler._victory);
                _manager.Pause(true);
                _win.SetActive(true);
            }
            _button.interactable = true;
        }
    

        /*switch (_waveNum) 
        {
            case 1:
                {
                    if (_timer <= 0 && _compte < _nbLoupW1)
                    {
                        _timer = Random.Range(_rangeW1.x, _rangeW1.y);
                        var loup = Instantiate(_loup, _road1[0].transform.position, new Quaternion());
                        var lifeBar = Instantiate(_lifeBar,_canvas);
                        Enemy enemy = loup.GetComponent<Enemy>();
                        enemy.SetWaypoints(_road1);
                        enemy._lifeBar = lifeBar;

                        _compte++;
                    }
                    if (_compte >= _nbLoupW1) _button.interactable = true;
                    break;
                }
            case 2:
                {
                    if (_timer <= 0 && _compte < _nbLoupW2)
                    {
                        _timer = Random.Range(_rangeW2.x, _rangeW2.y);
                        var loup = Instantiate(_loup, _road2[0].transform.position, new Quaternion());
                        var lifeBar = Instantiate(_lifeBar, _canvas);
                        Enemy enemy = loup.GetComponent<Enemy>();
                        enemy.SetWaypoints(_road2);
                        enemy._lifeBar = lifeBar;
                        _compte++;
                    }
                    if (_compte >= _nbLoupW2) _button.interactable = true;
                    break;
                }
            case 3:
                {
                    if (_timer <= 0 && _compte < _nbLoupW2)
                    {
                        int road = Random.Range(1, 3);
                        if (road == 1)
                        {
                            _timer = Random.Range(_rangeW3.x, _rangeW3.y);
                            var loup = Instantiate(_loup, _road1[0].transform.position, new Quaternion());
                            var lifeBar = Instantiate(_lifeBar, _canvas);
                            Enemy enemy = loup.GetComponent<Enemy>();
                            enemy.SetWaypoints(_road1);
                            enemy._lifeBar = lifeBar;

                            _compte++;
                        }
                        else
                        {
                            _timer = Random.Range(_rangeW3.x, _rangeW3.y);
                            var loup = Instantiate(_loup, _road2[0].transform.position, new Quaternion());
                            var lifeBar = Instantiate(_lifeBar, _canvas);
                            Enemy enemy = loup.GetComponent<Enemy>();
                            enemy.SetWaypoints(_road2);
                            enemy._lifeBar = lifeBar;
                            _compte++;
                        }
                    }
                    if (_compte >= _nbLoupW2) _button.interactable = true;
                    break;
                }
            case 4:
                {
                    _soundControler.PlaySound(_soundControler._victory);
                    _manager.Pause(true);
                    _win.SetActive(true);
                    break;
                }
        }*/
    }

    public void Degat(int amont)
    {
        _hpPlayer -= amont;
        _life.text = _hpPlayer.ToString();
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
        _waveIndic.text = "Wave : " + _curentWave;
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
}
