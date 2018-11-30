﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BarrelRotator : NetworkBehaviour {
    
    public float minAngle;
    public float maxAngle;

    // Private Variables
    public bool isCharacterFlipped = true;    // check if Character is Flipped

    // Transform of the Barrel;
    public Transform barrel;

    public int playerDirection;

    void Start()
    {
    }

	// Update is called once per frame
	void Update () {
        if (!hasAuthority)
        {
            return;
        }
        BarrelRotation(playerDirection);
    }

    void BarrelRotation(int dir)
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - barrel.transform.position;
        float angle = Mathf.Atan2(direction.y, (dir) * direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rotation.eulerAngles = new Vector3(0, 0, Mathf.Clamp(rotation.eulerAngles.z, minAngle, maxAngle));
        //Check if mouse is in the 3rd or 4th quadrant
        if (direction.y < 0)
        {
            if (direction.x > 0)// if in 4th quadrant
            {
                rotation.eulerAngles = new Vector3(0, 0, isCharacterFlipped ? maxAngle : minAngle);
            }
            else if (direction.x < 0)// if in 3rd quadrant
            {
                rotation.eulerAngles = new Vector3(0, 0, isCharacterFlipped ? minAngle : maxAngle);
            }
        }
        if(dir == -1)
        {
            rotation = Quaternion.Inverse(rotation);
        }
        barrel.transform.rotation = rotation;
    }

}
