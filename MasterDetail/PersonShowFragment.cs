using Android.App;
using Android.OS;
using Android.Views;
namespace MasterDetail
{
    public class PersonShowFragment : Fragment
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.PersonShowFragment, container, false);
            return view;
        }
    }
}