using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;
using EColor = ElmSharp.Color;

[assembly: ExportRenderer(typeof(Frame), typeof(CustomFrameRenderer))]
namespace Xamarin.Forms.Platform.Tizen
{
	public class CustomFrameRenderer : LayoutRenderer
	{
		const int _thickness = 2;

		static readonly EColor s_DefaultColor = EColor.Transparent;
		static readonly EColor s_ShadowColor = EColor.FromRgb(210, 210, 213);

		Frame FrameElement => (Element as Frame);
		bool HasBackgroundColor => !Element.BackgroundColor.IsDefault && FrameElement.CornerRadius != -1.0f;

		RoundRectangle _shadow = null;
		RoundRectangle _frame = null;

		public CustomFrameRenderer()
		{
			RegisterPropertyHandler(Frame.BorderColorProperty, UpdateColor);
			RegisterPropertyHandler(Frame.HasShadowProperty, UpdateShadowVisibility);
			RegisterPropertyHandler(Frame.CornerRadiusProperty, UpdateCornerRadius);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Layout> e)
		{
			if (Control == null)
			{
				SetNativeControl(new Canvas(Forms.NativeParent));
				_shadow = new RoundRectangle(NativeView);
				_shadow.Color = s_ShadowColor;
				Control.Children.Add(_shadow);

				_frame = new RoundRectangle(NativeView);
				_frame.Show();
				Control.Children.Add(_frame);
				Control.LayoutUpdated += OnLayoutUpdated;
			}
			base.OnElementChanged(e);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (Control != null)
				{
					Control.LayoutUpdated -= OnLayoutUpdated;
				}
			}
			base.Dispose(disposing);
		}

		protected override void UpdateBackgroundColor(bool initialize)
		{
			if (HasBackgroundColor)
				_frame.Color = FrameElement.BackgroundColor.ToNative();
			else
				Control.BackgroundColor = FrameElement.BackgroundColor.ToNative();
		}

		static void DrawFrame(RoundRectangle frame, int left, int top, int right, int bottom, int thickness)
		{
			frame.ClearPoints();
			if (left + thickness >= right || top + thickness >= bottom)
			{
				if (left >= right || top >= bottom)
					return;
				// shape reduces to a rectangle
				frame.AddPoint(left, top);
				frame.AddPoint(right, top);
				frame.AddPoint(right, bottom);
				frame.AddPoint(left, bottom);
				return;
			}
			// outside edge
			frame.AddPoint(left, top);
			frame.AddPoint(right, top);
			frame.AddPoint(right, bottom);
			frame.AddPoint(left, bottom);
			frame.AddPoint(left, top + thickness);
			// and inside edge
			frame.AddPoint(left + thickness, top + thickness);
			frame.AddPoint(left + thickness, bottom - thickness);
			frame.AddPoint(right - thickness, bottom - thickness);
			frame.AddPoint(right - thickness, top + thickness);
			frame.AddPoint(left, top + thickness);
		}

		void OnLayoutUpdated(object sender, Native.LayoutEventArgs e)
		{
			UpdateGeometry();
		}

		void UpdateGeometry()
		{
			var geometry = NativeView.Geometry;
			if (HasBackgroundColor)
			{
				_frame.Draw(NativeView.Geometry);
			}
			else
			{
				DrawFrame(_frame,
				geometry.X,
				geometry.Y,
				geometry.Right - _thickness,
				geometry.Bottom - _thickness,
				_thickness
			);
				DrawFrame(_shadow,
					geometry.X,
					geometry.Y,
					geometry.Right,
					geometry.Bottom,
					_thickness
				);
			}
			
		}

		void UpdateColor()
		{
			if (HasBackgroundColor)
				return;

			if (FrameElement.BorderColor.IsDefault)
				_frame.Color = s_DefaultColor;
			else
				_frame.Color = FrameElement.BorderColor.ToNative();
		}

		void UpdateShadowVisibility()
		{
			if (HasBackgroundColor)
				return;

			if (FrameElement.HasShadow)
				_shadow.Show();
			else
				_shadow.Hide();
		}

		void UpdateCornerRadius()
		{
			if (!HasBackgroundColor)
				return;

			int radius = 0;
			if (FrameElement.CornerRadius != -1f)
			{
				radius = Forms.ConvertToScaledPixel(FrameElement.CornerRadius);
            }
            _frame.SetRadius(radius);
            _frame.Draw();
        }
	}
}
