using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject _myAudio;
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _optionMenu;
    [SerializeField]
    private GameObject _loup;
    [SerializeField]
    private Image _lifeBar;
    [SerializeField]
    private GameObject _road;
    [SerializeField]
    private float _cooldown;
    
    private Transform _canvas;
    private float _timer;
    private List<Transform> _waypoints = new List<Transform>();


    private float _speed = 1.5f;

    private Animator _myAnimator;

    private void Start()
    {
        _myAnimator = gameObject.GetComponent<Animator>();
        _road.GetComponentsInChildren<Transform>(_waypoints);
        _waypoints.Remove(_road.transform);
        _canvas = transform;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        if (_timer <= 0) Spawn();
    }

    private void Spawn()
    {
        _timer = _cooldown;
        var loup = Instantiate(_loup, _waypoints[0].position, new Quaternion());
        var lifeBar = Instantiate(_lifeBar, _canvas);
        Enemy enemy = loup.GetComponent<Enemy>();
        enemy.SetupEnemy(_waypoints, lifeBar);
    }

    public void PlayButton()
    {
        DontDestroyOnLoad(_myAudio);
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._click);
        StartCoroutine("Wait");
        
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OptionButton()
    {
        _myAnimator.SetTrigger("option");
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._transi1);
    }

    public void Backbutton()
    {
        _myAnimator.SetTrigger("main");
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._transi2);
    }
    IEnumerator Wait()
    {      
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
