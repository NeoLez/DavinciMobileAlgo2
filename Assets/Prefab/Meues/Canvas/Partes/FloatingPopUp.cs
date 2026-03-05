using Root.FactoryAndPool;
using UnityEngine;

public class FloatingPopUp : Poolable
{
    [Header("Configuraci�n 3D")]
    [Tooltip("Velocidad de subida (en unidades del mundo).")]
    [SerializeField] private float moveSpeed = 2f;

    [Tooltip("Segundos antes de desaparecer.")]
    [SerializeField] private float lifetime = 1.0f;

    private float destroyTime;
    
    private void Awake() {
        destroyTime = Time.time + lifetime;
    }

    void Update()
    {
        // Mueve el objeto hacia ARRIBA en el mundo
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
        if (destroyTime >= Time.time) {
            TurnOff();
        }
    }
}