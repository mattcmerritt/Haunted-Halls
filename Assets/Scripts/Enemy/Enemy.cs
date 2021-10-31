using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float VisionRange;
    [Range(0, 360)]
    public float FOV;

    public UI UI;

    public GameObject NavigationMap;
    public float MoveSpeed;

    public Transform PlayerTransform;
    public GameObject VisionCone;

    public float MaxDetectionTime;
    public float DetectionDuration;

    public void Awake()
    {
        UI = FindObjectOfType<UI>();
    }

    public void Update()
    { 
        // player detection
        // math taken from Sebastian Lague's Field of view visualisation (E01)
        // link: https://www.youtube.com/watch?v=rQG9aUWarwE
        Vector3 directionToPlayer = (PlayerTransform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, directionToPlayer) < FOV / 2)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToPlayer, out hit, VisionRange))
            {
                if (hit.collider.tag == "Player")
                {
                    UI.PlayerDetected();
                    DetectionDuration += Time.deltaTime;

                    if (MaxDetectionTime < DetectionDuration)
                    {
                        UI.DisplayDeathScreen();
                        DetectionDuration = 0;
                    }
                }
                else
                {
                    UI.PlayerLost();
                    DetectionDuration = 0f;
                }
            }
            else
            {
                UI.PlayerLost();
                DetectionDuration = 0f;
            }
        }
        else
        {
            UI.PlayerLost();
            DetectionDuration = 0f;
        }

        // moving between nodes
        PathingNode activeNode = NavigationMap.GetComponentInChildren<PathingNode>();

        if (activeNode != null)
        {
            // direction to move in as a normalized vector
            Vector3 direction = Vector3.Normalize(activeNode.gameObject.transform.position - transform.position);

            // move in direction
            transform.position += direction * MoveSpeed * Time.deltaTime;

            // rotate to face node
            transform.eulerAngles = new Vector3(0f, Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z), 0f);
        }
    }

    public void EnableVisionCone()
    {
        VisionCone.SetActive(true);
    }

    public void DisableVisionCone()
    {
        VisionCone.SetActive(false);
    }
}
