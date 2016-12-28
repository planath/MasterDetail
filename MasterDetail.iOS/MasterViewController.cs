using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CoreGraphics;
using Foundation;
using GalaSoft.MvvmLight.Helpers;
using MasterDetail.Core.Model;
using MasterDetail.Core.ViewModel.mvvmlight.Core.ViewModel;
using UIKit;

namespace MasterDetail.iOS
{
    public partial class MasterViewController : UITableViewController
    {
        DataSource dataSource;

        public MasterViewController(IntPtr handle) : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            NavigationItem.LeftBarButtonItem = EditButtonItem;

            var addButton = new UIBarButtonItem(UIBarButtonSystemItem.Add, AddNewItem);
            addButton.AccessibilityLabel = "addButton";
            NavigationItem.RightBarButtonItem = addButton;

            TableView.Source = dataSource = new DataSource(this);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void AddNewItem(object sender, EventArgs args)
        {
            var firstName = RandomName(7);
            var lastName = RandomName(5);
            var email = RandomName(5).ToLower() + "@" + RandomName(3).ToLower() + ".ch";
            var birthday = DateTime.Now.AddYears(-24);
            var person = new Person(firstName, lastName, email, birthday);
            dataSource.Objects.Add(person);

            using (var indexPath = NSIndexPath.FromRowSection(0, 0))
                TableView.InsertRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showDetail")
            {
                var indexPath = TableView.IndexPathForSelectedRow;
                var item = dataSource.Objects[indexPath.Row];

                ((DetailViewController)segue.DestinationViewController).SetDetailItem(item);
            }
        }

        private string RandomName(int length)
        {
            var rand = new Random();
            char bigLetter = (char)('A' + rand.Next(26));
            var smallLetters = string.Concat(Enumerable.Range(0, length - 1).Select(i => ((char)('a' + rand.Next(0, 26)))).ToList());

            var name = bigLetter + smallLetters;
            return name;
        }

        class DataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("Cell");
            private ObservableCollection<Person> objects;
            private readonly List<Binding> bindings = new List<Binding>();
            readonly MasterViewController controller;

            public DataSource(MasterViewController controller)
            {
                this.controller = controller;
                Vm.Init();
                Objects = Vm.People;
            }

            public ObservableCollection<Person> Objects
            {
                get { return objects; }
                set { objects = value; }
            }

            // Customize the number of sections in the table view.
            public override nint NumberOfSections(UITableView tableView)
            {
                return 1;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return objects.Count;
            }

            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

                //bindings.Add(
                //    this.SetBinding(
                //      () => Vm.People[indexPath.Row].Name,
                //      () => cell.TextLabel.Text));
                //bindings.Add(
                //    this.SetBinding(
                //      () => Vm.People[indexPath.Row].Email,
                //      () => cell.DetailTextLabel.Text));

                cell.TextLabel.Text = Objects[indexPath.Row].Name ?? "";
                cell.DetailTextLabel.Text = Objects[indexPath.Row].Email ?? "";

                return cell;
            }

            public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
            {
                // Return false if you do not want the specified item to be editable.
                return true;
            }

            public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
            {
                if (editingStyle == UITableViewCellEditingStyle.Delete)
                {
                    // Delete the row from the data source.
                    objects.RemoveAt(indexPath.Row);
                    controller.TableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Fade);
                }
                else if (editingStyle == UITableViewCellEditingStyle.Insert)
                {
                    // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
                }
            }
            private PeopleViewModel Vm => Application.Locator.MainVm;
        }
    }
}

