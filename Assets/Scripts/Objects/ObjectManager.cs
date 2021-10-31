using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<SavedObject> SavedObjects;

    public void ResetAllObjects()
    {
        foreach (SavedObject saved in SavedObjects)
        {
            saved.ResetToSavedState();
        }
    }
}
