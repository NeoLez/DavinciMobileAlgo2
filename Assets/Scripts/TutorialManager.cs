using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Root.Gameplay
{
    public class TutorialManager : MonoBehaviour
    {
        [Header("UI del Tutorial")]
        [SerializeField] private TMP_Text tutorialText;
        [SerializeField] private GameObject tutorialPanel;

        [Header("Referencias de Escena")]
        [SerializeField] private EnemySpawner spawner;

        [Header("Configuracion de Tiempos")]
        [Tooltip("Tiempo en segundos antes de terminar el tutorial tras el ultimo paso")]
        [SerializeField] private float tiempoEsperaAlFinal = 3f;

        [Header("IDs de Localizacion (Google Sheet)")]
        [SerializeField] private string idStepConstruir = "ID_Tutorial_Build";
        [SerializeField] private string idStepMejorar = "ID_Tutorial_Upgrade";
        [SerializeField] private string idStepHorda = "ID_Tutorial_Wave";
        [SerializeField] private string idStepFin = "ID_Tutorial_Done";

        private int currentStep = 0;

        private void Start()
        {
            // Apago el spawner para que no salgan enemigos apenas arranca la escena
            if (spawner != null) spawner.enabled = false;

            // Me suscribo a los eventos necesarios para que el tutorial avance
            EventManager.Subscribe<EventPayloads.TowerBuilt>(OnTowerBuilt);
            EventManager.Subscribe<EventPayloads.TowerUpgraded>(OnTowerUpgraded);
            EventManager.Subscribe<EventPayloads.EnemiesEliminated>(OnEnemiesEliminated);

            // Si el usuario cambia el idioma en tiempo real, refresco el texto
            if (Localization.Ins != null) Localization.Ins.OnUpdate += RefreshText;

            // Inicio el primer paso del tutorial
            SetStep(0);
        }

        private void OnDestroy()
        {
            // Limpio las suscripciones para evitar errores de memoria o "NullReference" al cambiar de escena
            EventManager.Unsubscribe<EventPayloads.TowerBuilt>(OnTowerBuilt);
            EventManager.Unsubscribe<EventPayloads.TowerUpgraded>(OnTowerUpgraded);
            EventManager.Unsubscribe<EventPayloads.EnemiesEliminated>(OnEnemiesEliminated);

            if (Localization.Ins != null) Localization.Ins.OnUpdate -= RefreshText;
        }

        private void SetStep(int step)
        {
            currentStep = step;
            RefreshText();

            // Si llego al paso 2 (la horda), activo el spawner y lo despierto manualmente
            if (step == 2 && spawner != null)
            {
                spawner.enabled = true;
                spawner.StartSpawnerManual();
            }

            // Si es el ultimo paso, espero los segundos configurados en el inspector para terminar
            if (step == 3)
            {
                Invoke(nameof(TerminarTutorial), tiempoEsperaAlFinal);
            }
        }

        public void RefreshText()
        {
            // Me aseguro de que el sistema de localizacion este listo antes de pedirle el texto
            if (Localization.Ins == null || !Localization.Ins.IsInitialized()) return;

            string keyToUse = "";
            switch (currentStep)
            {
                case 0: keyToUse = idStepConstruir; break;
                case 1: keyToUse = idStepMejorar; break;
                case 2: keyToUse = idStepHorda; break;
                case 3: keyToUse = idStepFin; break;
            }

            if (!string.IsNullOrEmpty(keyToUse))
            {
                // Busco la traduccion segun la ID que asigne en el inspector
                tutorialText.text = Localization.Ins.GetTranslate(keyToUse);
            }
        }

        private void OnTowerBuilt(EventPayloads.TowerBuilt payload)
        {
            // Si construyo la torre y estoy en el paso 0, paso al siguiente
            if (currentStep == 0) SetStep(1);
        }

        private void OnTowerUpgraded(EventPayloads.TowerUpgraded payload)
        {
            // Si mejoro la torre y estoy en el paso 1, paso a la horda
            if (currentStep == 1) SetStep(2);
        }

        private void OnEnemiesEliminated(EventPayloads.EnemiesEliminated payload)
        {
            // Cuando mato a todos los enemigos de la horda, paso al final
            if (currentStep == 2) SetStep(3);
        }

        private void TerminarTutorial()
        {
            // Disparo el evento de fin de enemigos para que el Level.cs maneje la carga de la escena de victoria
            EventManager.Trigger(new EventPayloads.EnemiesEliminated());
        }
    }
}