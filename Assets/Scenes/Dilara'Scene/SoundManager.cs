using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance; // SoundManager örneği


    private float timer = 0f;
    private float interval = 19f;
    public AudioSource audioSource;
    public AudioSource mainMusic;
    public AudioSource atmosphereSoundEffects;
    public AudioSource atmosphereSoundEffects2;
    public AudioSource atmosphereSoundEffects3;
    public AudioSource atmosphereSoundEffects4;
    public AudioSource characterSoundEffects;
    public AudioSource characterSoundEffects2;

    //PlayAtmosphereSoundEffects sesleri
    [SerializeField] private AudioClip windForestTree; // orman,rüzgarı ve ağaçlar
    [SerializeField] private AudioClip forestWindBird; // orman,rüzgarı ve kuş sesleri
    [SerializeField] private AudioClip seaSandWave; // deniz,dalga,plaj sesi
    [SerializeField] private AudioClip mountainCricet; // dağ ve cırcır böceği sesi


    //MainCharacter Sesleri
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip run;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.8f; // AudioSource volume ayarı
        audioSource.pitch = 1.0f; // AudioSource pitch ayarı
        audioSource.spatialBlend = 1.0f; // AudioSource spatialBlend ayarı
    }

    private void PlayAtmosphereSoundEffects()    //PlayAtmosphereSoundEffects seslerini oynatır
    {
        atmosphereSoundEffects.PlayOneShot(windForestTree); // windForestTree sesini oynatır
        atmosphereSoundEffects2.PlayOneShot(forestWindBird); // ForestWindBird sesini oynatır
        atmosphereSoundEffects3.PlayOneShot(seaSandWave); // seaSandWave sesini oynatır
        atmosphereSoundEffects3.PlayOneShot(mountainCricet); // dağ ve cırcır böceği sesini oynatır

    }

    private void PlayCharacterSoundEffects()    //CharacterSoundEffects seslerini oynatır
    {
        characterSoundEffects.PlayOneShot(walk); // walk sesini oynatır
        characterSoundEffects2.PlayOneShot(run); // run sesini oynatır
    }

    //MainCharacter Seslerini oynatır
    public void Update()
    {
        timer += Time.deltaTime;


        if (Input.GetKey(KeyCode.W)&&timer >= interval)
        {
            characterSoundEffects.PlayOneShot(walk);
            timer = 0f;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterSoundEffects2.PlayOneShot(run);
        }
    }}