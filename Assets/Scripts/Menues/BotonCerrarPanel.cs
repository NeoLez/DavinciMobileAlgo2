using UnityEngine;
using Root;

public class BotonCerrarPanel : MonoBehaviour
{
    [SerializeField] private ComponentActivator panelManager;
    [SerializeField] private int panelIndex;

    public void OnClick()
    {
        panelManager.DesactivarPanel(panelIndex);
    }
}