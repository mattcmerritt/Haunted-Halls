using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombinationInput : MonoBehaviour
{
    public List<int> Digits;
    public List<TMP_Text> DigitTextboxes;
    public NoteManager NoteManager;
    public Safe Safe;

    private void Awake()
    {
        NoteManager = GameObject.FindObjectOfType<NoteManager>();
        Safe = GameObject.FindObjectOfType<Safe>();
    }

    public void CompareToCombination()
    {
        string expected = NoteManager.Combination;
        string found = "";

        foreach (int i in Digits)
        {
            found += i;
        }

        if (expected == found)
        {
            Safe.Unlock();
        }
    }

    public void IncreaseDigit(int digit)
    {
        // modify digit in list
        Digits[digit - 1] += 1;

        // keeping digits in bounds
        if (Digits[digit - 1] > 9)
        {
            Digits[digit - 1] = 0;
        }

        // show digit on display
        DigitTextboxes[digit - 1].SetText("" + Digits[digit - 1]);
    }

    public void DecreaseDigit(int digit)
    {
        // modify digit in list
        Digits[digit - 1] -= 1;

        // keeping digits in bounds
        if (Digits[digit - 1] < 0)
        {
            Digits[digit - 1] = 9;
        }

        // show digit on display
        DigitTextboxes[digit - 1].SetText("" + Digits[digit - 1]);
    }
    
}
