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

    public MeshCollider ClosedCollider;
    public BoxCollider[] OpenColliders;

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

        // changing colliders
        ClosedCollider.enabled = false;
        foreach (BoxCollider box in OpenColliders)
        {
            box.enabled = true;
        }

        // changing tag to be interactable again
        gameObject.tag = "Untagged";
    }

    public void Lock()
    {
        IsOpen = false;
        SafeMesh.mesh = Closed;

        // changing colliders
        ClosedCollider.enabled = true;
        foreach (BoxCollider box in OpenColliders)
        {
            box.enabled = false;
        }

        // changing tag to be interactable again
        gameObject.tag = "Interactable";
    }

    public void Reset()
    {
        transform.position = InitialPosition;
        transform.rotation = InitialRotation;

        Lock();
    }
}
