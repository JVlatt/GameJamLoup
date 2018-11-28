using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _optionMenu;

    private float _speed = 1.5f;

    private Animator _myAnimator;

    private void Start()
    {
        _myAnimator = gameObject.GetComponent<Animator>();
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OptionButton()
    {
        _myAnimator.SetTrigger("option");
    }

    public void Backbutton()
    {
        _myAnimator.SetTrigger("main");
    }
}
