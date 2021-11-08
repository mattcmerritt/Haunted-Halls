using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    // player
    public GameObject Player;
    public PlayerMovement PlayerMovement;
    public PlayerBehavior PlayerBehavior;

    // cameras
    public GameObject BackupCamera;

    public bool Playing = true;

    // help overlay
    public GameObject HelpOverlay;

    // combination
    public GameObject CombinationOverlay;

    // battery bar
    public float MinAnchor, MaxAnchor;
    public RectTransform BatteryBar;

    private void Awake()
    {
        PlayerMovement = Player.GetComponent<PlayerMovement>();
        PlayerBehavior = Player.GetComponent<PlayerBehavior>();
    }

    public void Update()
    {
        if (Playing)
        {
            WarningHUD.SetActive(Detected);

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ToggleInventory();
            }
        }
        else
        {
            WarningHUD.SetActive(false);
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
        if (items.Length != Slots.Length)
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

        BatteryBar.anchorMax = new Vector2(MinAnchor + (MaxAnchor - MinAnchor) * battery/100f, BatteryBar.anchorMax.y);
    }

    public void DisplayDeathScreen()
    {
        PlayerBehavior.GogglesActive = false;
        Playing = false;
        // clear all old data
        PlayerLost();
        UpdateInventory(new Collectible[Slots.Length]);
        InventoryActive = false;
        Inventory.SetActive(false);
        GogglesOverlay.SetActive(false);
        Crosshair.SetActive(false);
        BackupCamera.SetActive(true);
        Player.SetActive(false);
        HelpOverlay.SetActive(false);
        CombinationOverlay.SetActive(false);

        // show death screen
        DeathScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResetGame()
    {
        Playing = true;
        Player.SetActive(true);
        BackupCamera.SetActive(false);
        ObjectManager.ResetAllObjects();
        DeathScreen.SetActive(false);
        WinScreen.SetActive(false);
        Crosshair.SetActive(true);
        HelpOverlay.SetActive(true);
        CombinationOverlay.SetActive(false);
    }

    public void DisplayWinScreen()
    {
        PlayerBehavior.GogglesActive = false;
        Playing = false;
        // clear all old data
        PlayerLost();
        UpdateInventory(new Collectible[Slots.Length]);
        InventoryActive = false;
        Inventory.SetActive(false);
        GogglesOverlay.SetActive(false);
        Crosshair.SetActive(false);
        BackupCamera.SetActive(true);
        Player.SetActive(false);
        HelpOverlay.SetActive(false);
        CombinationOverlay.SetActive(false);

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

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowCombinationOverlay()
    {
        Cursor.lockState = CursorLockMode.None;
        CombinationOverlay.SetActive(true);

        // lock camera position
        PlayerMovement.LockCamera();

        // hiding other UI
        Crosshair.SetActive(false);
        HelpOverlay.SetActive(false);
    }

    public void HideCombinationOverlay()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CombinationOverlay.SetActive(false);

        // unlock camera position
        PlayerMovement.UnlockCamera();

        // showing other UI
        Crosshair.SetActive(true);
        HelpOverlay.SetActive(true);
    }
}
