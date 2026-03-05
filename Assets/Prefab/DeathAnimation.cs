using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float animationDuration = 0.5f;
    [SerializeField] private Vector3 spinSpeed = new Vector3(0, 720, 0);

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

        Destroy(gameObject);
    }
}