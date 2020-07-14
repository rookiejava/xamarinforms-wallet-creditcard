using Xamarin.Forms;
using PanCardView.Tizen;
using Rg.Plugins.Popup.Tizen;

namespace XFWallet.Tizen
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            MainWindow.IndicatorMode = ElmSharp.IndicatorMode.Hide;
            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            Popup.Init();
            Forms.Init(app, true);
            CardsViewRenderer.Preserve();
            app.Run(args);
        }
    }
}
