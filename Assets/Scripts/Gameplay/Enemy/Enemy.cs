using Root.FactoryAndPool;
using Root.Gameplay.Stats;
using UnityEngine;
using UnityEngine.Assertions;

namespace Root.Gameplay
{
    [RequireComponent(typeof(Stats.Stats))]
    public class Enemy : Poolable
    {
        [SerializeReference, SubclassSelector] private EnemyMovementBehaviour movementBehaviour;
        private Stats.Stats stats;
        private int health;
        [SerializeField] private EnemySO settings;
        private bool isDead;

        private void Update()
        {
            movementBehaviour.Update(Time.deltaTime);
        }

        public float GetPathPercentageCompletion()
        {
            return movementBehaviour.GetPathPercentageCompletion();
        }

        public Stats.Stats GetStats()
        {
            return stats;
        }

        public void Initialize(Vector3 position)
        {
            isDead = false;
            transform.position = position;

            // Me aseguro de que vuelva a su tama�o original al salir de la pool
            // porque la animacion de muerte lo deja en escala cero
            transform.localScale = Vector3.one;

            stats = GetComponent<Stats.Stats>();
            health = (int)stats.GetValue(Stat.MaxHealth).value;
            movementBehaviour.Initialize(this);
        }

        /// <summary>
        /// Returns leftover damage
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public int TakeDamage(int amount)
        {
            Assert.IsTrue(amount > 0);
            health -= amount;

            // Descomento el feedback de da�o para que se vea
            GetComponent<DamageFeedback>()?.PlayDamageEffect();

            if (health <= 0)
            {
                Die();
                return Mathf.Abs(health);
            }

            return 0;
        }

        public void Die()
        {

            if (isDead) return;
            isDead = true;

            if (settings.CashReward > 0)
            {
                var coins = PoolManager.Instance.GetObject(settings.MoneyImagePrefab);
                coins.transform.position = transform.position;
                Level.Ins.gold.AddGold(settings.CashReward);
            }


            EventManager.Trigger(new EventPayloads.EnemyDied());

            // Llamo a la animacion y dejo que ella se encargue de hacer el TurnOff() al terminar
            GetComponent<DeathAnimation>()?.PlayDeathAnimationAndDestroy();

        }
    }
}