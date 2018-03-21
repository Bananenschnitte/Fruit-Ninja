using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If the Player slices the bomb, the game ends.
/// </summary>
public class Bomb : MonoBehaviour {

    private void OnTriggerEnter2D (Collider2D collision) {
        Blade b = collision.GetComponent<Blade>();
        if (!b) {
            return;
        }

        FindObjectOfType<GameManager>().OnBombHit();
    }
}
