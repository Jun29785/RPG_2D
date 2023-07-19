namespace Define
{
    public enum UnitType 
    {
        Knight,
        Bandit,
        Dragon
    }

    public enum UnitState
    {
        Team,
        Individual
    }

    public enum TeamState
    {
        Wait,
        Move
    }

    public enum GameEffect
    {
        None,
    }

    public enum AttackKind
    {
        None,
        Normal,
        Critical
    }

    public enum EnemyType
    {
        Normal_1 = 0,
        Normal_2,
        Normal_3
    }
}
