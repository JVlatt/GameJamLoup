using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rangeTower : MonoBehaviour {

    private bool _canShoot = true;
    [SerializeField]
    private float _cooldownTime;
    private float _timer;

    [SerializeField]
    private GameObject _projectile;
    private GameObject _currentProjectile;
    private Rigidbody2D _projectileRigidbody;

    [SerializeField]
    private GameObject _leftRotate;
    [SerializeField]
    private GameObject _rightRotate;

    private int _rotateDirection;

    [SerializeField]
    private Vector2 _projectileDirection = new Vector2(5,0);

    private BoxCollider2D _aggroRange;
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
        if (_timer >= 0)
        {
            _timer -= Time.deltaTime;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.tag == "Enemy" && _timer <= 0)
        {
             Shoot();
            
            _timer = _cooldownTime;
        }

    }

    private void Shoot()
    {
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._tire);
        _currentProjectile = Instantiate(_projectile, transform.position, Quaternion.identity);
        _currentProjectile.GetComponent<Rigidbody2D>().velocity = _projectileDirection;
        Destroy(_currentProjectile, 5);

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
            case 0 :
                _projectileDirection = new Vector2(5, 0);
                _aggroRange.offset = new Vector2(4, 0);
                _aggroRange.size = new Vector2(10, 1);
                break;
            case 1 :
                _projectileDirection = new Vector2(0, -5);
                _aggroRange.offset = new Vector2(0, -4);
                _aggroRange.size = new Vector2(1, 10);
                break;
            case 2 :
                _projectileDirection = new Vector2(-5, 0);
                _aggroRange.offset = new Vector2(-4, 0);
                _aggroRange.size = new Vector2(10, 1);
                break;
            case 3 :
                _projectileDirection = new Vector2(0, 5);
                _aggroRange.offset = new Vector2(0, 4);
                _aggroRange.size = new Vector2(1, 10);
                break;
        }
        _animatorController.SetInteger("_animRotateDirection", _rotateDirection);
    }

}
