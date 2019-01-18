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
	public partial class CategoryController : UIViewController
	{
		public CategoryController () : base ("CategoryController", null)
		{
			Title = "BBC News";
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
			var list = new List<CategoryModel>();
			var table = new UITableView (new RectangleF (0, 0, View.Bounds.Width, View.Bounds.Height));
			View.Add (table);

			try{
				var request = WebRequest.Create("http://api.bbcnews.appengine.co.uk/topics");
				var response = request.GetResponse ();
				using(var stream = new StreamReader(response.GetResponseStream())){
					var json = stream.ReadToEnd ();
					var jsonVal = JsonValue.Parse (json);				
					for(var i=0; i<jsonVal["topics"].Count; i++){
						list.Add (new CategoryModel () {
							Id = jsonVal["topics"][i]["id"],
							Title = jsonVal["topics"][i]["title"]
						});
					}				
				};
				response.Close ();
			}catch{
			}
			table.Source = new CategoryTableSource(list.ToArray(), NavigationController);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
	}
}

