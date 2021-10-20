using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public Sprite Preview;

    // important for if the player dies, will be used to reset the level
    public Vector3 InitialPosition;
    public Quaternion InitialRotation;

    private void Start()
    {
        InitialPosition = transform.position;
        InitialRotation = transform.rotation;
    }
}
