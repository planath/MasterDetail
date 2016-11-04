using Foundation;
using System;
using System.ComponentModel;
using GalaSoft.MvvmLight.Helpers;
using MasterDetail.Core.Model;
using MasterDetail.Core.ViewModel.mvvmlight.Core.ViewModel;
using UIKit;

namespace MasterDetail.iOS
{
    public partial class PeopleTableViewController : UITableViewController, INotifyPropertyChanged
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
            AddButton.Clicked += (sender, args) => { };

            // bindings
            AddButton.SetCommand("Clicked", Vm.AddPersonCommand);
            _currentIndexBinding = this.SetBinding(
                () => CurrentIndex,
                () => Vm.CurrentIndex);

            source = Vm.People.GetTableViewSource(
                 CreateTaskCell,
                 BindTaskCell,
                 factory: () => new PeopleListObservableTableSource());

            PeopleTableView.Source = source;
            PeopleTableView.Delegate = this;
            PeopleTableView.RowHeight = 185;
            PeopleTableView.ReloadData();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            Vm.Init();
        }

        private int _currentIndex;
        public int CurrentIndex
        {
            get
            {
                return _currentIndex;
            }
            set
            {
                _currentIndex = value;
                OnPropertyChanged("CurrentIndex");
            }
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

        public override NSIndexPath WillSelectRow(UITableView tableView, NSIndexPath indexPath)
        {
            CurrentIndex = indexPath.Row;
            return indexPath;
        }
        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableTableViewSource<Person> source;
        private Binding<int, int> _currentIndexBinding;
        private PeopleViewModel Vm => Application.Locator.MainVm;
    }
}