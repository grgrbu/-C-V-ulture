using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

namespace App
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Button buttonSignUp;
        private Button buttonLogIn;

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.registration);

            buttonSignUp = FindViewById<Button>(Resource.Id.buttonSignUp); //открытие окна зарег

            buttonSignUp.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                reg_dialogwindow reg_Dialogwindow = new reg_dialogwindow();
                reg_Dialogwindow.Show(transaction, "dialog fragment");
            };

            buttonLogIn = FindViewById<Button>(Resource.Id.buttonSignUp); //открытие окна войти

            buttonLogIn.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_signin dialog_Signin = new dialog_signin();
                dialog_Signin.Show(transaction, "dialog fragment");
            };

            Button buttonSignIn = (Button)FindViewById(Resource.Id.buttonFilters); //войти
            buttonSignIn.Click += delegate
            {
                SetContentView(Resource.Layout.profile); 
            };

            Button button_SignUp = (Button)FindViewById(Resource.Id.buttonFilters); //зарег
            button_SignUp.Click += delegate
            {
                SetContentView(Resource.Layout.profile); 
            };

            Button buttonCreateRoute = (Button)FindViewById(Resource.Id.buttonFilters); //открыть создать маршрут
            buttonCreateRoute.Click += delegate
            {
                SetContentView(Resource.Layout.create);
            };

            Button buttonMyRoutes = (Button)FindViewById(Resource.Id.buttonFilters); //открыть мои маршруты
            buttonMyRoutes.Click += delegate
            {
                SetContentView(Resource.Layout.my_routes);
            };

            Button buttonPopular = (Button)FindViewById(Resource.Id.buttonFilters); //открыть рек маршруты
            buttonPopular.Click += delegate
            {
                SetContentView(Resource.Layout.popular);
            };

            Button buttonProfile = (Button)FindViewById(Resource.Id.buttonFilters); //открыть профиль
            buttonProfile.Click += delegate
            {
                SetContentView(Resource.Layout.profile);
            };

            Button filterButton = (Button)FindViewById(Resource.Id.buttonFilters); //открыть страницу фильтров
            filterButton.Click += delegate
            {
                SetContentView(Resource.Layout.filter);
            };

            Button btnAdd = (Button)FindViewById(Resource.Id.buttonFilters); //применить фильтр
            btnAdd.Click += delegate
            {
                SetContentView(Resource.Layout.create);
            };

            Button buttonCreate = (Button)FindViewById(Resource.Id.buttonFilters); //создать новый маршрут
            buttonCreate.Click += delegate
            {
                SetContentView(Resource.Layout.after_search);
            };

            Button btnInfo_Cla = (Button)FindViewById(Resource.Id.buttonFilters); //информация о маршруте классицизм
            btnInfo_Cla.Click += delegate
            {
                SetContentView(Resource.Layout.route_klassicism);
            };

            Button btnInfo_First = (Button)FindViewById(Resource.Id.buttonFilters); //информация о обзорном маршруте 
            btnInfo_First.Click += delegate
            {
                SetContentView(Resource.Layout.route_first);
            };

            Button btnInfo_Rom = (Button)FindViewById(Resource.Id.buttonFilters); //информация о маршруте романтизм
            btnInfo_Rom.Click += delegate
            {
                SetContentView(Resource.Layout.route_romantism);
            };

            Button button_Search = (Button)FindViewById(Resource.Id.buttonFilters); //поиск
            button_Search.Click += delegate
            {
                SetContentView(Resource.Layout.search);
            };
        }
    }
}