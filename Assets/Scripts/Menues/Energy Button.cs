using UnityEngine;
using TMPro;
using Root.Database;


public class EnergyButton : MonoBehaviour
{
    [SerializeField] private TMP_Text staminaText;

    void Update()
    {
        if (Database.Ins != null && Database.Ins.staminaSystem != null)
        {
            int currentStamina = Database.Ins.staminaSystem.GetStamina();
            int maxStamina = Database.Ins.staminaSystem.GetMaxStamina();
            staminaText.text = $"{currentStamina}/{maxStamina}";
        }
        else
        {
            staminaText.text = "-";
        }
    }
}