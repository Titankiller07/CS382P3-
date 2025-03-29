using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    [Header("UI References")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    
    private int _currentScore = 0;
    private int _highscore = 0;
    
    private const string HighscoreKey = "Highscore";
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        LoadHighscore();
    }
    
    private void LoadHighscore()
    {
        _highscore = PlayerPrefs.GetInt(HighscoreKey, 0);
        UpdateHighscoreDisplay();
    }
    
    public void AddScore(int amount)
    {
        _currentScore += amount;
        UpdateScoreDisplay();
        
        if (_currentScore > _highscore)
        {
            _highscore = _currentScore;
            PlayerPrefs.SetInt(HighscoreKey, _highscore);
            UpdateHighscoreDisplay();
        }
    }
    
    public void ResetScore()
    {
        _currentScore = 0;
        UpdateScoreDisplay();
    }
    
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {_currentScore:N0}";
    }
    
    private void UpdateHighscoreDisplay()
    {
        if (highscoreText != null)
            highscoreText.text = $"Highscore: {_highscore:N0}";
    }
}