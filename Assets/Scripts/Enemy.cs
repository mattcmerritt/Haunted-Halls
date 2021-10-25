using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float VisionRange;
    public float FOV;

    public UI UI;

    public GameObject NavigationMap;
    public float MoveSpeed;

    public void Awake()
    {
        UI = FindObjectOfType<UI>();
    }

    public void Update()
    {
        // player detection
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
            //Debug.DrawRay(transform.position, direction, Color.magenta);
            transform.position += direction * MoveSpeed * Time.deltaTime;
            // rotate to face node
            transform.eulerAngles = new Vector3(0f, Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z), 0f);
        }
    }

}
