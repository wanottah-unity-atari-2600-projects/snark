
using System;
using UnityEngine;

//
// Snark [Atari 1978] v2023.04.30
//
// v2023.05.04
//

public class ScoreController : MonoBehaviour
{
    public static ScoreController scoreController;

    //public SpriteRenderer[] player1Score;

    //public SpriteRenderer[] player2Score;

    public SpriteRenderer[] highScore;

    public Sprite[] numberDigits;

    public const int PLAYER_1 = 1;
    public const int PLAYER_2 = 2;
    public const int HIGH_SCORE = 3;

    private const int TENS_DIGIT = 0;


    private void Awake()
    {
        scoreController = this;
    }

    
    public void InitialiseScores()
    {/*
        player1Score[TENS_DIGIT].transform.gameObject.SetActive(false);

        player2Score[TENS_DIGIT].transform.gameObject.SetActive(false);

        for (int scoreDigit = 0; scoreDigit < player1Score.Length; scoreDigit++)
        {
            player1Score[scoreDigit].sprite = numberDigits[0];

            player2Score[scoreDigit].sprite = numberDigits[0];
        }*/
    }


    public void InitialiseHighScore()
    {
        for (int scoreDigit = 0; scoreDigit < highScore.Length; scoreDigit++)
        {
            highScore[scoreDigit].sprite = numberDigits[0];
        }
    }


    public void UpdateScoreDisplay(int score, int display)
    {
        string scoreText = score.ToString();

        for (int scoreDigit = 0; scoreDigit < scoreText.Length; scoreDigit++)
        {
            string digitText = scoreText.Substring(scoreDigit, 1);

            int digit = Convert.ToInt32(digitText);

            switch (display)
            {
                case PLAYER_1:

                    UpdatePlayer1(scoreText, scoreDigit, digit);

                    break;

                case PLAYER_2:

                    UpdatePlayer2(scoreText, scoreDigit, digit);

                    break;

                case HIGH_SCORE:

                    UpdateHighScore(scoreText, scoreDigit, digit);

                    break;
            }
        }
    }


    private void UpdatePlayer1(string scoreText, int scoreDigit, int digit)
    {
        switch (scoreText.Length)
        {
            // 00
            case 2:

                //player1Score[scoreDigit].transform.gameObject.SetActive(true);

                //player1Score[scoreDigit].sprite = numberDigits[digit];

                break;

            // 0
            case 1:

                //player1Score[scoreDigit + 1].sprite = numberDigits[digit];

                break;
        }

    }


    private void UpdatePlayer2(string scoreText, int scoreDigit, int digit)
    {
        switch (scoreText.Length)
        {
            // 00
            case 2:

                //player2Score[scoreDigit].transform.gameObject.SetActive(true);

                //player2Score[scoreDigit].sprite = numberDigits[digit];

                break;

            // 0
            case 1:

                //player2Score[scoreDigit + 1].sprite = numberDigits[digit];

                break;
        }
    }


    private void UpdateHighScore(string scoreText, int scoreDigit, int digit)
    {
        switch (scoreText.Length)
        {
            // 0000000
            case 7:

                highScore[scoreDigit].sprite = numberDigits[digit];

                break;

            // 000000
            case 6:

                highScore[scoreDigit + 1].sprite = numberDigits[digit];

                break;

            // 00000
            case 5:

                highScore[scoreDigit + 2].sprite = numberDigits[digit];

                break;

            // 0000
            case 4:

                highScore[scoreDigit + 3].sprite = numberDigits[digit];

                break;

            // 000
            case 3:

                highScore[scoreDigit + 4].sprite = numberDigits[digit];

                break;

            // 00
            case 2:

                highScore[scoreDigit + 5].sprite = numberDigits[digit];

                break;

            // 0
            case 1:

                highScore[scoreDigit + 6].sprite = numberDigits[digit];

                break;
        }
    }


} // end of class
