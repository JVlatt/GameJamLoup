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
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._click);
        StartCoroutine("Wait");
        
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void OptionButton()
    {
        _myAnimator.SetTrigger("option");
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._transi1);
    }

    public void Backbutton()
    {
        _myAnimator.SetTrigger("main");
        SoundControler._soundControler.PlaySound(SoundControler._soundControler._transi2);
    }
    IEnumerator Wait()
    {      
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
