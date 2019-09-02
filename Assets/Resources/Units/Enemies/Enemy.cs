public class Enemy : Unit
{
    internal EnemyData enemyData;

    internal override UnitStats CurStats
    {
        get => curStats;
        set => curStats = value;
    }

    internal Enemy(EnemyData data)
    {
        enemyData = data;
        InitData(enemyData);
    }
}