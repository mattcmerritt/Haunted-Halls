using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Transform CameraTransform;
    public UI UI;

    public Collectible[] Inventory;
    public int InventoryIndex; // this will need to be the slot that items should be added at

    private void Start()
    {
        Inventory = new Collectible[5];
        UI = FindObjectOfType<UI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, CameraTransform.forward, out hit))
            {
                if (hit.collider.tag == "Collectible")
                {
                    Inventory[InventoryIndex] = hit.collider.gameObject.GetComponent<Collectible>();

                    hit.collider.gameObject.SetActive(false);

                    UI.UpdateInventory(Inventory);
                }
            }
        }
    }
}
