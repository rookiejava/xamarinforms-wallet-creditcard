using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;
using XFWallet.Renderers;
using XFWallet.Tizen.Renderers;
using XEntry = Xamarin.Forms.Entry;
using EEntry = ElmSharp.Entry;
using ELayout = ElmSharp.Layout;
using NEntry = Xamarin.Forms.Platform.Tizen.Native.Entry;

[assembly: ExportRenderer(typeof(CustomEntryBorderless), typeof(CustomEntryBorderlessRenderer))]
namespace XFWallet.Tizen.Renderers
{
    public class CustomEntryBorderlessRenderer : EntryRenderer
    {
        public CustomEntryBorderlessRenderer()
        {
             RegisterPropertyHandler(XEntry.CursorPositionProperty, UpdateCursorPosition);
        }

        protected override EEntry CreateNativeControl()
        {
            return new NEntry(Forms.NativeParent)
            {
                IsSingleLine = true,
            };
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.BackgroundColor = ElmSharp.Color.White;
            }
        }

        void UpdateCursorPosition()
        {
            ((NEntry)Control).CursorPosition = Element.CursorPosition;
        }
    }

    public class CustomEditfieldEntry : EditfieldEntry
    {
        public CustomEditfieldEntry(EvasObject parent) : base(parent)
        {
        }

		protected override ELayout CreateEditFieldLayout(EvasObject parent)
		{
			var layout = new ELayout(parent);
			layout.SetTheme("layout", "application", "default");
			layout.AllowFocus(true);
			layout.Unfocused += (s, e) =>
			{
				SetFocusOnTextBlock(false);
				layout.SignalEmit("elm,state,unfocused", "");
				OnEntryLayoutUnfocused();
			};
			layout.Focused += (s, e) =>
			{
				layout.SignalEmit("elm,state,focused", "");
				OnEntryLayoutFocused();
			};

			layout.KeyDown += (s, e) =>
			{
				if (e.KeyName == "Return")
				{
					if (!IsTextBlockFocused)
					{
						SetFocusOnTextBlock(true);
						e.Flags |= EvasEventFlag.OnHold;
					}
				}
			};
			Clicked += (s, e) => SetFocusOnTextBlock(true);

			Focused += (s, e) =>
			{
				layout.RaiseTop();
				layout.SignalEmit("elm,state,focused", "");
			};

			Unfocused += (s, e) =>
			{
				layout.SignalEmit("elm,state,unfocused", "");
			};

			return layout;
		}
	}
}