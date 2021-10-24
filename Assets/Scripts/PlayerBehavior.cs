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
        // interacting with surroundings
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, CameraTransform.forward, out hit, 3f))
            {
                // picking up an item
                if (hit.collider.tag == "Collectible")
                {
                    if (InventoryIndex == Inventory.Length)
                    {
                        Debug.LogWarning("Inventory Error: Inventory is full!");
                    }
                    else
                    {
                        Inventory[InventoryIndex] = hit.collider.gameObject.GetComponent<Collectible>();

                        InventoryIndex++;

                        hit.collider.gameObject.SetActive(false);

                        UI.UpdateInventory(Inventory);
                    }
                }
                // interacting with an object in the scene (like a door)
                else if (hit.collider.tag == "Interactable")
                {
                    Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();

                    bool hasKeyItem = false;
                    foreach (Collectible c in Inventory)
                    {
                        if (c == interactable.GetKeyItem())
                        {
                            hasKeyItem = true;
                        }
                    }

                    if (hasKeyItem)
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }
}
