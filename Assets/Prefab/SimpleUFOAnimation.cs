using UnityEngine;

public class SimpleUFOAnimation : MonoBehaviour
{
    [Header("--- Configuración de Rotación ---")]
    [Tooltip("Velocidad de rotación en grados por segundo.")]
    [Range(0f, 360f)] public float rotationSpeed = 90f;

    [Tooltip("Elige el eje de rotación. Para un UFO clásico suele ser Y (Vector3.up), pero pediste X (Vector3.right).")]
    // Aquí definimos que por defecto rote en X (Vector3.right), como pediste.
    // Si quisieras que gire como un trompo, cambia esto a Vector3.up en el inspector.
    public Vector3 rotationAxis = Vector3.right;

    [Header("--- Configuración de Flotación (Bobbing) ---")]
    [Tooltip("Qué tan rápido sube y baja (Frecuencia).")]
    public float bobSpeed = 2f;

    [Tooltip("Qué tan alto y bajo se mueve desde su centro (Amplitud).")]
    public float bobHeight = 0.25f;

    private Vector3 startLocalPosition;

    void Start()
    {
        // Es fundamental guardar la posición local inicial.
        // El UFO flotará alrededor de este punto central.
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        AnimateUFO();
    }

    void AnimateUFO()
    {
        // --- PARTE 1: ROTACIÓN ---
        // Rotamos el objeto sobre el eje elegido, multiplicado por la velocidad y el tiempo.
        // Usamos Space.Self para asegurar que rote sobre su propio eje local, sin importar cómo esté el padre.
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);


        // --- PARTE 2: FLOTACIÓN (Subir y Bajar) ---
        // Usamos Mathf.Sin (Seno) que genera una onda suave entre -1 y 1 a lo largo del tiempo.
        // Multiplicamos por bobHeight para definir cuánto sube y baja.
        float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;

        // Calculamos la nueva posición Y sumando el desplazamiento a la posición inicial.
        float newY = startLocalPosition.y + yOffset;

        // Aplicamos la nueva posición local. IMPORTANTE: Mantenemos las X y Z originales
        // para que el modelo no se desplace lateralmente, solo arriba y abajo.
        transform.localPosition = new Vector3(startLocalPosition.x, newY, startLocalPosition.z);
    }
}