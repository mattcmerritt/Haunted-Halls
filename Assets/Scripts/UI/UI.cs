using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    // warning display data
    public GameObject WarningHUD;
    public bool Detected;

    // inventory data
    public GameObject Inventory;
    public bool InventoryActive;
    public Image[] Slots;

    // battery data
    public TMP_Text BatteryMeter;
    public GameObject GogglesOverlay;

    // death screen
    public GameObject DeathScreen;
    public ObjectManager ObjectManager;

    // win screen
    public GameObject WinScreen;

    // crosshair
    public GameObject Crosshair;

    public void Update()
    {
        WarningHUD.SetActive(Detected);


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    public void PlayerDetected()
    {
        Detected = true;
    }

    public void PlayerLost()
    {
        Detected = false;
    }

    public void ToggleInventory()
    {
        InventoryActive = !InventoryActive;
        Inventory.SetActive(InventoryActive);
    }

    public void UpdateInventory(Collectible[] items)
    {
        if (items.Length != 5)
        {
            Debug.LogError("Inventory Error: Not enough items passed to display in slots");
        }
        else
        {
            for (int i = 0; i < items.Length && i < Slots.Length; i++)
            {
                if (items[i] != null)
                {
                    Slots[i].sprite = items[i].Preview;
                }
                else
                {
                    Slots[i].sprite = null;
                }
            }
        }
    }

    public void UpdateBatteryLevel(float battery)
    {
        BatteryMeter.SetText("Remaining Battery: " + Mathf.RoundToInt(battery) + "%");
    }

    public void DisplayDeathScreen()
    {
        // clear all old data
        PlayerLost();
        UpdateInventory(new Collectible[5]);
        InventoryActive = false;
        Inventory.SetActive(false);
        GogglesOverlay.SetActive(false);
        Crosshair.SetActive(false);

        // show death screen
        DeathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResetGame()
    {
        ObjectManager.ResetAllObjects();
        DeathScreen.SetActive(false);
        WinScreen.SetActive(false);
        Crosshair.SetActive(true);
    }

    public void DisplayWinScreen()
    {
        // clear all old data
        PlayerLost();
        UpdateInventory(new Collectible[5]);
        InventoryActive = false;
        Inventory.SetActive(false);
        GogglesOverlay.SetActive(false);
        Crosshair.SetActive(false);

        // show win screen
        WinScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void DisplayGoggles()
    {
        GogglesOverlay.SetActive(true);
    }

    public void HideGoggles()
    {
        GogglesOverlay.SetActive(false);
    }
}
