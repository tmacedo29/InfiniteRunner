using FFImageLoading.Maui;

public delegate void Callback();

public class Player : AnimatedGif
{
  //------------------------------------------------------------------------------------------------------------------------

  public Callback DeathCallback;

  //------------------------------------------------------------------------------------------------------------------------

  public Player(Image imageView) : base(imageView)
  {
    for (int i = 1; i <= 24; i++)
    {
      animationFilenameList01.Add($"player{i.ToString("D2")}.png");  
    }

    for (int i = 1; i <= 27; i++)
    {
      animationFilenameList02.Add($"dead{i.ToString("D2")}.png");  
    }
    
    SetActiveAnimation(1);
  }

  //------------------------------------------------------------------------------------------------------------------------

  public void Die()
  {
    SetActiveAnimation(2);
    shouldLoop = false;
  }

  //------------------------------------------------------------------------------------------------------------------------

  public void Run()
  {
    shouldLoop = true;
    SetActiveAnimation(1);
    Play();
  }

  //------------------------------------------------------------------------------------------------------------------------

  public void MoveY(int step)
  {
    imageView.TranslationY += step;
  }

  //------------------------------------------------------------------------------------------------------------------------

  public double MoveY()
  {
    return imageView.TranslationY;
  }
  
  //------------------------------------------------------------------------------------------------------------------------

  public void SetPosY(int posY)
  {
    imageView.TranslationY = posY;
  }

  //------------------------------------------------------------------------------------------------------------------------

  public override void OnStop()
  {
    base.OnStop();
    if (DeathCallback != null)
      DeathCallback();
  }

  //------------------------------------------------------------------------------------------------------------------------
}