using System;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private TextMeshProUGUI _failsUI;
    
    public AudioClip finishSound,failSound;
    public int fails =0;
    
    private Ball _ball;
    private Board _player;
    private AudioSource _audioSource;

    public Action OnLevelFinished;
    
    private void Awake()
    {
        Instance = this;
        _player = FindObjectOfType<Board>();
        _ball = FindObjectOfType<Ball>();
        _ball.player = _player;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _failsUI.text = "Fails : " + fails;
        _ball.BallReset();
    }
    
    public void InvokeCheckLevelFinished(){ Invoke(nameof(CheckLevelFinished),.5f);}
    
    public void CheckLevelFinished()
    {
        if (FindObjectOfType<Brick>() != null) return;
        
        print("level finished ");
        if(finishSound) _audioSource.PlayOneShot(finishSound);
        _ball.BallReset();
        OnLevelFinished?.Invoke();
    }

    public void Fail()
    {
        fails++;
        _failsUI.text = "Fails : " + fails;
        if(failSound) _audioSource.PlayOneShot(failSound);
        _ball.BallReset();
    }
    
}
