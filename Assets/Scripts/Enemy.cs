using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float VisionRange;
    public float FOV;

    public UI UI;

    public GameObject NavigationMap;

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

        // moving between nodes
        PathingNode activeNode = NavigationMap.GetComponentInChildren<PathingNode>();
        
        if (activeNode != null)
        {
            Vector3 direction = Vector3.Normalize(activeNode.gameObject.transform.position - transform.position);
            Debug.DrawLine(transform.position, transform.position + (direction * 10f), Color.magenta);
        }
    }

}
