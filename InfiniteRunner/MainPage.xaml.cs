namespace InfiniteRunner;

public partial class MainPage : ContentPage
{
  bool estaMorto = false;
  bool estaPulando = false;
  bool estaNoAr = false;

  bool estaNoChao = true;

  int tempoPulando = 0;
  int tempoNoAr = 0;
  const int maxTempoPulando = 8;
  const int maxTempoNoAr = 14;

  const int forcaGravidade = 20;
  const int forcaPulo = 40;

  int tempoEntreFrames = 25;
  int velocidadeNivel01 = 0;
  int velocidadeNivel02 = 0;
  int velocidadeNivel03 = 0;
  int velocidade = 0;

  double larguraJanela = 0;

  Image imgCurrentEnemy = null;

  Player player;

  Enemies enemies;

  //------------------------------------------------------------------------------------------------

	public MainPage()
	{
		InitializeComponent();
	}

  void OnGridTapped(object sender, TappedEventArgs e)
  {
    if (estaMorto) 
    {
      estaMorto = false;
      player.Run();
      Desenha();
    }
    else if (estaNoChao)
      estaPulando = true;
  }

  //------------------------------------------------------------------------------------------------

  protected override void OnAppearing()
  {
    base.OnAppearing();
    player = new Player(imgPlayer);
    player.DeathCallback = OnDie;

    Desenha();
  }

  //------------------------------------------------------------------------------------------------

  void OnDie()
  {
    estaMorto = true;
  }

  //------------------------------------------------------------------------------------------------

  protected override void OnSizeAllocated(double w, double h)
  {
    base.OnSizeAllocated(w, h);
    larguraJanela = w;
    CorrigeTamanhoCenario(w,h);
    DefineVelocidade(w);

    enemies = new Enemies(-larguraJanela);
    enemies.Add( new Enemy(imgEnemy01) );
    enemies.Add( new Enemy(imgEnemy02) );
    enemies.Add( new Enemy(imgEnemy03) );
    enemies.Add( new Enemy(imgEnemy04) );
  }

  //------------------------------------------------------------------------------------------------

  async Task Desenha()
  {
    while (!estaMorto)
    {
      GerenciaCenario();
      
      if (enemies != null)
        enemies.Draw(velocidade);

      if (!estaPulando && !estaNoAr)
      {
        AplicaGravidade();
        player.Draw();
      }
      else
        AplicaPulo();

      await Task.Delay(tempoEntreFrames);
    }
  }

  //------------------------------------------------------------------------------------------------

  void AplicaGravidade()
  {
    if (player.MoveY() < 0)
      player.MoveY(forcaGravidade);
    else if (player.MoveY() >= 0)
    {
      player.SetPosY(0);
      estaNoChao = true;
    }
  }

  //------------------------------------------------------------------------------------------------

  void AplicaPulo()
  {
    estaNoChao = false;
    if (estaPulando && tempoPulando >= maxTempoPulando)
    {
      estaPulando = false;
      estaNoAr    = true;
      tempoNoAr = 0;
    }
    else if (estaNoAr && tempoNoAr >= maxTempoNoAr)
    {
      estaPulando  = false;
      estaNoAr     = false;
      tempoPulando = 0;
      tempoNoAr    = 0;
    }
    else if (estaPulando && tempoPulando < maxTempoPulando)
    {
      player.MoveY(-forcaPulo);
      tempoPulando++;
    }
    else if (estaNoAr)
      tempoNoAr++;
  }

  //------------------------------------------------------------------------------------------------

  void DefineVelocidade(double w)
  {
    velocidadeNivel01 = (int)(w * 0.001);
    velocidadeNivel02 = (int)(w * 0.004);
    velocidadeNivel03 = (int)(w * 0.008);
    velocidade        = (int)(w * 0.01);
  }

  //------------------------------------------------------------------------------------------------

  void GerenciaCenario()
  {
    MoveCenario();
    GerenciaCenario(horizontalStackLayer1);
    GerenciaCenario(horizontalStackLayer2);
    GerenciaCenario(horizontalStackLayer3);
    GerenciaCenario(horizontalStackChao);
  }

  //------------------------------------------------------------------------------------------------

  void MoveCenario()
  {
    horizontalStackLayer1.TranslationX -= velocidadeNivel01;
    horizontalStackLayer2.TranslationX -= velocidadeNivel02;
    horizontalStackLayer3.TranslationX -= velocidadeNivel03;
    horizontalStackChao.TranslationX   -= velocidade;
  }

  //------------------------------------------------------------------------------------------------

  void GerenciaCenario(HorizontalStackLayout horizontalLayout)
  {
    var view = horizontalLayout.Children.First() as Image;
    if (view!.WidthRequest + horizontalLayout.TranslationX < 0)
    {
      horizontalLayout.Children.Remove(view);
      horizontalLayout.Children.Add(view);
      horizontalLayout.TranslationX = view!.TranslationX;
    }
  }

  //------------------------------------------------------------------------------------------------

  void CorrigeTamanhoCenario(double w, double h)
  {
    foreach(var child in horizontalStackLayer1.Children)
    {
      (child as Image).WidthRequest = w;
    }
    foreach(var child in horizontalStackLayer2.Children)
    {
      (child as Image).WidthRequest = w;
    }
    foreach(var child in horizontalStackLayer3.Children)
    {
      (child as Image).WidthRequest = w;
    }
    foreach(var child in horizontalStackChao.Children)
    {
      (child as Image).WidthRequest = w;
    }
    horizontalStackLayer1.WidthRequest = 1.5*w;
    horizontalStackLayer2.WidthRequest = 1.5*w;
    horizontalStackLayer3.WidthRequest = 1.5*w;
    horizontalStackChao.WidthRequest   = 1.5*w;
  }

  //------------------------------------------------------------------------------------------------

}

