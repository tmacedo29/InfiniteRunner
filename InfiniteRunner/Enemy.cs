

public class Enemy
{
  Image imageView;
  //-------------------------------------------------------------------------------------

  public Enemy(Image imgView)
  {
    this.imageView = imgView;
  }

  //-------------------------------------------------------------------------------------

  public void MoveX(double step)
  {
    imageView.TranslationX -= step;
  }

  //-------------------------------------------------------------------------------------

  public double GetX()
  {
    return imageView.TranslationX;
  }

  //-------------------------------------------------------------------------------------

  public void Reset()
  {
    imageView.TranslationX = 500;
  }

  //-------------------------------------------------------------------------------------
}