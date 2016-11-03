using Foundation;
using System;
using GalaSoft.MvvmLight.Helpers;
using MasterDetail.Core.Model;
using MasterDetail.Core.ViewModel.mvvmlight.Core.ViewModel;
using UIKit;

namespace MasterDetail.iOS
{
    public partial class PeopleTableViewController : UITableViewController
    {
        public PeopleTableViewController (IntPtr handle) : base (handle)
        {
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Vm.Init();

            AddButton = new UIBarButtonItem(UIBarButtonSystemItem.Add);
            NavigationItem.SetRightBarButtonItem(AddButton, false);
            AddButton.Clicked += (sender, args) =>
            {
                Vm.SelectedPerson = Vm.People[2];
            };
            //AddButton.SetCommand("Clicked", Vm.AddPersonCommand);

            
            source = Vm.People.GetTableViewSource(
                 CreateTaskCell,
                 BindTaskCell,
                 factory: () => new PeopleListObservableTableSource());

            PeopleTableView.Source = source;

            //PeopleTableView.RowHeight = UITableView.AutomaticDimension;
            //PeopleTableView.EstimatedRowHeight = 4f;
            PeopleTableView.RowHeight = 185;
            PeopleTableView.ReloadData();
        }
        public UIBarButtonItem AddButton {  get; private set; }

        private void BindTaskCell(UITableViewCell cell, Person person, NSIndexPath path)
        {
            var personCell = cell as PersonTableViewCell;
            personCell.Email = person.Email;
            personCell.Name = person.Name;
            personCell.Image = $"Images{path.Row % 3}";
        }

        private UITableViewCell CreateTaskCell(NSString cellIdentifier)
        {
            var cell = PeopleTableView.DequeueReusableCell("PersonCellIdentifier") as PersonTableViewCell;
            if (cell == null)
            {
                cell = new PersonTableViewCell((NSString)"PersonCellIdentifier");
            }

            return cell;
        }

        private ObservableTableViewSource<Person> source;
        private PeopleViewModel Vm => Application.Locator.MainVm;
    }
}