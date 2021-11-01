using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesInRoom : MonoBehaviour
{
    public List<Notes> Spawns;
    public Notes ActiveNote;

    public void ChooseSpawn()
    {
        ActiveNote = Spawns[Random.Range(0, Spawns.Count)];
        foreach (Notes n in Spawns)
        {
            if (n != ActiveNote)
            {
                n.gameObject.SetActive(false);
            }
        }
    }

    public void Reset()
    {
        foreach (Notes n in Spawns)
        {
            n.gameObject.SetActive(true);
            n.Reset();
        }
        ChooseSpawn();
    }
}
