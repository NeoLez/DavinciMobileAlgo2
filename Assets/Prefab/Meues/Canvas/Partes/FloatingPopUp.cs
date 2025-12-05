using UnityEngine;

public class FloatingPopUp : MonoBehaviour
{
    [Header("Configuración 3D")]
    [Tooltip("Velocidad de subida (en unidades del mundo).")]
    [SerializeField] private float moveSpeed = 2f;

    [Tooltip("Segundos antes de desaparecer.")]
    [SerializeField] private float lifetime = 1.0f;

    void Start()
    {
        // Programa la destrucción automática
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Mueve el objeto hacia ARRIBA en el mundo
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime, Space.World);
    }
}