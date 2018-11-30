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
        if(Input.GetMouseButtonDown(0))
        {
            if(GameController._myGC == null)
            {
                _currentTower = Instantiate(_newTower, _buildZone.transform.position, Quaternion.identity) as GameObject;
                GameObject particles = Instantiate(Resources.Load("build", typeof(GameObject)), _buildZone.transform.position, Quaternion.identity, _currentTower.transform) as GameObject;
                Destroy(particles, 20);
                SoundControler._soundControler.PlaySound(SoundControler._soundControler._jobdone);
                _buildZone.SetActive(false);
            }
            else if(GameController._myGC._money >= _cost)
            { 
                GameController._myGC.AddMoney(-_cost);            
                _currentTower = Instantiate(_newTower, _buildZone.transform.position, Quaternion.identity) as GameObject;
                GameObject particles = Instantiate(Resources.Load("build", typeof(GameObject)), _buildZone.transform.position, Quaternion.identity, _currentTower.transform) as GameObject;
                Destroy(particles, 20);
                SoundControler._soundControler.PlaySound(SoundControler._soundControler._jobdone);
                _buildZone.SetActive(false);
            }
        }
       
    }
}
