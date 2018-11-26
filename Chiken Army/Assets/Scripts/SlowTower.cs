using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTower : MonoBehaviour
{

    private Enemy _target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _target = collision.gameObject.GetComponent<Enemy>();
            _target._speed = _target._speed/2;
            Debug.Log("Enemy Detected");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            _target = collision.gameObject.GetComponent<Enemy>();
            _target._speed *= 2;
        }
    }
}
