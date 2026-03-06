using UnityEngine;
using TMPro;

namespace Root.Gameplay
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text tutorialText;
        [SerializeField] private GameObject tutorialPanel;
        [SerializeField] private EnemySpawner spawner;

        // --- IDS DE TRADUCCIÓN (Asegurate que existan en tu Google Sheet) ---
        private const string ID_STEP_0 = "ID_Tutorial_Build";   // "Construye una torreta..."
        private const string ID_STEP_1 = "ID_Tutorial_Upgrade"; // "Mejora la torreta..."
        private const string ID_STEP_2 = "ID_Tutorial_Wave";    // "Prepárate para la horda..."
        private const string ID_STEP_3 = "ID_Tutorial_Done";    // "¡Tutorial completado!"

        private int currentStep = 0;

        private void Start()
        {
            if (spawner != null) spawner.enabled = false;

            EventManager.Subscribe<EventPayloads.TowerBuilt>(OnTowerBuilt);
            EventManager.Subscribe<EventPayloads.TowerUpgraded>(OnTowerUpgraded);
            EventManager.Subscribe<EventPayloads.EnemiesEliminated>(OnEnemiesEliminated);

            // Nos suscribimos al cambio de idioma por si el usuario lo cambia en medio del tutorial
            if (Localization.Ins != null) Localization.Ins.OnUpdate += RefreshText;

            SetStep(0);
        }

        private void OnDestroy()
        {
            EventManager.Unsubscribe<EventPayloads.TowerBuilt>(OnTowerBuilt);
            EventManager.Unsubscribe<EventPayloads.TowerUpgraded>(OnTowerUpgraded);
            EventManager.Unsubscribe<EventPayloads.EnemiesEliminated>(OnEnemiesEliminated);
            if (Localization.Ins != null) Localization.Ins.OnUpdate -= RefreshText;
        }

        private void SetStep(int step)
        {
            currentStep = step;
            RefreshText();

            if (step == 2 && spawner != null)
            {
                // Activamos el spawner y nos aseguramos que empiece a correr
                spawner.enabled = true;
                spawner.StartSpawnerManual();
            }
        }

        private void RefreshText()
        {
            if (Localization.Ins == null || !Localization.Ins.IsInitialized()) return;

            string key = currentStep switch
            {
                0 => ID_STEP_0,
                1 => ID_STEP_1,
                2 => ID_STEP_2,
                3 => ID_STEP_3,
                _ => ""
            };

            tutorialText.text = Localization.Ins.GetTranslate(key);
        }

        private void OnTowerBuilt(EventPayloads.TowerBuilt payload) => { if (currentStep == 0) SetStep(1); }
        private void OnTowerUpgraded(EventPayloads.TowerUpgraded payload) => { if (currentStep == 1) SetStep(2);
}
private void OnEnemiesEliminated(EventPayloads.EnemiesEliminated payload) => { if (currentStep == 2) SetStep(3); }

private void HideTutorial() { if (tutorialPanel != null) tutorialPanel.SetActive(false); }
    }
}