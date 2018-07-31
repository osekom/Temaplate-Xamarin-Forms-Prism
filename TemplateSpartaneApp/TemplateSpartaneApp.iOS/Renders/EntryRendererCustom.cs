using System;
using System.ComponentModel;
using CoreGraphics;
using TemplateSpartaneApp.Controls;
using TemplateSpartaneApp.iOS.Renders;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryControl), typeof(EntryRendererCustom))]
namespace TemplateSpartaneApp.iOS.Renders
{
    public class EntryRendererCustom : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var view = (EntryControl)Element;
                Control.LeftView = new UIView(new CGRect(0f, 0f, 9f, 20f));

                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.KeyboardAppearance = UIKeyboardAppearance.Dark;
                Control.ReturnKeyType = UIReturnKeyType.Done;
                // Radius for the curves  
                Control.Layer.CornerRadius = Convert.ToSingle(view.CornerRadius);
                // Thickness of the Border Color  
                Control.Layer.BorderColor = view.BorderColor.ToCGColor();
                // Thickness of the Border Width  
                Control.Layer.BorderWidth = view.BorderWidth;
                Control.ClipsToBounds = true;
                if (view != null)
                {
                    SetIcon(view);

                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var view = (EntryControl)Element;
            if (e.PropertyName == EntryControl.IconProperty.PropertyName)
            {
                SetIcon(view);
            }
        }

        private void SetIcon(EntryControl view)
        {
            if (!string.IsNullOrEmpty(view.Icon))
            {

                Control.LeftViewMode = UITextFieldViewMode.Always;
                Control.LeftViewRect(new CGRect(5, 5, 5, 5));
                Control.LeftView = new UIImageView(UIImage.FromBundle(view.Icon));
            }
            else
            {
                Control.LeftViewMode = UITextFieldViewMode.Never;
                Control.LeftView = null;
            }
        }
    }
}
