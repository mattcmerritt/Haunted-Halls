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
}
