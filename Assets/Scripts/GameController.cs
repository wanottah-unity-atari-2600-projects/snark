
using System.Collections;
using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.SceneManagement;

//
// Snark [Atari 1978] v2023.04.30
//
// v2023.05.19
//

//
// resources
// https://www.youtube.com/watch?v=l5tMUVsUzd0&list=PLi9NLID-4erDozKV7LtSNKaKK1_Hmo01Z&index=9
// https://www.spriters-resource.com/arcade/berzerk/sheet/125848/
// https://answers.unity.com/questions/534314/how-to-delete-instantiated-gameobject.html
//

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    //public Transform highScoreTable;
    //public Transform coinSlot;

    //public Transform arenaPanel;

    //public Transform gameOverText;
    //public Transform insertCoinsText;
    //public Transform copyrightText;
    //public Transform pressStartText;

    //public Transform mazeGenerator;

    public Transform player1Controller;
    //public Transform player1ScoreController;
    //public Transform player1LivesController;
    //public Transform player1BonusScoreController;

    //public Transform robotController;

    public Sprite[] number;

    public const int PLAYER_ONE = 1;
    public const int PLAYER_TWO = 2;

    //public const int START_LIVES = 3;
    //private const int EXTRA_LIFE_SCORE = 5000;
    //private const int EXTRA_LIFE = 0;
    public const int START_SCORE = 0;
    public const int GAME_OVER = 0;

    //public const int INSERT_COINS = 0;
    //public const int ONE_PLAYER_COINS = 1;
    //public const int MAXIMUM_COINS = 99;


    // console initialisation
    private const string GAME_TITLE = "SNARK";
    //private const int TV_MODE = AtariConsoleController.COLOUR_TV;


    public int player1Score;
    public int player1Lives;

    public int player2Score;

    public int highScore;

    public int robotsDestroyed;


    // game credits
    public int gameCredits;
    public bool coinsInserted;


    // game mode
    public bool canPlay;
    public bool inPlayMode;
    public bool inAttractMode;
    public bool inPawzMode;
    public bool gameOver;
    public bool twoPlayer;
    public bool playerRespawning;





    private void Awake()
    {
        if (gameController != null)
        {
            Destroy(gameController);
        }

        else
        {
            gameController = this;
        }
    }


    private void Start()
    {
        CabinetStartUp();
    }


    // =============================================================================
    // check for player input
    // =============================================================================
    private void Update()
    {
        GameLoop();
    }


    private void GameLoop()
    {
        KeyboardInput();

        ControllerInput();

        //MoveRobots();
    }


    private void MoveRobots()
    {
        //if (!gameOver && robotController.gameObject.activeInHierarchy)
        //{
            //RobotController.robotController.MoveRobots();
        //}
    }


    private void CabinetStartUp()
    {
        InitialiseCabinet();

        StartAttractMode();
    }


    private void InitialiseCabinet()
    {
        //gameCredits = INSERT_COINS;

        canPlay = false;

        //gameOver = true;
    }


    public void StartAttractMode()
    {
        //gameOverText.gameObject.SetActive(true);
        //StartCoroutine(CycleText());

        // start attract mode
        inAttractMode = true;

        inPlayMode = false;

        // hide game arena
        //arenaPanel.gameObject.SetActive(true);
    }


    private void KeyboardInput()
    {
        if (!inAttractMode)
        {
            return;
        }

        // insert coins
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (gameCredits > 99)
            {
                return;
            }

            else
            {
                gameCredits += 1;

                //CreditsController.creditsController.UpdateGameCredits();

                if (!coinsInserted)
                {
                    AudioController.audioController.PlayAudioClip("Coin Inserted");

                    coinsInserted = true;
                }

                //insertCoinsText.gameObject.SetActive(false);
            }
        }

        // start game
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //if (gameCredits > 0)
            //{
               //StopCoroutine(CycleText());

                //gameCredits -= 1;

                //CreditsController.creditsController.UpdateGameCredits();

                //StartCoroutine(StartDelay());

            StartOnePlayerGame();
            //}
        }
    }


    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.5f);

        //coinSlot.gameObject.SetActive(false);

        //highScoreTable.gameObject.SetActive(false);

        // start game
        StartOnePlayerGame();
    }


    public void StartOnePlayerGame()
    {
        //StartCoroutine(InitialiseGame());
    //}


    //IEnumerator InitialiseGame()
    //{
        //yield return new WaitForSeconds(2f);

        //arenaPanel.gameObject.SetActive(false);

        //mazeGenerator.gameObject.SetActive(true);

        //SecurityDoorController.doorController.DeactivateSecurityDoors();

        player1Score = START_SCORE;

        //player1Lives = START_LIVES;

        //yield return new WaitForSeconds(1f);

        //player1Controller.gameObject.SetActive(true);

        //player1ScoreController.gameObject.SetActive(true);

        //Player1ScoreController.scoreController.InitialiseScores();
        ScoreController.scoreController.InitialiseScores();

        //Player1ScoreController.scoreController.UpdateScoreDisplay(player1Score, PLAYER_ONE);
        ScoreController.scoreController.UpdateScoreDisplay(player1Score, PLAYER_ONE);

        //player1LivesController.gameObject.SetActive(true);

        //Player1LivesController.livesController.UpdateLives(player1Lives);

        //player1BonusScoreController.gameObject.SetActive(true);

        //BonusController.bonusController.InitialiseBonusScore();

        Player1Controller.player1.Initialise();

        //yield return new WaitForSeconds(3f);

        //robotController.gameObject.SetActive(true);
        
        //RobotController.robotController.SpawnRobots();

        robotsDestroyed = 0;

        inAttractMode = false;

        gameOver = false;
    }


    private void ControllerInput()
    {
        if (!gameOver && !inPawzMode && !inAttractMode)
        {
            Player1Controller.player1.CheckPlayerInput();
        }
    }


    public void PlayerDied()
    {
        player1Lives -= 1;

        //Player1LivesController.livesController.UpdateLives(player1Lives);

        if (player1Lives == GAME_OVER)
        {
            GameOver();

            return;
        }

        StartCoroutine(RespawnPlayer());
    }


    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(2f);

        //RobotController.robotController.DeactivateAllRobots();

        yield return new WaitForSeconds(0.5f);

        //arenaPanel.gameObject.SetActive(true);

        //SecurityDoorController.doorController.DeactivateSecurityDoors();

        //BerzerkMazeGenerator.mazeGenerator.GenerateNewRoom();

        yield return new WaitForSeconds(1f);

        //arenaPanel.gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        playerRespawning = true;

        //if (!Player1Controller.player1.playerIsFacingRight)
        //{
            //Player1Controller.player1.FlipPlayerSprite();
        //}

        Player1Controller.player1.Initialise();

        //Player1LivesController.livesController.UpdateLives(player1Lives);

        yield return new WaitForSeconds(3f);

        //RobotController.robotController.SpawnRobots();

        robotsDestroyed = 0;
    }


    public void UpdatePlayerScore(int points)
    {
        // Update score
        player1Score += points;

        robotsDestroyed += 1;

        //if (robotsDestroyed == RobotController.robotController.numberOfRobotsToSpawn)
        //{
            //BonusController.bonusController.AddBonusPoints();
        //}

        //Player1ScoreController.scoreController.UpdateScoreDisplay(player1Score, PLAYER_ONE);
    }


    public void GameOver()
    {
        gameOver = true;

        StartCoroutine(GameOverDelay());
    }


    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(1f);

        //robotController.gameObject.SetActive(false);

        //mazeGenerator.gameObject.SetActive(false);

        player1Controller.gameObject.SetActive(false);

        //player1ScoreController.gameObject.SetActive(false);

        //player1LivesController.gameObject.SetActive(false);

        StartAttractMode();
    }


    // returns the opposite exit from which the player left the room
    public int GetOppositeExit(int exit)
    {
        int oppositeExit = -1;

        switch (exit)
        {
            //case RoomController.NORTH_EXIT: oppositeExit = RoomController.SOUTH_EXIT; break;

            //case RoomController.EAST_EXIT: oppositeExit = RoomController.WEST_EXIT; break;

            //case RoomController.SOUTH_EXIT: oppositeExit = RoomController.NORTH_EXIT; break;

            //case RoomController.WEST_EXIT: oppositeExit = RoomController.EAST_EXIT; break;
        }

        return oppositeExit;
    }


} // end of class
