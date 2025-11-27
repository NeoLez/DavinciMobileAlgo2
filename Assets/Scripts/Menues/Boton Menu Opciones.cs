using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Root;

// Script para los botones del menú
namespace Root
{
    public class BotonMenuOpciones : MonoBehaviour
    {
        [SerializeField] private ComponentActivator panelManager; // Referencia al manager de paneles
        [SerializeField] private int panelIndex; // Índice del panel a mostrar

        // Método que se llama al hacer click en el botón
        public void OnClick()
        {
            panelManager.DesactivarTodos(); // Desactiva todos los paneles
            panelManager.ActivarPanel(panelIndex); // Activa el panel correspondiente
        }
    }
}
