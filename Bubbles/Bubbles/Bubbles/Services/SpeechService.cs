using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Cognitive.BingSpeech;
using Xamarin.Forms;
using Plugin.AudioRecorder;
using Newtonsoft.Json;

namespace Bubbles.Services
{
    public class SpeechService
    {
        public static readonly string SpeechRecognitionEndpoint = "https://speech.platform.bing.com/recognize";
        public static readonly string AuthenticationTokenEndpoint = "https://api.cognitive.microsoft.com/sts/v1.0";

        public static readonly string API_KEY = "fced1b7790b248a3800a434aa0ae4b25";
        public static string Token = String.Empty;

        async static Task<string> FetchTokenAsync(string fetchUri, string apiKey)
        {
            if (Token != String.Empty)
            {
                return Token;
            }
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey);
                UriBuilder uriBuilder = new UriBuilder(fetchUri);
                uriBuilder.Path += "/issueToken";

                var result = await client.PostAsync(uriBuilder.Uri.AbsoluteUri, null);
                string token = await result.Content.ReadAsStringAsync();
                Token = token;

                return token;
            }
        }

        async static Task<string> SendRequestAsync(Stream fileStream, string url, string bearerToken, string contentType)
        {
            var content = new StreamContent(fileStream);
            content.Headers.TryAddWithoutValidation("Content-Type", contentType);

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                var response = await httpClient.PostAsync(url, content);

                return await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task<string> RecognizeSpeechAsync(string filename)
        {
            // Read audio file to a stream
            var file = await PCLStorage.FileSystem.Current.LocalStorage.GetFileAsync(filename);
            var fileStream = await file.OpenAsync(PCLStorage.FileAccess.Read);

            string requestUri = GenerateRequestUri(SpeechRecognitionEndpoint);
            string accessToken = await FetchTokenAsync(AuthenticationTokenEndpoint, API_KEY);
            var response = await SendRequestAsync(fileStream, requestUri, accessToken, @"audio/wav; codec=""audio/pcm""; samplerate=16000");

            fileStream.Dispose();
            var sr = Models.SpeechResult.FromJson(response);

            if (sr.Header.Status == "success")
            {
                return sr.Header.Name;
            }
            else
            {
                return null;
            }
        }

        public static string GenerateRequestUri(string speechEndpoint)
        {
            string requestUri = speechEndpoint;
            requestUri += @"?scenarios=ulm";                                    // websearch is the other option
            requestUri += @"&appid=D4D52672-91D7-4C74-8AD8-42B1D98141A5";       // You must use this ID
            requestUri += @"&locale=en-US";                                     // Other languages supported
            requestUri += string.Format("&device.os={0}", Device.OS.ToString());     // Open field
            requestUri += @"&version=3.0";                                      // Required value
            requestUri += @"&format=json";                                      // Required value
            requestUri += @"&instanceid=fe34a4de-7927-4e24-be60-f0629ce1d808";  // GUID for device making the request
            requestUri += @"&requestid=" + Guid.NewGuid().ToString();           // GUID for the request
            return requestUri;
        }
    }
}





