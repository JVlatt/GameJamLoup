using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    private List<Transform> _waypoints;
    private int _waypointActuel =-1 ;
    private bool _changeWaypoint;

    public float _speed;
    public int _degat = 1;
    public int _deathReward = 10;
    public GameObject LoupMort;

    private Image _lifeBar;
    private Vector2 _start;
    private Vector2 _end;
    private Vector2 _trajet;

    private SpriteRenderer _mySpriteRenderer;
    private Animator _myAnimator;
    private GameController _gameController;
    private Camera _camera;
    public Vector3 _offset;

    private float _difXY;

    public float _hp = 20;
    private float _hpMax;

    private void Awake()
    {
        _mySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _myAnimator = GetComponentInChildren<Animator>();
        _gameController = GameController._myGC;
        _camera = Camera.main;
    }

    // Use this for initialization
    void Start () {
        _hpMax = _hp;
        _offset = new Vector3(0, 0.5f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        LifeBarPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            LifeController(2);
            Destroy(collision.gameObject);
        }

    }

    public void LifeController(int degat)
    {
        if (_hp > 0)
            _hp -= degat;
        if (_hp <= 0)
        {
            SoundControler._soundControler.PlaySound(SoundControler._soundControler._loupDeath);
            if(_gameController != null)_gameController.RemoveLoup(gameObject);
            Destroy(gameObject);
            var mort = Instantiate(LoupMort,transform.position,_mySpriteRenderer.transform.rotation);
            Destroy(mort, 1);
            if (_gameController != null) _gameController.AddMoney(_deathReward);
            Destroy(_lifeBar.gameObject);
        }
        _lifeBar.fillAmount = (_hp / _hpMax);
    }

    private void Move()
    {
        _start = transform.position;
        _end = _waypoints[_waypointActuel + 1].transform.position;
        _trajet = _end - _start;
        if (_changeWaypoint) Animation();
        if (_trajet.magnitude < 0.1 && _waypointActuel == _waypoints.Count - 2)
        {
            if (_gameController != null)
            {
                _gameController.Degat(_degat);
                _gameController.RemoveLoup(gameObject);
            }
            Destroy(gameObject);
            Destroy(_lifeBar.gameObject);
        }
        if (_trajet.magnitude < 0.1 && _waypointActuel != _waypoints.Count - 2)
        {
            _waypointActuel += 1;
            _changeWaypoint = true;
        }
        _trajet = _trajet.normalized;
        transform.Translate(_trajet * _speed * Time.deltaTime);
    }

    private void Animation()
    {
        _difXY = Mathf.Abs(_trajet.x) - Mathf.Abs(_trajet.y);
        _myAnimator.SetFloat("y", _trajet.y);
        _myAnimator.SetFloat("dif", _difXY);

        if (_trajet.x < 0) _mySpriteRenderer.flipX = true;
        else _mySpriteRenderer.flipX = false;
    }

    private void LifeBarPosition()
    {
        _lifeBar.transform.position = _camera.WorldToScreenPoint(transform.position + _offset);
    }

    public void SetupEnemy(List<Transform> _newWaypoints,Image _newLifeBar)
    {
        _waypoints = _newWaypoints;
        _lifeBar = _newLifeBar;
    }
}
