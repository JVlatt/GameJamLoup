using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRotateButton : MonoBehaviour {

    [SerializeField]
    private rangeTower _tower;

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _tower.RotateLeft();
        }
    }
}
