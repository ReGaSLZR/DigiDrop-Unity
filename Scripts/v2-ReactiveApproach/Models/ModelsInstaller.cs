using Zenject;

public class ModelsInstaller : MonoInstaller<ModelsInstaller>
{
	
    public override void InstallBindings()
    {
		Container.Bind<CatcherModel>().AsSingle();
		Container.Bind<CountdownTimerModel>().AsSingle();
		Container.Bind<GameControlModel>().AsSingle();

    }

}