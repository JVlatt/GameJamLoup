using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControler : MonoBehaviour {

    public AudioClip _loupDeath;
    public AudioClip _gameOver;
    public AudioClip _victory;
    public AudioClip _tire;
    public static SoundControler _soundControler;

    private AudioSource _source;

    private void Awake()
    {
        _soundControler = this;
        _source = GetComponentInParent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        _source.PlayOneShot(sound);
    }

}
