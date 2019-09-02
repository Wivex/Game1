public class Enemy : Unit
{
    internal EnemyData enemyData;

    internal Enemy(EnemyData data)
    {
        enemyData = data;
        InitData(enemyData);
    }
}