

using FFImageLoading.Maui;

public class AnimatedGif
{
  //------------------------------------------------------------------------------------------

  protected List<String> animationFilenameList01 = new List<string>();
  protected List<String> animationFilenameList02 = new List<string>();
  protected List<String> animationFilenameList03 = new List<string>();

  protected bool shouldLoop = true;

  int activeAnimation = 1;
  int timeBetweenFrames = 25;

  bool stopped = false;

  int currentFrame = 0;

  protected Image imageView;

  //------------------------------------------------------------------------------------------

  public AnimatedGif(Image imageView)
  {
    this.imageView = imageView;
  }

  //------------------------------------------------------------------------------------------

  public void SetTimeBetweenFrames(int milliseconds)
  {
    timeBetweenFrames = milliseconds;
  }

  //------------------------------------------------------------------------------------------

  public void StopAnimation()
  {
    stopped = true;
  }

  //------------------------------------------------------------------------------------------

  public void Play()
  {
    stopped = false;
  }

  //------------------------------------------------------------------------------------------

  public void SetActiveAnimation(int a)
  {
    activeAnimation = a;
    currentFrame = 0;
  }

  //------------------------------------------------------------------------------------------

  public void Draw()
  {
    if (stopped)
      return;
    
    string filename = "";
    int sizeOfAnimation = 0;
    if (activeAnimation == 1)
    {
      filename = animationFilenameList01[currentFrame];
      sizeOfAnimation = animationFilenameList01.Count;
    }
    else if (activeAnimation == 2)
    {
      filename = animationFilenameList02[currentFrame];
      sizeOfAnimation = animationFilenameList02.Count;
    }
    else if (activeAnimation == 3)
    {
      filename = animationFilenameList03[currentFrame];
      sizeOfAnimation = animationFilenameList03.Count;
    }

    imageView.Source = ImageSource.FromFile(filename);
    currentFrame++;
    if (currentFrame >= sizeOfAnimation - 1)
    {
      if (shouldLoop)
        currentFrame = 0;
      else
      {
        stopped = true;
        OnStop();
      }
    }
  }

  //------------------------------------------------------------------------------------------

  public virtual void OnStop()
  {

  }

  //------------------------------------------------------------------------------------------
  
}