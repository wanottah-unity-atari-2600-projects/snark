
using UnityEngine;

//
// Snark [Atari 1978] v2023.04.30
//
// v2023.06.04
//

public class Player1ReloadController : MonoBehaviour
{
    // check if player 1 torpedo has hit player 1
    private void OnTriggerEnter2D(Collider2D collidingObject)
    {/*
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            return;
        }

        if (collidingObject.gameObject.CompareTag("Player 1 Torpedo"))
        {
            Player1Controller.player1.playerTorpedo.SetActive(false);

            Player1Controller.player1.playerTorpedo.transform.position = Player1Controller.player1.weaponLauncher.position;

            Player1Controller.player1.torpedoLaunched = false;
        }*/
    }

} // end of class
