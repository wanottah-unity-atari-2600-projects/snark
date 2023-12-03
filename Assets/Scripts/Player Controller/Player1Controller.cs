
using System.Collections;
using UnityEngine;

//
// Snark [Atari 1978] v2023.04.30
//
// v2023.06.16
//

//
// references
// https://answers.unity.com/questions/534314/how-to-delete-instantiated-gameobject.html
//

public class Player1Controller : MonoBehaviour
{
    public static Player1Controller player1;

    // reference to the player's 'Rigidbody' component
    private Rigidbody2D playerRigidbody;

    private BoxCollider2D playerCollider;

    // reference to the player's 'Animator' component
    public Animator playerAnimator;

    // player death animation
    //public GameObject playerDeathAnimation;

    //private GameObject playerTorpedo;
    public GameObject playerTorpedo;

    public Transform weaponHolder;
    public Transform weaponLauncher;
    //public Transform reloadSensor;

    public SpriteRenderer playerSpriteRenderer;

    // player spawn points
    public Transform[] playerSpawnPoint;

    private const float PLAYER_MOVE_SPEED = 3.5f; //2.5f;
    //private const float PLAYER_FIRE_RATE = 0.4f;

    // player animation states
    private const int ANIMATION_PLAYER_IDLE = 0;
    private const int ANIMATION_MOVE_PLAYER = 1;
    private const int ANIMATION_FIRE_UP = 1;
    private const int ANIMATION_FIRE_UP_RIGHT = 2;
    private const int ANIMATION_FIRE_RIGHT = 3;
    private const int ANIMATION_FIRE_DOWN_RIGHT = 4;
    private const int ANIMATION_FIRE_DOWN = 5;
    private const int ANIMATION_FIRE_DOWN_LEFT = 6;
    private const int ANIMATION_FIRE_LEFT = 7;
    private const int ANIMATION_FIRE_UP_LEFT = 8;

    private const int RIGHT = 0;
    private const int UP_RIGHT = 2;
    private const int UP = 7;
    private const int UP_LEFT = 4;
    private const int LEFT = 1;
    private const int DOWN_LEFT = 6;
    private const int DOWN = 3;
    private const int DOWN_RIGHT = 8;



    // weapon launcher positions
    //private const float LAUNCHER_OFFSET_X = 0.7f;
    //private const float LAUNCHER_OFFSET_Y = 1f;

    //private const int NORTH_SECTOR = 2;
    //private const int SOUTH_SECTOR = 12;
    //private const int EAST_SECTOR = 9;
    //private const int WEST_SECTOR = 5;

    // player start position
    //private float player1PositionX;
	//private float player1PositionY;

	private Vector2 player1StartPosition;

    //public int playerSector;

    //public int player1Lives;
    private int patternAlpha;
    private int patternDelta;

	private float playerSpeed;
    private int playerFireDirection;

    public float horizontalDirection;
    public float verticalDirection;
    public float controllerDirectionX;
    public float controllerDirectionY;

    // shoot delay
    //private float fireRate;
    //private float shootDelay;

    private bool playerIsMoving;
    //public bool playerIsFacingRight;
    public bool weaponIsLoaded;
    public bool torpedoLaunched;
    //public bool launchingTorpedo;
    public bool playerIsDead;

    //public bool playerHasLeftRoom;
    //public int exit;

    public bool inPlay;




    private void Awake()
	{
        if (player1 != null)
        {
            Destroy(player1);
        }

        else
        {
            player1 = this;
        }

        // get reference to player's rigidbody component
        playerRigidbody = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<BoxCollider2D>();
	}


    // called from game controller
    public void Initialise()
    {
        playerSpeed = PLAYER_MOVE_SPEED;

        //playerFireDirection = ANIMATION_PLAYER_IDLE;

        //fireRate = PLAYER_FIRE_RATE;

        // reset player 1 start position
        //GameController.gameController.playerRespawning = true;

        //EnterNewRoom(RoomController.WEST_EXIT);

        //player1Lives = GameController.START_LIVES;
        
        //playerIsFacingRight = true;

        playerIsMoving = false;

        weaponIsLoaded = false;

        //launchingTorpedo = false;

        torpedoLaunched = false;

        //playerIsDead = false;

        //playerHasLeftRoom = false;

        //exit = -1;

        inPlay = false;
    }


    private void PositionPlayer(Vector2 position)
    {
        player1StartPosition = new Vector2(position.x, position.y);

        transform.position = player1StartPosition;

        StartCoroutine(PlayerStart());
    }


    IEnumerator PlayerStart()
    {
        playerSpriteRenderer.enabled = true;

        playerAnimator.SetBool("playerStart", true);

        yield return new WaitForSeconds(2.5f);

        playerAnimator.SetBool("playerStart", false);

        inPlay = true;
    }


    private void PositionLauncher(float launcherOffsetX, float launcherOffsetY, float launcherRotation)
    {
        // set launcher direction
        weaponLauncher.position = 
            
            new Vector3(
                transform.position.x + launcherOffsetX, 
                transform.position.y + launcherOffsetY, 
                0f);

        weaponLauncher.eulerAngles = new Vector3(0f, 0f, launcherRotation);
    }


    public void SetTorpedoTrajectoryAngle()
    {
        playerTorpedo.transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }


    public void CheckPlayerInput()
    {
        //if (playerIsDead)
        //{
            //return;
        //}

        PlayerWeaponController();

        PlayerControllerInput();

        PlayerFireInput();

        //PlayerMoveAnimation();
    }


    private void PlayerWeaponController()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {         
            //reloadSensor.gameObject.SetActive(false);

            // lower shields
            playerCollider.enabled = false;

            // load torpedo
            // if torpedo has not been launched
            if (!torpedoLaunched)
            {
                // arm torpedo
                playerTorpedo.SetActive(true);

                weaponIsLoaded = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {           
            //reloadSensor.gameObject.SetActive(true);

            // raise shields
            playerCollider.enabled = true;

            weaponIsLoaded = false;

            // if torpedo has not been launched
            if (!torpedoLaunched)
            {
                // disarm torpedo
                playerTorpedo.SetActive(false);
            }
        }
    }


    private void StopPlayerMoving()
    {
        horizontalDirection = 0f;
        verticalDirection = 0f;

        playerRigidbody.velocity = Vector2.zero;
    }


    // player 1
    private void PlayerControllerInput()
    {
        playerIsMoving = false;

        // if player is not moving
        if (!playerIsMoving) // || playerIsDead)
        {
            StopPlayerMoving();
        }

        controllerDirectionX = Input.GetAxisRaw("Horizontal");
        controllerDirectionY = Input.GetAxisRaw("Vertical");


        // up
        if (controllerDirectionX == 0f && controllerDirectionY > 0f)
        {
            if (weaponIsLoaded)
            {
                return;
            }

            horizontalDirection = 0f;
            verticalDirection = playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }


        // down
        if (controllerDirectionX == 0f && controllerDirectionY < 0f)
        {
            if (weaponIsLoaded)
            {
                return;
            }

            horizontalDirection = 0f;
            verticalDirection = -playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }


        // left
        if (controllerDirectionX < 0f && controllerDirectionY == 0f)
        {
            if (weaponIsLoaded)
            {
                return;
            }

            horizontalDirection = -playerSpeed;
            verticalDirection = 0f;

            playerIsMoving = true;

            MovePlayer();
        }


        // right
        if (controllerDirectionX > 0 && controllerDirectionY == 0f)
        {
            if (weaponIsLoaded)
            {
                return;
            }

            horizontalDirection = playerSpeed;
            verticalDirection = 0f;

            playerIsMoving = true;

            MovePlayer();
        }


        // up - left
        if (controllerDirectionY > 0f && controllerDirectionX < 0f)
        {
            if (weaponIsLoaded)
            {
                return;
            }

            horizontalDirection = -playerSpeed;
            verticalDirection = playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }


        // up - right
        if (controllerDirectionY > 0f && controllerDirectionX > 0f)
        {
            if (weaponIsLoaded)
            {
                return;
            }

            horizontalDirection = playerSpeed;
            verticalDirection = playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }


        // down - left
        if (controllerDirectionY < 0f && controllerDirectionX < 0f)
        {
            if (weaponIsLoaded)
            {
                return;
            }

            horizontalDirection = -playerSpeed;
            verticalDirection = -playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }


        // down - right
        if (controllerDirectionY < 0f && controllerDirectionX > 0f)
        {
            if (weaponIsLoaded)
            {
                return;
            }

            horizontalDirection = playerSpeed;
            verticalDirection = -playerSpeed;

            playerIsMoving = true;

            MovePlayer();
        }
    }


    private void PlayerFireInput()
    {
        if (!weaponIsLoaded)
        {
            return;
        }

        // up
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            // set launcher direction
            PositionLauncher(0f, 0f, 90f);

            patternAlpha = 8;
            patternDelta = -1;

            FirePlayer1Torpedo(UP);
        }


        // down
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            // set launcher direction
            PositionLauncher(0f, 0f, 270f);

            patternAlpha = 4;
            patternDelta = -1;

            FirePlayer1Torpedo(DOWN);
        }


        // left
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            // set launcher direction
            PositionLauncher(0f, 0f, 180f);

            patternAlpha = 13;
            patternDelta = 1;

            FirePlayer1Torpedo(LEFT);
        }


        // right
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            // set launcher trajectory
            PositionLauncher(0f, 0f, 0f);

            patternAlpha = 1;
            patternDelta = 6;

            FirePlayer1Torpedo(RIGHT);
        }


        // up - left
        if (Input.GetKeyUp(KeyCode.UpArrow) && Input.GetKeyUp(KeyCode.LeftArrow))
        {
            // set launcher direction
            PositionLauncher(0f, 0f, 135f);

            patternAlpha = 13;
            patternDelta = -1;

            SetTorpedoTrajectoryAngle();

            FirePlayer1Torpedo(UP_LEFT);
        }


        // up - right
        if (Input.GetKeyUp(KeyCode.UpArrow) && Input.GetKeyUp(KeyCode.RightArrow))
        {
            // set launcher direction
            PositionLauncher(0f, 0f, 45f);

            patternAlpha = 1;
            patternDelta = 6;

            SetTorpedoTrajectoryAngle();

            FirePlayer1Torpedo(UP_RIGHT);
        }


        // down - left
        if (Input.GetKeyUp(KeyCode.DownArrow) && Input.GetKeyUp(KeyCode.LeftArrow))
        {
            // set launcher direction
            PositionLauncher(0f, 0f, 225f);

            patternAlpha = 0; // 7;
            patternDelta = -1;

            SetTorpedoTrajectoryAngle();

            FirePlayer1Torpedo(DOWN_LEFT);
        }


        // down - right
        if (Input.GetKeyUp(KeyCode.DownArrow) && Input.GetKeyUp(KeyCode.RightArrow))
        {
            // set launcher direction
            PositionLauncher(0f, 0f, 315f);

            patternAlpha = 9;
            patternDelta = -1;

            SetTorpedoTrajectoryAngle();

            FirePlayer1Torpedo(DOWN_RIGHT);
        }
    }


    /*private void PlayerFireAnimation(int fireDirection)
    {
        SetPlayerSpriteDirection();

        //playerAnimator.SetInteger("fireDirection", fireDirection);
    }*/


    private void FirePlayer1Torpedo(int torpedoStartTrajectory)
    {
        int torpedoPattern;

        torpedoPattern = patternAlpha;

        torpedoLaunched = true;

        weaponIsLoaded = false;

        Player1TorpedoController.player1Torpedo.InitialiseTorpedo(torpedoStartTrajectory, torpedoPattern);

        //AudioController.audioController.PlayAudioClip("Player Shoot");
    }

    /*
    private void PlayerMoveAnimation()
    {
        SetPlayerSpriteDirection();

        if (playerIsMoving)
        {
            //playerAnimator.SetInteger("playerAnimation", ANIMATION_MOVE_PLAYER);
        }

        else
        {
            //playerAnimator.SetInteger("playerAnimation", ANIMATION_PLAYER_IDLE);
        }
    }

    
    private void SetPlayerSpriteDirection()
    {
        if (horizontalDirection > 0 && !playerIsFacingRight)
        {
            FlipPlayerSprite();
        }

        else if (horizontalDirection < 0 && playerIsFacingRight)
        {
            FlipPlayerSprite();
        }
	}


	public void FlipPlayerSprite()
    {
		playerIsFacingRight = !playerIsFacingRight;

		Vector3 playerTransform = transform.localScale;
 
		playerTransform.x *= -1;

		transform.localScale = playerTransform;
	}*/


    private void MovePlayer()
    {
        if (playerIsMoving)
        {
            playerRigidbody.velocity = new Vector2(horizontalDirection, verticalDirection);
        }
    }


    void OnTriggerEnter2D(Collider2D objectCollidedWith)
    {
        //if (playerIsDead)
        //{
            //return;
        //}

        //if (other.CompareTag("Enemy") || other.CompareTag("Wall") || other.CompareTag("Enemy Bullet"))
        if (objectCollidedWith.CompareTag("Wall"))
        {
            //playerIsDead = true;

            //weaponIsLoaded = false;

            // stop player moving
            //playerRigidbody.velocity = Vector2.zero;

            //playerIsMoving = false;

            //playerAnimator.SetInteger("playerAnimation", ANIMATION_PLAYER_IDLE);

            // disable the player game object sprite renderer
            //playerSpriteRenderer.enabled = false;

            //AudioController.audioController.PlayAudioClip("Player Died");

            //StartCoroutine(PlayerDied());
        }
    }


	IEnumerator PlayerDied()
    {       
        // create a clone of the player death animation
        //GameObject killBill = Instantiate(playerDeathAnimation, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(1.5f);

        //Destroy(killBill);

        //GameController.gameController.PlayerDied();
	}


} // end of class
