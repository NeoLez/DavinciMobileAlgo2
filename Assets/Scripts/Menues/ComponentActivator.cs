using UnityEngine;
using System.Collections.Generic;

namespace Root
{
    // Interfaz para activar y desactivar
    public interface IActivable
    {
        void Activar(); // Activa
        void Desactivar(); // Desactiva
    }

    // Clase para guardar el panel
    [System.Serializable]
    public class ActivableComponent : IActivable
    {
        [SerializeField] private GameObject panelObject; // El panel

        public void Activar()
        {
            if (panelObject != null)
                panelObject.SetActive(true); // Activa el panel
        }

        public void Desactivar()
        {
            if (panelObject != null)
                panelObject.SetActive(false); // Desactiva el panel
        }
    }

    // Script para manejar los paneles
    public class ComponentActivator : MonoBehaviour
    {
        [SerializeField]
        private List<ActivableComponent> componentes; // Lista de paneles

        public void ActivarTodos()
        {
            foreach (var c in componentes)
                c.Activar(); // Activa todos
        }

        public void DesactivarTodos()
        {
            foreach (var c in componentes)
                c.Desactivar(); // Desactiva todos
        }

        public void ActivarPanel(int index)
        {
            if (index >= 0 && index < componentes.Count)
                componentes[index].Activar(); // Activa uno
        }

        public void DesactivarPanel(int index)
        {
            if (index >= 0 && index < componentes.Count)
                componentes[index].Desactivar(); // Desactiva uno
        }
    }
}
