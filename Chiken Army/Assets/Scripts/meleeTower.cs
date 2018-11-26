using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeTower : MonoBehaviour {

    public int _degat;
    public float _cooldown;
    private int _rotateDirection = 0;

    private BoxCollider2D _aggroRange;

    private float _timer;
    private Animator _animatorController;

    private void Start()
    {
        _aggroRange = GetComponent<BoxCollider2D>();
        _rotateDirection = 0;
        _animatorController = GetComponent<Animator>();
        SetRotation();
    }
    private void Update()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(1);
        if (collision.tag == "Enemy" && _timer <= 0)
        {
            collision.GetComponent<Enemy>().LifeController(_degat);
            _timer = _cooldown;
        }
    }
    public void RotateRight()
    {
        if (_rotateDirection == 0)
        {
            _rotateDirection = 3;
        }
        else
        {
            _rotateDirection -= 1;
        }
        SetRotation();
    }
    public void RotateLeft()
    {
        if (_rotateDirection == 3)
        {
            _rotateDirection = 0;
        }
        else
        {
            _rotateDirection += 1;
        }
        SetRotation();
    }
    private void SetRotation()
    {


        switch (_rotateDirection)
        {
            case 0:
                
                _aggroRange.offset = new Vector2(1, 0);
                _aggroRange.size = new Vector2(4, 1.5f);
                break;
            case 1:
                
                _aggroRange.offset = new Vector2(0, -1);
                _aggroRange.size = new Vector2(1.5f, 4);
                break;
            case 2:
                
                _aggroRange.offset = new Vector2(-1, 0);
                _aggroRange.size = new Vector2(4, 1.5f);
                break;
            case 3:
                
                _aggroRange.offset = new Vector2(0, 1);
                _aggroRange.size = new Vector2(1.5f, 4);
                break;
        }
        _animatorController.SetInteger("_animRotateDirection", _rotateDirection);
    }
}
