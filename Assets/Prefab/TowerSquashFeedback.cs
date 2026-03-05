using System.Collections;
using UnityEngine;

public class TowerSquashFeedback : MonoBehaviour
{
    [Header("Configuración de Disparo")]
    [SerializeField] private float inflateAmount = 1.2f; // 1.2 significa un 20% más grande
    [SerializeField] private float animationDuration = 0.15f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void PlayShootEffect()
    {
        StopAllCoroutines();
        StartCoroutine(InflateRoutine());
    }

    private IEnumerator InflateRoutine()
    {
        //Multiplicamos el vector completo para inflarlo parejo
        Vector3 targetScale = originalScale * inflateAmount;

        float halfDuration = animationDuration / 2f;
        float timer = 0f;

        // Fase 1: Se infla
        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / halfDuration);
            yield return null;
        }

        timer = 0f;

        // Fase 2: Vuelve a su tamaño normal
        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / halfDuration);
            yield return null;
        }

        transform.localScale = originalScale;
    }
}