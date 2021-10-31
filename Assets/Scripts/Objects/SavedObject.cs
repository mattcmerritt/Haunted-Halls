using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedObject : MonoBehaviour
{
    public Vector3 Position;
    public Quaternion Rotation;
    public bool Active;

    private void Start()
    {
        SaveState();
    }

    public void SaveState()
    {
        Position = transform.position;
        Rotation = transform.rotation;
        Active = gameObject.activeSelf;
    }

    public void ResetToSavedState()
    {
        transform.position = Position;
        transform.rotation = Rotation;
        gameObject.SetActive(Active);

        // checking for components to reset
        Door door = gameObject.GetComponent<Door>();
        if (door != null)
        {
            door.Reset();
        }

        PlayerMovement playerMovement = gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.Reset();
        }

        PlayerBehavior playerBehavior = gameObject.GetComponent<PlayerBehavior>();
        if (playerBehavior != null)
        {
            playerBehavior.Reset();
        }
    }
}
