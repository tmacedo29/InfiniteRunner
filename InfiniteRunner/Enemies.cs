

public class Enemies
{
  //---------------------------------------------------------------------------------------------------

  List<Enemy> enemies = new List<Enemy>();
  Enemy current = null;
  double minX = 0;

  //---------------------------------------------------------------------------------------------------

  public Enemies(double minX)
  {
    this.minX = minX;
  }
  
  //---------------------------------------------------------------------------------------------------

  public void Add(Enemy enemy)
  {
    enemies.Add(enemy);
    if (current == null)
      current = enemy;
    Initialize();
  }

  //---------------------------------------------------------------------------------------------------

  public void Draw(int speed)
  {
    current.MoveX(speed);
    ManageEnemies();
  }

  //---------------------------------------------------------------------------------------------------

  void Initialize()
  {
    foreach(var enemy in enemies)
      enemy.Reset();
  }

  //---------------------------------------------------------------------------------------------------

  void ManageEnemies()
  {
    if (current.GetX() < minX)
    {
      Initialize();
      if (enemies.Count > 1)
      {
        var rand = Random.Shared.Next(0, enemies.Count);
        current = enemies[rand];
      }
      else
        current = enemies[0];
    }
  }

  //---------------------------------------------------------------------------------------------------
}