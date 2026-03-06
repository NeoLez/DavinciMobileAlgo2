using Root.Gameplay.Stats;
using UnityEngine;
using Root; // <--- AGREGADO: Para conectar con tu SoundFXManager

namespace Root.Gameplay
{
    [RequireComponent(typeof(Stats.Stats), typeof(CircleCollider2D))]
    public class Tower : MonoBehaviour
    {
        private Stats.Stats stats;
        [SerializeReference, SubclassSelector] private TowerAction action;
        private Enemy targetedEnemy;
        private float targetDistance = float.MaxValue;
        [SerializeField] private TowerSO TowerSO;
        [SerializeField] private int upgradeLevel;

        [Header("Configuración de Sonido")] // <--- AGREGADO
        [SerializeField] private AudioClip shootSound;
        [SerializeField, Range(0f, 1f)] private float volume = 1f;

        private void Start()
        {
            stats = GetComponent<Stats.Stats>();
            GetComponent<CircleCollider2D>().radius = stats.GetValue(Stat.TowerRange).value;
            action.Initialize(this);
        }

        private void Update()
        {
            action.Update();
        }

        // --- FUNCIÓN NUEVA PARA DISPARAR EL SONIDO ---
        public void PlayShootSound()
        {
            if (shootSound != null && SoundFXManager.Instance != null)
            {
                SoundFXManager.Instance.PlaySound(shootSound, transform, volume);
            }
        }

        public Stats.Stats GetStats()
        {
            return stats;
        }

        public TowerSO GetTowerSO()
        {
            return TowerSO;
        }

        public int GetUpgradeLevel()
        {
            return upgradeLevel;
        }

        public Enemy GetTargetedEnemy()
        {
            return targetedEnemy;
        }

        [SerializeField] private LayerMask enemyLayer;
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Enemy")) return;

            float dist = Vector2.Distance(transform.position, other.transform.position);
            if (dist < targetDistance)
            {
                targetedEnemy = other.GetComponent<Enemy>();
                targetedEnemy.OnTurnedOff += EnemyDied;
                targetDistance = dist;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (targetedEnemy == null || other.gameObject == null) return;

            if (targetedEnemy.gameObject == other.gameObject)
            {
                targetedEnemy.OnTurnedOff -= EnemyDied;
                targetedEnemy = null;
                targetDistance = float.MaxValue;
            }
        }

        private void EnemyDied()
        {
            targetedEnemy = null;
            targetDistance = float.MaxValue;
        }
    }
}