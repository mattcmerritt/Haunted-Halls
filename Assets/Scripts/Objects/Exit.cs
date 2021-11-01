using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour, Interactable
{
    public UI UI;

    public Collectible KeyItem;

    // important for if the player dies, will be used to reset the level
    public Vector3 InitialPosition;
    public Quaternion InitialRotation;

    private void Start()
    {
        InitialPosition = transform.position;
        InitialRotation = transform.rotation;
        UI = FindObjectOfType<UI>();
    }

    public Collectible GetKeyItem()
    {
        return KeyItem;
    }
    public void Interact()
    {
        UI.DisplayWinScreen();
    }

    public void Reset()
    {
        transform.position = InitialPosition;
        transform.rotation = InitialRotation;
    }
}
