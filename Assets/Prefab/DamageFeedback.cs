using System.Collections;
using UnityEngine;

public class DamageFeedback : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float shrinkAmount = 0.8f;
    [SerializeField] private float effectDuration = 0.15f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void PlayDamageEffect()
    {
        StopAllCoroutines();
        StartCoroutine(PunchScaleRoutine());
    }

    private IEnumerator PunchScaleRoutine()
    {
        Vector3 targetScale = originalScale * shrinkAmount;
        float halfDuration = effectDuration / 2f;
        float timer = 0f;

        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, timer / halfDuration);
            yield return null;
        }

        timer = 0f;

        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, timer / halfDuration);
            yield return null;
        }

        transform.localScale = originalScale;
    }
}