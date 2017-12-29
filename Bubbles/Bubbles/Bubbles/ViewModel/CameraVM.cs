using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bubbles.ViewModel
{
    public class CameraVM : INotifyPropertyChanged
    {
        private string photoImage { get; set; }
        public string PhotoImage
        {


            get { return photoImage; }
            set
            {
                photoImage = value;
                OnPropertyChanged();

            }
        }


        public ICommand Click => (new Command(async () => await displayCommand()));
        private async Task displayCommand()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
            { 
                PhotoImage = photo.Path;
                OnPropertyChanged("PhotoImage");
            }
            else { }
        
    }
        public event PropertyChangedEventHandler PropertyChanged;



        public void OnPropertyChanged([CallerMemberName]string name = "") =>

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

