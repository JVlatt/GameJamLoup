using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cancelBuild : MonoBehaviour
{
    public buildZone _myBuildZone = null;
    public GameObject _subMenu;

    private void Awake()
    {

    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            

            _myBuildZone.SwitchOff();
            _subMenu.SetActive(false);
        }
    }
}
