public class EnemyEncounter : Event
{
    public Hero hero;
    public Enemy enemy;

    // TODO: increase chance with each iteration?
    public Enemy TrySpawnEnemy(Location location)
    {
        for (var tryIndex = 0; tryIndex < 100; tryIndex++)
        {
            //foreach (var enemyData in location.XMLData.Enemies)
            //{
            //    //if (Globals.RNGesus.NextDouble() < enemyData.Value)
            //    //    return new Enemy(enemyData.Key);
            //}
        }

        //throw new Exception("Generating enemy takes too many tries");
        return null;
    }
}