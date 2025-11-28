namespace Root {
    public static class EventPayloads {
        public class BattleEndEvent {
            public bool Victory { get; private set; }
            public BattleEndEvent(bool victory) => Victory = victory;
        }

        public class EnemyReachedEnd {
            public int Damage { get; private set; }
            public EnemyReachedEnd(int damage) => Damage = damage;
        }
    }
}