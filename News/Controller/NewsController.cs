using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Collections.Generic;
using System.Net;
using System.Json;
using System.IO;

namespace News
{
	public partial class NewsController : UIViewController
	{
		string _category;
		public NewsController (string category) : base ("NewsController", null)
		{
			Title = "News";
			_category = category;
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
			var list = new List<NewsModel>();
			var table = new UITableView (new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height));
			View.Add (table);

			try{
				var request = WebRequest.Create("http://api.bbcnews.appengine.co.uk/stories/"+_category);
				var response = request.GetResponse ();
				using(var stream = new StreamReader(response.GetResponseStream())){
					var json = stream.ReadToEnd ();
					var jsonVal = JsonValue.Parse (json);				
					for(var i=0; i<jsonVal["stories"].Count; i++){
						list.Add (new NewsModel () {
							Title = jsonVal["stories"][i]["title"],
							Description = jsonVal["stories"][i]["description"],
							Thumbnail = jsonVal["stories"][i]["thumbnail"],
							Link = jsonVal["stories"][i]["link"]
						});
					}				
				};
				response.Close ();
			}catch{
			}
			table.Source = new NewsTableSource(list.ToArray(), NavigationController);
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

