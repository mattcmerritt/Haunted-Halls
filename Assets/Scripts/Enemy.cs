using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float VisionRange;
    public float FOV;

    public UI UI;

    public void Awake()
    {
        UI = FindObjectOfType<UI>();
    }

    public void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, VisionRange))
        {
            if (hit.collider.tag == "Player")
            {
                UI.PlayerDetected();
            }
            else
            {
                UI.PlayerLost();
            }
        }
        else
        {
            UI.PlayerLost();
        }
    }

}
