using Prism;
using Prism.Ioc;
using BackgroundJob.ViewModels;
using BackgroundJob.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Shiny.Jobs;
using Shiny;
using BackgroundJob.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BackgroundJob
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await ShinyHost.Resolve<IJobManager>().Schedule(MyShinyStartup.RepeatedJob);

        }

        protected override async void OnSleep()
        {
            base.OnSleep();
            await ShinyHost.Resolve<IJobManager>().Schedule(MyShinyStartup.RepeatedJob);

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
        }
    }
}
