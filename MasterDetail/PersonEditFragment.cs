using Android.App;
using Android.OS;
using Android.Views;
namespace MasterDetail
{
    public class PersonEditFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.PersonEditFragment, container, false);
            return view;
        }
    }
}