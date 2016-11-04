using Foundation;
using System;
using System.ComponentModel;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Messaging;
using MasterDetail.Core.ViewModel;
using MasterDetail.Core.ViewModel.Helper;
using UIKit;

namespace MasterDetail.iOS
{
    public partial class PersonViewController : UIViewController, INotifyPropertyChanged
    {
        private bool editMode = false;
        private ViewMode _viewMode;
        private Binding<string, string> _nameLabelBinding;
        private Binding<ViewMode, ViewMode> _viewModeBinding;

        public PersonViewController (IntPtr handle) : base (handle)
        {
            Messenger.Default.Register<PropertyChangedMessage<ViewMode>>(this, ChangeContainer);
        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Vm.Init();

            // bindings
            _nameLabelBinding = this.SetBinding(
                () => Vm.CurrentPerson.Name,
                () => persName.Text);
            _viewModeBinding = this.SetBinding(
                () => Vm.CurrentViewMode,
                () => CurrentViewMode);
            editButton.SetCommand(Vm.EditTogglePersonCommand);
            deleteButton.SetCommand(Vm.RemovePersonCommand);

            SetupConatiner();
        }

        private void SetupConatiner()
        {
            if (CurrentViewMode == ViewMode.Show)
            {
                ContainerEdit.Hidden = true;
                ContainerShow.Hidden = false;
            }
            else
            {
                ContainerShow.Hidden = true;
                ContainerEdit.Hidden = false;
            }
            SetEditButtonTitle();
        }

        private void ToggleEditingContainer()
        {
            if (CurrentViewMode == ViewMode.Show)
            {
                ContainerEdit.Hidden = true;
                ContainerShow.Hidden = false;
            }
            else
            {
                ContainerShow.Hidden = true;
                ContainerEdit.Hidden = false;
                // TODO: as ViewMode changes, execute SacePersonCommand in VM
                //Vm.SavePersonCommand.Execute("");
            }
            SetEditButtonTitle();
        }

        public ViewMode CurrentViewMode
        {
            get { return _viewMode; }
            set
            {
                if (_viewMode != value)
                {
                    /*OnPropertyChanged("CurrentViewMode");*/
                    _viewMode = value;
                }
            }
        }

        private void SetEditButtonTitle()
        {
            if (CurrentViewMode != ViewMode.Show)
            {
                editButton.SetTitle("Save", UIControlState.Normal);
            }
            else
            {
                editButton.SetTitle("Edit", UIControlState.Normal);
            }
        }
        private void ChangeContainer(PropertyChangedMessage<ViewMode> obj)
        {
            var newViewMode = obj.NewValue;
            CurrentViewMode = newViewMode;
            ToggleEditingContainer();
        }


        #region PropertyChanged functions
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
        #endregion
        private PersonViewModel Vm => Application.Locator.DetailVm;
    }
}