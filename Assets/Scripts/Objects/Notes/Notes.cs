using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    public int Value;
    public List<Sprite> Previews;

    public Collectible Collectible;

    public void Awake()
    {
        Collectible = GetComponent<Collectible>();
    }

    public void Reset()
    {
        Value = Random.Range(0, 10);
        Collectible.Preview = Previews[Value];
    }
}
