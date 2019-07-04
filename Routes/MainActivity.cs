using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Support.V4.App;
using Android;
using Android.Content;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Routes
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button loadImageBtn, searchImageBtn;
        private ImageView imgView;
        private TextView pictureInfo;
        private string imagePath;

        public const string key = "3fa1f65516424aefa6af2a2abaf63308";
        public readonly HttpClient client = new HttpClient
        {
            DefaultRequestHeaders = { { "Ocp-Apim-Subscription-Key", key } }
        };
        public string baseurl = "https://westcentralus.api.cognitive.microsoft.com/vision/v2.0/analyze";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.search);

            loadImageBtn = (Button)FindViewById(Resource.Id.button4);
            searchImageBtn = (Button)FindViewById(Resource.Id.button5);
            imgView = (ImageView)FindViewById(Resource.Id.imageView1);
            pictureInfo = (TextView)FindViewById(Resource.Id.textView57);
            loadImageBtn.Click += delegate {

                Intent image = new Intent();
                image.SetType("image/*");
                image.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(image, "choose"), 57);
            };
            searchImageBtn.Click += delegate
            {
                Analyze(imagePath);
            };
            ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, 57);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == Result.Ok)
            {
                string path = ActualPath.GetActualPathFromFile(data.Data, this);
                imgView.SetImageURI(data.Data);
                imagePath = path;
            }
        }
        async Task Analyze(string path)
        {
            string param = "visualFeatures=Description";
            string url = baseurl + "?" + param;
            HttpResponseMessage response;
            byte[] img = GetBytesFromImage(path);
            using (ByteArrayContent content = new ByteArrayContent(img))
            {
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(url, content);
            }
            string resp = await response.Content.ReadAsStringAsync();
            var tmp = JToken.Parse(resp).ToString();
            SetContentView(Resource.Layout.paint_info);
            pictureInfo = (TextView)FindViewById(Resource.Id.textView57);
            pictureInfo.Text = tmp;
            //JToken data = JToken.Parse(resp);
            //string tmp = data["description"]["captions"][0]["text"].ToString();
            //text.Text = tmp;
        }
        private byte[] GetBytesFromImage(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(stream);
                return reader.ReadBytes((int)stream.Length);
            }
        }

    }
}