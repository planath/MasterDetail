using Foundation;
using GalaSoft.MvvmLight.Helpers;
using MasterDetail.Core.Model;
using UIKit;

namespace MasterDetail.iOS
{
    public class PeopleListObservableTableSource : ObservableTableViewSource<Person>
    {
        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override void CommitEditingStyle(UITableView tableView,
                                                UITableViewCellEditingStyle editingStyle,
                                                NSIndexPath indexPath)
        {
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    // remove the item from the underlying data source
                    DataSource.RemoveAt(indexPath.Row);
                    // No need to delete the row from the table as the tableview is bound to the data source
                    break;
            }
        }

        public override UITableViewCellEditingStyle EditingStyleForRow(UITableView tableView,
                                                                        NSIndexPath indexPath)
        {
            return UITableViewCellEditingStyle.Delete;
        }

    }
}