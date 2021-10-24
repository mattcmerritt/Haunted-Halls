using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public Collectible KeyItem;

    // important for if the player dies, will be used to reset the level
    public Vector3 InitialPosition;
    public Quaternion InitialRotation;

    private void Start()
    {
        InitialPosition = transform.position;
        InitialRotation = transform.rotation;
    }

    public Collectible GetKeyItem()
    {
        return KeyItem;
    }

    public void Interact()
    {
        transform.Rotate(new Vector3(0, 90, 0));
    }

    public void Reset()
    {
        transform.position = InitialPosition;
        transform.rotation = InitialRotation;
    }
}
