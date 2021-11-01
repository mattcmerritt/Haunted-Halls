using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelpOverlay : MonoBehaviour
{
    public bool InteractActive, GogglesActive, InventoryActive;
    public GameObject Interact, Goggles, Inventory;
    public TMP_Text InteractText;

    public float GogglesActiveTimer;
    public float InventoryActiveTimer;

    public float PromptTime;

    // counting down to hide any active prompts
    private void Update()
    {
        if (GogglesActive && GogglesActiveTimer > 0f)
        {
            GogglesActiveTimer -= Time.deltaTime;
        }
        else if (GogglesActive && GogglesActiveTimer <= 0f)
        {
            ToggleGogglesPrompt();
        }

        if (InventoryActive && InventoryActiveTimer > 0f)
        {
            InventoryActiveTimer -= Time.deltaTime;
        }
        else if (InventoryActive && InventoryActiveTimer <= 0f)
        {
            ToggleInventoryPrompt();
        }
    }

    public void ShowInteractPrompt(string message)
    {
        InteractActive = true;
        Interact.SetActive(InteractActive);

        if (InteractActive)
        {
            InteractText.SetText("- " + message);
        }
    }

    public void HideInteractPrompt()
    {
        InteractActive = false;
        Interact.SetActive(InteractActive);
    }

    public void ToggleGogglesPrompt()
    {
        GogglesActive = !GogglesActive;
        Goggles.SetActive(GogglesActive);

        if (GogglesActive)
        {
            GogglesActiveTimer = PromptTime;
        }    
    }

    public void ToggleInventoryPrompt()
    {
        InventoryActive = !InventoryActive;
        Inventory.SetActive(InventoryActive);

        if (InventoryActive)
        {
            InventoryActiveTimer = PromptTime;
        }
    }
}
