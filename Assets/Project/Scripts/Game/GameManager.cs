using TMPro;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public AudioClip finishSound,failSound;
    public int fails =0;
    
    private Ball _ball;
    private Board _player;
    private TextMeshProUGUI _failsUI;
    private AudioSource _audioSource;
    
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
    }
    
    public void CheckLevelFinished()
    {
        if (FindObjectOfType<Brick>() != null) return;
        
        print("level finished ");
        _audioSource.PlayOneShot(finishSound);
        _ball.BallReset();
    }

    public void Fail()
    {
        fails++;
        _failsUI.text = "Fails : " + fails;
        _audioSource.PlayOneShot(failSound);
    }
    
}
