using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Don't forget this using directive for TextMeshPro

public class BudgetDisplay : MonoBehaviour
{
    public TextMeshProUGUI budgetText; // Assign this in the inspector
    public PlayerController player; // Assume this is your player class that contains the budget info

    void Update()
    {
        // Update the text to display the player's current budget
        // Assuming 'budget' is a public field or property in your Player class
        budgetText.text = "Budget: $" + player.GetBudget().ToString();
    }
}

