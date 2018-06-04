using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UserWindow
{
    public static class WebBrowserUtility
    {
        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserUtility), new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            if (browser != null)
            {
                string uri = e.NewValue as string;
                browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
            }
        }


        public static void HideScrollBar(this WebBrowser browser, bool isHidden)
        {
            if (browser != null)
            {
                IHTMLDocument2 document = browser.Document as IHTMLDocument2;
                if (document == null)
                {
                    // If too early
                    browser.LoadCompleted += (o, e) => HideScrollBar(browser, isHidden);
                    return;
                }

                //string bodyOverflow = string.Format("document.body.style.overflow='{0}';", isHidden ? "hidden" : "auto");
                //document.parentWindow.execScript(bodyOverflow); // This does not work for me...

                string elementOverflow = string.Format("document.documentElement.style.overflow='{0}';", isHidden ? "hidden" : "auto");
                document.parentWindow.execScript(elementOverflow);
            }
        }

    }
}
