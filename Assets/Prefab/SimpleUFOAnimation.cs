using UnityEngine;

public class SimpleUFOAnimation : MonoBehaviour
{
    [Header("--- Configuración de Rotación ---")]
    [Tooltip("Velocidad de rotación en grados por segundo.")]
    [Range(0f, 360f)] public float rotationSpeed = 90f;

   
   
    public Vector3 rotationAxis = Vector3.right;

    [Header("--- Configuración de Flotación (Bobbing) ---")]
    [Tooltip("Qué tan rápido sube y baja (Frecuencia).")]
    public float bobSpeed = 2f;

    [Tooltip("Qué tan alto y bajo se mueve desde su centro (Amplitud).")]
    public float bobHeight = 0.25f;

    private Vector3 startLocalPosition;

    void Start()
    {
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        AnimateUFO();
    }

    void AnimateUFO()
    {
        
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);


        
        float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobHeight;

       
        float newY = startLocalPosition.y + yOffset;

        
        transform.localPosition = new Vector3(startLocalPosition.x, newY, startLocalPosition.z);
    }
}