using UnityEngine;
using UnityEngine.SceneManagement;

namespace Root
{
    public class BotonIniciarNivel : MonoBehaviour
    {
        [SerializeField] private string nombreEscenaNivel;
        [SerializeField] private int staminaCost;
        
        public void ComenzarNivel()
        {
            if(!Database.Database.Ins.staminaSystem.ConsumeStamina(staminaCost)) return;
            SceneManager.LoadScene(nombreEscenaNivel);
        }

    }
}
