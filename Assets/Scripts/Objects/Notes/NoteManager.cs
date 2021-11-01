using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public NotesInRoom Room1, Room2, Room3;
    public string Combination;

    public void Reset()
    {
        Room1.Reset();
        Room2.Reset();
        Room3.Reset();
        Combination = "" + Room1.ActiveNote.Value + Room2.ActiveNote.Value + Room3.ActiveNote.Value;
    }

    private void Start()
    {
        Reset();
    }
}
