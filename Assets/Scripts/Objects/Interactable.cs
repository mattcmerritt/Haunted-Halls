using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    // gets the key or object that the player needs in order to use this object, null if none needed
    public Collectible GetKeyItem();

    // changes the objects state
    public void Interact();

    // resets the object back to an initial position
    public void Reset();


}
