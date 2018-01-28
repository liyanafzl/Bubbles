using Bubbles.Services;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bubbles.ViewModel
{
    public class CameraPageVM
    {
        ICommand _CameraCommand;
        private INavigation _Navigation { get; set; }

        public CameraPageVM(INavigation navigation)
        {
            this._Navigation = navigation;
            _CameraCommand = new Command(OnTapped);
        }
        public ICommand CameraCommand
        {
            get { return _CameraCommand; }
        }
        async void OnTapped()
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { PhotoSize = PhotoSize.Medium, CompressionQuality = 60 });
            Models.ImageResult res = await VisionAPIService.MakeAnalysisRequest(photo.Path);
            string word = res.Description.Tags[0];
            await _Navigation.PushModalAsync(new MainPage(photo.Path,word));

        }
    }
}
