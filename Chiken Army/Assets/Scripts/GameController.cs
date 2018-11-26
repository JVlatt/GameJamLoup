using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public static GameController _me;
    public Button _button;
    public GameObject _loup;
    public Image _lifeBar;
    public Transform _canvas;
    public GameObject[] _road1;
    public GameObject[] _road2;
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
    private int _wave = 0;
    private int _compte=0;

    public int _nbLoupW1;
    public Vector2 _rangeW1;

    public int _nbLoupW2;
    public Vector2 _rangeW2;

    public int _nbLoupW3;
    public Vector2 _rangeW3;

    private void Start()
    {
        _me = this;
        AddMoney(0);
        Degat(0);
        _manager = GameManager._gameManager;
        _soundControler = SoundControler._soundControler;
    }

    // Update is called once per frame
    void Update () {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        Spawn();
	}

    private void Spawn()
    {
        switch (_wave) 
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
        }
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
        _wave++;
        _compte = 0;
        _button.interactable = false;
        _waveIndic.text = "Wave : " + _wave;
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
        _me = null;
    }
}
