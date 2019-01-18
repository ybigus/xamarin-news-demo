using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace News
{
	public class CategoryTableSource: UITableViewSource
	{
		CategoryModel[] _category;
		UINavigationController _controller;
		public CategoryTableSource (CategoryModel[] category, UINavigationController controller)
		{
			_category = category;
			_controller = controller;
		}

		public override int RowsInSection (UITableView tableview, int section)
		{
			return _category.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell("TableCell");
			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, "TableCell");
			}

			cell.TextLabel.Text = _category [indexPath.Row].Title;
			cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
			return cell;
		}

		/*public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 50;
		}*/

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			_controller.PushViewController (new NewsController (_category [indexPath.Row].Id), true);
			tableView.DeselectRow(indexPath,false);
		}
	}
}

