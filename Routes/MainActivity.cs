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
using System.Collections.Generic;
using Square.Picasso;

namespace Routes
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button loadImageBtn, searchImageBtn;
        private ImageView imgView;
        //private TextView pictureInfo;
        private string imagePath;
        private List<Painting> all_paintings = ContentLoader.load();

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
                ImageView imgView = (ImageView)FindViewById(Resource.Id.imageView1);
                imgView.SetImageURI(data.Data);
                //Picasso.With(Application.Context).Load("http://i.imgur.com/DvpvklR.png").Into(imgView);
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
            JToken tmp = JToken.Parse(resp);
            SetContentView(Resource.Layout.paint_info);

            TextView pictureName = (TextView)FindViewById(Resource.Id.textView56);
            TextView pictureInfo = (TextView)FindViewById(Resource.Id.textView57);
            ImageView img_view = (ImageView)FindViewById(Resource.Id.imageView57);
            List<string> taglist = new List<string>(tmp["description"]["tags"].ToObject<List<string>>());
            Painting pic = findBestPaint(taglist);
            pictureName.Text = pic.name + " - " + pic.author;
            pictureInfo.Text = pic.info;
            //Picasso.With(Application.Context).Load(Android.Net.Uri.Parse(path)).Into(img_view);
            //img_view.SetImageBitmap(pic.GetImageBitmap());
            //img_view.SetImageURI(Android.Net.Uri.Parse(path));
        }
        private byte[] GetBytesFromImage(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                BinaryReader reader = new BinaryReader(stream);
                return reader.ReadBytes((int)stream.Length);
            }
        }
        private Painting findBestPaint(List<string> tags)
        {
            int bestcount = -1;
            Painting bestp = all_paintings[0];
            foreach (Painting p in all_paintings)
            {
                int tmp = p.countTags(tags);
                if (tmp > bestcount)
                {
                    bestcount = tmp;
                    bestp = p;
                }
            }
            return bestp;
        }
    }
}