
using UnityEngine;

//
// Snark [Atari 1978] v2023.04.30
//
// v2023.06.16
//

public class SnarkController : MonoBehaviour
{
    public static SnarkController snarkController;

    //public Transform snarkTransform;

    [HideInInspector] public Rigidbody2D snarkRigidbody;

    private const int SNARK_PATTERN_START = 0;
    private const int SNARK_PATTERN_END = 9;

    [HideInInspector] public float snarkSpeed;

    public float xSnarkTrajectory;
    public float ySnarkTrajectory;

    public int snarkTorpedoPattern;

    private bool adjustSnarkTrajectory;
    private bool snarkIsMoving;

    // ball bounce speed
    //[HideInInspector] public float snarkBounceSpeed;

    // ball speed increase
    //[HideInInspector] public float snarkSpeedIncrease;

    // maximum ball speed
    //[HideInInspector] public float maxSnarkSpeed;



    private void Awake()
    {
        if (snarkController != null)
        {
            Destroy(snarkController);
        }

        else
        {
            snarkController = this;
        }

        // get reference to player's rigidbody component
        snarkRigidbody = GetComponent<Rigidbody2D>();
    }


    public void InitialiseSnark(int snarkStartTrajectory, int snarkPattern)
    {
        snarkTorpedoPattern = SNARK_PATTERN_START;

        xSnarkTrajectory = 0f;
        ySnarkTrajectory = 0f;

        snarkRigidbody.velocity = Vector2.zero;

        snarkSpeed = 5f;

        // ball bounce speed
        //snarkBounceSpeed = 2f;

        // ball speed increase
        //snarkSpeedIncrease = 1.1f;

        // maximum ball speed
        //maxSnarkSpeed = 12f;

        //snarkTransform.gameObject.SetActive(true);

        adjustSnarkTrajectory = false;

        snarkIsMoving = false;

        LoadSnark(snarkStartTrajectory, snarkPattern);

        // randomise player serve
        //snarkRigidbody.velocity = new Vector2(RandomServe(snarkSpeed), RandomDirection(snarkSpeed));
    }




    private void LoadSnark(int snarkStartTrajectory, int snarkPatternStart)
    {
        snarkTorpedoPattern = snarkPatternStart;

        SetInitialSnarkTrajectory(snarkStartTrajectory);
    }


    private void SetInitialSnarkTrajectory(int snarkStartTrajectory)
    {
        AdjustSnarkTrajectory(snarkStartTrajectory);

        LaunchSnark();
    }


    private void LaunchSnark()
    {
        if (adjustSnarkTrajectory)
        {
            AdjustSnarkTrajectory(snarkTorpedoPattern);
        }

        Vector2 newSnarkTrajectory = new Vector2(xSnarkTrajectory, ySnarkTrajectory);

        newSnarkTrajectory.Normalize();

        snarkRigidbody.velocity = newSnarkTrajectory * snarkSpeed;

        snarkIsMoving = true;
    }


    private void AdjustSnarkTrajectory(int snarkTrajectory)
    {
        switch (snarkTrajectory)
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


    // snark trajectories
    private void Left()
    {
        xSnarkTrajectory = -1f;
        ySnarkTrajectory = 0f;
    }


    private void Right()
    {
        xSnarkTrajectory = 1f;
        ySnarkTrajectory = 0f;
    }


    private void Up()
    {
        xSnarkTrajectory = 0f;
        ySnarkTrajectory = 1f;
    }


    private void Down()
    {
        xSnarkTrajectory = 0f;
        ySnarkTrajectory = -1f;
    }


    private void UpLeft()
    {
        xSnarkTrajectory = -1f;
        ySnarkTrajectory = 1f;
    }


    private void UpRight()
    {
        xSnarkTrajectory = 1f;
        ySnarkTrajectory = 1f;
    }


    private void DownLeft()
    {
        xSnarkTrajectory = -1f;
        ySnarkTrajectory = -1f;
    }


    private void DownRight()
    {
        xSnarkTrajectory = 1f;
        ySnarkTrajectory = -1f;
    }


    private void OnTriggerEnter2D(Collider2D objectCollidedWith)
    {
        if (!adjustSnarkTrajectory)
        {
            adjustSnarkTrajectory = true;
        }

        // trajectory index (0 - 15)
        if (objectCollidedWith.gameObject.CompareTag("Wall"))
        {
            // change torpedo trajectory
            LaunchSnark();

            // increment trajectory pattern index
            snarkTorpedoPattern++;

            if (snarkTorpedoPattern > SNARK_PATTERN_END)
            {
                snarkTorpedoPattern = SNARK_PATTERN_START;
            }
        }

        // player 1

        // player 2

        // snark
    }


} // end of class
