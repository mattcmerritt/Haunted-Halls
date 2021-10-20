using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    // warning display data
    public GameObject WarningHUD;
    public bool Detected;

    // inventory data
    public GameObject Inventory;
    public bool InventoryActive;

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
}
