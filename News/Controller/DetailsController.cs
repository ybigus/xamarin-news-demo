using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace News
{
	public partial class DetailsController : UIViewController
	{
		string _url;
		public DetailsController (string url) : base ("DetailsController", null)
		{
			Title = "Details";
			_url = url;
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{

			base.ViewDidLoad ();
			var webView = new UIWebView (new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height));
			View.Add (webView);
			webView.LoadRequest (new NSUrlRequest (new NSUrl(_url)));
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

