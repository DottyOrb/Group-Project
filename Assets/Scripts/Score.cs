using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance;
    public TMP_Text scoreText;

    int score = 0;

    void Start()
    {
        scoreText.text = "SCORE: " + score.ToString();
    }

    private void Awake()
    {
        Instance = this;
    }

    public void AddToScore(int EnemyScore)
    {
        score += EnemyScore;
        scoreText.text = "SCORE: " + score.ToString();
    }


    void Update()
    {

    }
}
