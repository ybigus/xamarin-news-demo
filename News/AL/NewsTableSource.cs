using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace News
{
	public class NewsTableSource:UITableViewSource
	{
		NewsModel[] _news;
		UINavigationController _controller;
		public NewsTableSource (NewsModel[] news, UINavigationController controller)
		{
			_news = news;
			_controller = controller;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _news.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell("TableCell");
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, "TableCell");
			}

			cell.TextLabel.Text = _news [indexPath.Row].Title;
			try{
				cell.ImageView.Image = UIImage.LoadFromData (NSData.FromUrl (new NSUrl(_news [indexPath.Row].Thumbnail)));
			}catch{
			}
			cell.DetailTextLabel.Text = _news [indexPath.Row].Description;
			cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
			return cell;
		}

		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 50;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			_controller.PushViewController (new DetailsController (_news [indexPath.Row].Link), true);
			tableView.DeselectRow(indexPath,false);
		}

	}
}

