using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRotateButton : MonoBehaviour {

    [SerializeField]
    private rangeTower _tower;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _tower.RotateRight();
        }
    }
}
