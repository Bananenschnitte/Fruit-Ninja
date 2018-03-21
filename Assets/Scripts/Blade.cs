using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Curser aka the "Blade" to slice the fruits
/// </summary>
public class Blade : MonoBehaviour {

    /// <summary>
    /// The min velocity of the blade
    /// </summary>
    public float minVelo = 0.1f;

    private Rigidbody2D rb;
    private Vector3 lastMousePos;
    private Vector3 mouseVelo;
    private Collider2D col;

    // Use this for initialization
    void Awake() { 
	    rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update() {
        col.enabled = IsMouseMoving();
	    SetBladeToMouse();
    }

    /// <summary>
    /// Sets the Blade on the same position as the mouse/cursor
    /// </summary>
    private void SetBladeToMouse() {
	    var mousePos = Input.mousePosition;
	    mousePos.z = 10; // distance of 10 units from the camera
	    rb.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    /// <summary>
    /// Determines if the mouse/cursor/blade is moving
    /// </summary>
    /// <returns>Returns True if mouse is moving. Returns false otherwise</returns>
    private bool IsMouseMoving(){
        Vector3 curMousePos = transform.position;
        float traveled = (lastMousePos - curMousePos).magnitude;
        lastMousePos = curMousePos;

        if (traveled > minVelo) {
            return true;
        } else {
            return false;
        }
    }

}
