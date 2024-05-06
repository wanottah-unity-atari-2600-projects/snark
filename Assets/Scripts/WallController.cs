
using UnityEngine;

//
// Pong [Atari 1972] v2019.02.24
//
// v2021.12.26
//

public class WallController : MonoBehaviour
{
    // if the ball bounces off the wall
    private void OnCollisionEnter2D(Collision2D collidingObject)
    {
        if (collidingObject.transform.CompareTag("Ball") && !GameController.gameController.inAttractMode)
        {
            // play a sound
            AudioController.audioController.PlayAudioClip("Wall Bounce");
        }
    }


} // end of class
