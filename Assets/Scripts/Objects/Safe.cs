using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour, Interactable
{
    public bool IsOpen;

    // important for if the player dies, will be used to reset the level
    public Vector3 InitialPosition;
    public Quaternion InitialRotation;

    public UI UI;

    public MeshFilter SafeMesh;
    public Mesh Open, Closed;

    private void Awake()
    {
        InitialPosition = transform.position;
        InitialRotation = transform.rotation;

        UI = GameObject.FindObjectOfType<UI>();
        Closed = SafeMesh.mesh;
    }

    // no key item for safe
    public Collectible GetKeyItem()
    {
        return null;
    }

    public void Interact()
    {
        if (!IsOpen)
        {
            UI.ShowCombinationOverlay();
            // TODO: take away player movement
        }
        else
        {
            // do nothing
        }
    }

    public void Unlock()
    {
        IsOpen = true;
        SafeMesh.mesh = Open;
        UI.HideCombinationOverlay();
    }

    public void Reset()
    {
        transform.position = InitialPosition;
        transform.rotation = InitialRotation;
        IsOpen = false;
        SafeMesh.mesh = Closed;
    }
}
