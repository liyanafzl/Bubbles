using Bubbles.View;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bubbles.ViewModel
{
   public class FirstPageVM
    {
        ICommand _PlayCommand;
        private INavigation _Navigation { get; set; }

        public FirstPageVM(INavigation navigation)
        {
            this._Navigation = navigation;
            _PlayCommand = new Command(OnTapped);
        }
        public ICommand PlayCommand
        {
            get { return _PlayCommand; }
        }
        async void OnTapped()
        {
            await _Navigation.PushModalAsync(new CameraPage());

        }

    }
}
