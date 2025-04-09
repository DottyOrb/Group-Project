using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public TMP_Text scoreText;
    public int score;

    void Start()
    {
      score = 0;
        scoreText.text = "SCORE: " + score.ToString();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void AddToScore(int EnemyScore)
    {
        score += EnemyScore;
        scoreText.text = "SCORE: " + score.ToString(); // Updates the Score Text


    }

    

    void Update()
    {
        
    }
}
