using Bubbles.Services;
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
        private string words { get; set; }
        public string word
        {


            get { return words; }
            set
            {
                words = value;
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
                //await Navigation.PushAsync(new SpeakVM(PhotoImage));
                Models.ImageResult res = await VisionAPIService.MakeAnalysisRequest(photo.Path);
                //double max = 0;
                //int i = 0;
                //if(res!=null)
                //{
                //    foreach(var x in res.Description.Captions)
                //    {
                //        if (x.Confidence > max)
                //        {
                //            max = x.Confidence;
                //            word = res.Description.Tags[i];
                            
                //        }
                //        i++;
                        
                //    }
                //}
                //var con=res.Description.Tags.
                word = res.Description.Tags[0];

            }
            else { }
        
    }
        public event PropertyChangedEventHandler PropertyChanged;



        public void OnPropertyChanged([CallerMemberName]string name = "") =>

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

