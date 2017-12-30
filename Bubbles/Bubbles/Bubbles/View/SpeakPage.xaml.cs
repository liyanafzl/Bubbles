using Bubbles.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bubbles.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SpeakPage : ContentPage
	{
      //  public string path;

        public SpeakPage ()
		{
			InitializeComponent ();
           // BindingContext = new SpeakVM(path);
        }
	}
}