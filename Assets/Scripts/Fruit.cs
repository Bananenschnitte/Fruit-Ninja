using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Fruits to slice.
/// Slicing increments the score of the player
/// </summary>
public class Fruit : MonoBehaviour {

    /// <summary>
    /// The Sliced Variation of the fruit
    /// </summary>
    public GameObject slicedFruitPrefab;

    /// <summary>
    /// The minimum Force of the Explision, when slicing a fruit and pushing the sliced parts away
    /// </summary>
    public float ExplosionForce_min = 500f;

    /// <summary>
    /// The maxim Force of the Exploision, when slicing a fruit and pushing the sliced parts away
    /// </summary>
    public float ExplosionForce_max = 1000f;

    /// <summary>
    /// The Radius of the Explosion when slicing a fruit and puhsing the sliced parts away
    /// </summary>
    public float ExplosionRadius = 5f;

    /// <summary>
    /// Creates the sliced-Variation of the fruit
    /// </summary>
    public void CreateSlicedFruit () {

        //  Create the Slieced-Fruit Variation
        GameObject inst = (GameObject) Instantiate(slicedFruitPrefab, transform.position, transform.rotation);
        Rigidbody[] rbsOnSliced = inst.transform.GetComponentsInChildren<Rigidbody>();
        
        foreach(Rigidbody r in rbsOnSliced){
            r.transform.rotation = Random.rotation;            
            r.AddExplosionForce(Random.Range(ExplosionForce_min, ExplosionForce_max), transform.position, ExplosionRadius);
        }

        //  Play Slice Sound
        FindObjectOfType<GameManager>().PlayRandomSliceSound();

        //  Increase the Players score
        FindObjectOfType<GameManager>().IncreaseScore(3);

        //  Destory gameobject
        Destroy(inst.gameObject, 5);
        Destroy(gameObject);
    }

    /// <summary>
    /// Collision detection. Triggers when the player hits the fruit.
    /// Creates on the same position the sliced Fruit-variation of ot
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D (Collider2D collision) {
        Blade b = collision.GetComponent<Blade>();

        if(!b){
            return;
        }

        CreateSlicedFruit();
    }


}
