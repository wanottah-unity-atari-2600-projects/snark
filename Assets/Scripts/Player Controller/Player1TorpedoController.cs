
using UnityEngine;

//
// Snark [Atari 1978] v2023.04.30
//
// v2023.06.04
//

public class Player1TorpedoController : MonoBehaviour
{
    public static Player1TorpedoController player1Torpedo;

    // reference to player's torpedo rigidbody components
    public Rigidbody2D player1TorpedoRigidbody;

    // enemy death animation
    public GameObject enemyDeathAnimation;

    private const int TORPEDO_PATTERN_START = 0;
    private const int TORPEDO_PATTERN_END = 15;

    private float player1TorpedoSpeed;

    public float xTorpedoTrajectory;
    public float yTorpedoTrajectory;

	public int player1TorpedoPattern;

    private bool adjustTorpedoTrajectory;


    private void Awake()
    {
        if (player1Torpedo != null)
        {
            Destroy(player1Torpedo);
        }

        else
        {
            player1Torpedo = this;
        }

        // get reference to player's rigidbody component
        player1TorpedoRigidbody = GetComponent<Rigidbody2D>();
    }


    public void InitialiseTorpedo(int torpedoStartTrajectory, int torpedoPattern)
    {
        player1TorpedoPattern = TORPEDO_PATTERN_START;

        xTorpedoTrajectory = 0f;
        yTorpedoTrajectory = 0f;

        player1TorpedoRigidbody.velocity = Vector2.zero;

        player1TorpedoSpeed = 10f;

        adjustTorpedoTrajectory = false;

        LoadTorpedoTubes(torpedoStartTrajectory, torpedoPattern);
    }


    private void LoadTorpedoTubes(int torpedoStartTrajectory, int torpedoPatternStart)
    {
        player1TorpedoPattern = torpedoPatternStart;

        SetInitialTorpedoTrajectory(torpedoStartTrajectory);
    }


    private void ReloadTorpedo()
    {
        Player1Controller.player1.playerTorpedo.SetActive(false);

        Player1Controller.player1.playerTorpedo.transform.position = Player1Controller.player1.weaponLauncher.position;

        Player1Controller.player1.torpedoLaunched = false;
    }


    private void SetInitialTorpedoTrajectory(int torpedoStartTrajectory)
    {
        AdjustTorpedoTrajectory(torpedoStartTrajectory);

        LaunchTorpedo();
    }


    private void LaunchTorpedo()
    {
        if (adjustTorpedoTrajectory)
        {
            AdjustTorpedoTrajectory(player1TorpedoPattern);
        }

        Vector2 newTorpedoTrajectory = new Vector2(xTorpedoTrajectory, yTorpedoTrajectory);

        newTorpedoTrajectory.Normalize();

        player1TorpedoRigidbody.velocity = newTorpedoTrajectory * player1TorpedoSpeed;
    }


    private void AdjustTorpedoTrajectory(int torpedoTrajectory)
    {
        switch (torpedoTrajectory)
        { 
            // right
            case 0:

                Right();

                break;

            // left
            case 1:

                Left();

                break;

            // up-right
            case 2:

                UpRight();

                break;

            // down
            case 3:

                Down();

                break;

            // up-left
            case 4:

                UpLeft();

                break;

            // right
            case 5:

                Right();

                break;

            // down-left
            case 6:

                DownLeft();

                break;

            // up
            case 7:

                Up();

                break;

            // down-right
            case 8:

                DownRight();

                break;

            // up-left
            case 9:

                UpLeft();

                break;

            // down
            case 10:

                Down();

                break;

            // up-right
            case 11:

                UpRight();

                break;

            // left
            case 12:

                Left();

                break;

            // down-right
            case 13:

                DownRight();

                break;

            // up
            case 14:

                Up();

                break;

            // down-left
            case 15:

                DownLeft();

                break;
        }
    }


    // torpedo trajectories
    private void Left()
    {
        xTorpedoTrajectory = -1f;
        yTorpedoTrajectory = 0f;
    }


    private void Right()
    {
        xTorpedoTrajectory = 1f;
        yTorpedoTrajectory = 0f;
    }


    private void Up()
    {
        xTorpedoTrajectory = 0f;
        yTorpedoTrajectory = 1f;
    }


    private void Down()
    {
        xTorpedoTrajectory = 0f;
        yTorpedoTrajectory = -1f;
    }


    private void UpLeft()
    {
        xTorpedoTrajectory = -1f;
        yTorpedoTrajectory = 1f;
    }


    private void UpRight()
    {
        xTorpedoTrajectory = 1f;
        yTorpedoTrajectory = 1f;
    }


    private void DownLeft()
    {
        xTorpedoTrajectory = -1f;
        yTorpedoTrajectory = -1f;
    }


    private void DownRight()
    {
        xTorpedoTrajectory = 1f;
        yTorpedoTrajectory = -1f;
    }


    private void OnTriggerEnter2D(Collider2D objectCollidedWith)
    {
        if (!adjustTorpedoTrajectory)
        {
            adjustTorpedoTrajectory = true;
        }

        // trajectory index (0 - 15)
        if (objectCollidedWith.gameObject.CompareTag("Wall"))
        {
            // change torpedo trajectory
            LaunchTorpedo();

            // increment trajectory pattern index
            player1TorpedoPattern++;

            if (player1TorpedoPattern > TORPEDO_PATTERN_END)
            {
                player1TorpedoPattern = TORPEDO_PATTERN_START;
            }
        }

        if (objectCollidedWith.gameObject.CompareTag("Player 1"))
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                return;
            }

            ReloadTorpedo();
        }


        // player 2

        // snark
        if (objectCollidedWith.gameObject.CompareTag("Snark"))
        {
            Debug.Log("hit snark");
            ReloadTorpedo();

            /*if (SnarkController.snarkController.)
            {
                SnarkController.snarkController.InitialiseSnark();
            }*/
        }
    }


} // end of class
