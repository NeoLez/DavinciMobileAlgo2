using System;
using Root.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Root
{
    public class BotonIniciarNivel : MonoBehaviour
    {
        [SerializeField] private string nombreEscenaNivel;
        [SerializeField] private int staminaCost;

        private void Awake()
        {
            staminaCost = RemoteManager.GetInt("levelStaminaCost");
        }

        public void ComenzarNivel()
        {
            if(!Database.Database.Ins.staminaSystem.ConsumeStamina(staminaCost)) return;
            SceneManager.LoadScene(nombreEscenaNivel);
        }

    }
}
