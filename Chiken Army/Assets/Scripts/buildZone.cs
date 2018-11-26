using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildZone : MonoBehaviour {

    [SerializeField]
    private GameObject _buildButton;
   
    
    [SerializeField]
    private GameObject _subMenu;

    private bool _isBuildMenuActive;
    private bool _isSubMenuActive;
/*
    protected bool _isBuildMenuActive;

    protected bool _isSubMenuActive;
        
 */   
  
    public void SwitchOff()
    {
        _isSubMenuActive = false;
        _isBuildMenuActive = false;
    }
    private void Awake()
    {
        _isBuildMenuActive = false;
        _isSubMenuActive = false;
    }

    private void OnMouseOver()
    {
        if(_isBuildMenuActive == false && _isSubMenuActive == false)
        {
            _buildButton.SetActive(true);
            _isBuildMenuActive = true;
        }
    }
    private void OnMouseExit()
    {
        if (_isBuildMenuActive == true && _isSubMenuActive == false)
        {
            _buildButton.SetActive(false);
            _isBuildMenuActive = false;
        }
    }

    private void Update()
    {
        if (_isBuildMenuActive && Input.GetMouseButtonDown(0) && _isSubMenuActive == false)
        {
            Debug.Log("Ouverture Menu");
            _isSubMenuActive = true;
            _subMenu.SetActive(true);
        }

        if(_isSubMenuActive)
            _buildButton.SetActive(false);
    }

}
