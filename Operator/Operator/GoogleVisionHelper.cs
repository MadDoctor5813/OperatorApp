using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using System.Net;
using Newtonsoft.Json;

namespace Operator
{
    class GoogleVisionHelper
    {

        private static readonly string baseUrl = "https://vision.googleapis.com/v1/images:annotate?key=AIzaSyB-Hs5-b5YxaRTTVw9tVJUfzgJ2CZ-i0FM";
        private static WebClient webClient = new WebClient();

        private static readonly int MaxResults = 5;

        public static IList<string> GetImageLabels(byte[] data)
        {
            //convert data to base64
            string base64Data = Convert.ToBase64String(data);
            VisionAPIRequest apiRequest = new VisionAPIRequest();
            apiRequest.requests = new List<Request>();
            Request request = new Request();
            request.features = new List<Feature>();
            Feature feature = new Feature();
            Image image = new Image();
            image.content = base64Data;
            feature.type = "LABEL_DETECTION";
            feature.maxResults = MaxResults;
            request.features.Add(feature);
            request.image = image;
            apiRequest.requests.Add(request);
            string requestJSONStr = JsonConvert.SerializeObject(apiRequest);
            byte[] requestJSON = Encoding.UTF8.GetBytes(requestJSONStr);
            byte[] responseData = webClient.UploadData(baseUrl, requestJSON);
            VisionAPIResponse response = JsonConvert.DeserializeObject<VisionAPIResponse>(Encoding.UTF8.GetString(responseData));
            IList<string> labels = new List<string>(response.responses[0].labelAnnotations.Count);
            for (int i =  0; i < response.responses[0].labelAnnotations.Count; i++)
            {
                labels.Add(response.responses[0].labelAnnotations[i].description);
            }
            return labels;
        }

        public class Image
        {
            public string content { get; set; }
        }

        public class Feature
        {
            public string type { get; set; }
            public int maxResults { get; set; }
        }

        public class Request
        {
            public Image image { get; set; }
            public List<Feature> features { get; set; }
        }

        public class VisionAPIRequest
        {
            public List<Request> requests { get; set; }
        }

        public class LabelAnnotation
        {
            public string mid { get; set; }
            public string description { get; set; }
            public double score { get; set; }
        }

        public class Response
        {
            public List<LabelAnnotation> labelAnnotations { get; set; }
        }

        public class VisionAPIResponse
        {
            public List<Response> responses { get; set; }
        }

    }
}