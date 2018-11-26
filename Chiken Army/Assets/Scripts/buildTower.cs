using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildTower : MonoBehaviour {

    [SerializeField]
    private GameObject _buildZone;

    [SerializeField]
    private GameObject _newTower;
    private GameObject _currentTower;

    [SerializeField]
    private int _cost = 50;

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0) && GameController._me._money >= _cost)
        {
            GameController._me.AddMoney(-_cost);
            Debug.Log(GameController._me._money);
            _currentTower = Instantiate(_newTower, _buildZone.transform.position, Quaternion.identity) as GameObject;
            _buildZone.SetActive(false);
        }
    }
}
