using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private TextMeshProUGUI _failsUI;
    
    public AudioClip finishSound,failSound;
    public int lives =3;
    
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
        _failsUI.text = "Lives : " + lives;
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
        lives--;
        _failsUI.text = "Lives : " + lives;
        if(failSound) _audioSource.PlayOneShot(failSound);
        _ball.BallReset();
        if (lives < 1) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
