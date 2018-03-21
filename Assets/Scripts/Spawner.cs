using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawn contiounisly Fruits and "shoots" them into the screen
/// </summary>
public class Spawner : MonoBehaviour {

    /// <summary>
    /// The Objects to spawn
    /// </summary>
    public GameObject[] objsToSpawn;

    /// <summary>
    /// The Bomb that ends the game on hit
    /// </summary>
    public GameObject bomb;

    /// <summary>
    /// Spawnpoints to spawn and shoot the fruits from
    /// </summary>
    public Transform[] spawnPlaces;

    /// <summary>
    /// The minimum delay/wait till the next shot
    /// </summary>
    public float minWait = .3f;

    /// <summary>
    /// The Maximim delay/wait till the next shot
    /// </summary>
    public float maxWait = 1f;

    /// <summary>
    /// The minimum Force to shoot the fruits with
    /// </summary>
    public float minForce = 12;

    /// <summary>
    /// The maximum Force to shoot the fruits with
    /// </summary>
    public float maxForce = 17;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnFruits());
	}
	
    /// <summary>
    /// Spawn and shoots the fruits
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnFruits () {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));

            Transform t = spawnPlaces[Random.Range(0, spawnPlaces.Length)];

            GameObject go = null;
            float p = Random.Range(0, 100);

            if (p < 10) {
                go = bomb;
            } else{
                go = objsToSpawn[Random.Range(0, objsToSpawn.Length)];
            }

            GameObject fruit = Instantiate(go, t.position, t.rotation);
            fruit.GetComponent<Rigidbody2D>().AddForce(t.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);

            Debug.Log("Fruit gets spawned");

            Destroy(fruit, 5);
        }


    }


}
