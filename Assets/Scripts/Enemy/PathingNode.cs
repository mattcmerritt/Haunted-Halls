using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathingNode : MonoBehaviour
{
    public bool IsActive;
    public List<PathingNode> ConnectedNodes;

    public int EnemyLayerMask;

    private void Start()
    {
        // setting up the layer mask to only include the enemy layer
        EnemyLayerMask = 1 << 6;
    }

    private void Update()
    {
        if (IsActive)
        {
            if (Physics.CheckSphere(transform.position, 0.5f, EnemyLayerMask))
            {
                int selectedNode = Random.Range(0, ConnectedNodes.Count);
                ConnectedNodes[selectedNode].ActivateNode();
                DeactivateNode();
            }
        }
    }

    public void ActivateNode()
    {
        IsActive = true;
        gameObject.SetActive(true);
    }

    public void DeactivateNode()
    {
        IsActive = false;
        gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (IsActive)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}
