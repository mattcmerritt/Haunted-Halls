using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public Transform CameraTransform;
    public UI UI;

    public Collectible[] Inventory;
    public int InventoryIndex; // this will need to be the slot that items should be added at

    public Enemy Enemy;
    public float RemainingBattery;
    public float BatteryUseRate;

    [Range(1, 20)]
    public float GrabDistance;

    public bool HasGoggles;
    public HelpOverlay HelpOverlay;

    private void Start()
    {
        Inventory = new Collectible[6];
        UI = FindObjectOfType<UI>();
        Enemy = FindObjectOfType<Enemy>();
    }

    private void Update()
    {
        // interacting with surroundings
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, CameraTransform.forward, out hit, GrabDistance))
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
                        HelpOverlay.ToggleInventoryPrompt();

                        if (hit.collider.name.Contains("goggles"))
                        {
                            HasGoggles = true;
                            HelpOverlay.ToggleGogglesPrompt();
                        }

                        UI.UpdateInventory(Inventory);
                    }
                }
                // interacting with an object in the scene (like a door)
                else if (hit.collider.tag == "Interactable")
                {
                    Interactable interactable = hit.collider.gameObject.GetComponentInParent<Interactable>();

                    bool hasKeyItem = false;
                    foreach (Collectible c in Inventory)
                    {
                        if (c == interactable.GetKeyItem())
                        {
                            hasKeyItem = true;
                        }
                    }

                    // if null, no key needed
                    if (interactable.GetKeyItem() == null)
                    {
                        hasKeyItem = true;
                    }

                    if (hasKeyItem)
                    {
                        interactable.Interact();
                    }
                }
            }
        }

        // ghost vision goggles
        if (Input.GetKey(KeyCode.Q) && HasGoggles)
        {
            UI.DisplayGoggles();
            // if battery left, turn on the light
            if (RemainingBattery > 0)
            {
                Enemy.EnableVisionCone();
                RemainingBattery -= BatteryUseRate * Time.deltaTime;

                UI.UpdateBatteryLevel(RemainingBattery);
            }
            else
            {
                // checking if a battery is in the inventory
                int batteryIndex = -1;
                for (int i = 0; i < Inventory.Length; i++)
                {
                    if (Inventory[i] != null && Inventory[i].name.Contains("Battery"))
                    {
                        batteryIndex = i;
                    }
                }

                // refilling remaining battery and turning on light
                if (batteryIndex != -1)
                {
                    RemainingBattery = 100f;

                    // removing battery and shifting inventory
                    for (int i = batteryIndex; i < Inventory.Length - 1; i++)
                    {
                        Inventory[i] = Inventory[i + 1];
                    }
                    Inventory[Inventory.Length - 1] = null;
                    InventoryIndex--;

                    UI.UpdateInventory(Inventory);

                    Enemy.EnableVisionCone();
                    RemainingBattery -= BatteryUseRate * Time.deltaTime;

                    UI.UpdateBatteryLevel(RemainingBattery);
                }
                // light stays off
                else
                {
                    Enemy.DisableVisionCone();
                    UI.UpdateBatteryLevel(RemainingBattery);
                }
            }
        }
        else
        {
            Enemy.DisableVisionCone();
            UI.UpdateBatteryLevel(RemainingBattery);
            UI.HideGoggles();
        }

        // checking if the hints for interact should be displayed
        RaycastHit itemHit;
        if (Physics.Raycast(transform.position, CameraTransform.forward, out itemHit, GrabDistance))
        {
            if (itemHit.collider.tag == "Collectible")
            {
                HelpOverlay.ShowInteractPrompt("Collect");
            }
            else if (itemHit.collider.tag == "Interactable")
            {
                HelpOverlay.ShowInteractPrompt("Interact");
            }
            else
            {
                HelpOverlay.HideInteractPrompt();
            }
        }
        else
        {
            HelpOverlay.HideInteractPrompt();
        }
    }

    public void Reset()
    {
        RemainingBattery = 100;
        Inventory = new Collectible[Inventory.Length];
        InventoryIndex = 0;
        HasGoggles = false;
    }
}
