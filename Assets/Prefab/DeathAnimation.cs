using System.Collections;
using UnityEngine;
using Root.FactoryAndPool; // Agrego esto para acceder a Poolable

public class DeathAnimation : MonoBehaviour
{
    [Header("Configuracion")]
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Vector3 spinSpeed = new Vector3(0, 720, 0);

    private Poolable _poolable;

    private void Awake()
    {
        // reviso el componente poolable que tiene el enemigo
        _poolable = GetComponent<Poolable>();
    }

    public void PlayDeathAnimationAndDestroy()
    {
        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        Vector3 startScale = transform.localScale;
        float timer = 0f;

        while (timer < animationDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / animationDuration;

            transform.Rotate(spinSpeed * Time.deltaTime);
            transform.localScale = Vector3.Lerp(startScale, Vector3.zero, progress);

            yield return null;
        }

        // Importante: Reseteo la escala antes de apagarlo 
        // para que cuando vuelva a salir de la pool no este en cero
        transform.localScale = startScale;

        // Si tiene el componente poolable llamo a TurnOff
        if (_poolable != null)
        {
            _poolable.TurnOff();
        }
        else
        {
            // Si por algun motivo no es poolable lo Destruyonormalmente
            Destroy(gameObject);
        }
    }
}