using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeLeftRotate : MonoBehaviour {

    [SerializeField]
    private meleeTower _tower;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _tower.RotateLeft();
        }
    }
}
