using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using NewCapstone.ORM;
using System.Threading;
using Android.Views.InputMethods;

namespace NewCapstone
{
    [Activity(Label = "Nest Roommate App", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //g used for global variablees
        private RelativeLayout gRelativeLayout;
        private RelativeLayout gFragmentRelativeLayout;
        private Button gBtnSignIn;
        private Button gBtnSignUp;
        private ProgressBar gprogressBar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource or the first landing page
            SetContentView (Resource.Layout.Main);

            gBtnSignIn = FindViewById<Button>(Resource.Id.buttonSignIn);
            gBtnSignUp = FindViewById<Button>(Resource.Id.buttonSignUp);
            gprogressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            gRelativeLayout = FindViewById<RelativeLayout>(Resource.Id.mainRelativeLayout);
            gFragmentRelativeLayout = FindViewById<RelativeLayout>(Resource.Id.fragmentRelativeLayout);


            gprogressBar.Visibility = ViewStates.Invisible;

            //testing get data
            gBtnSignUp.Click += (object sender, EventArgs e) => //when Sign Up button is clicked
            {
                //Bring up dialog box
                FragmentTransaction transaction = FragmentManager.BeginTransaction(); //begin to pull dialog from activity
                fragment_SignUp fragmentDialog = new fragment_SignUp();
                fragmentDialog.Show(transaction, "dialog"); //shows dialog

                fragmentDialog.gOnSignUpComplete += fragment_SignUp_gOnSignUpComplete;
            };

            gBtnSignIn.Click += (object sender, EventArgs e) =>
            {
                Intent intent = new Intent(this, typeof(Home));
                this.StartActivity(intent);
                this.Finish(); //kill login screen after user logs in
                /*
                DBRepository dbr = new DBRepository();
                var result = dbr.GetRecords();
                Toast.MakeText(this, result, ToastLength.Short).Show();
                */
            };
            
        }

        void fragment_SignUp_gOnSignUpComplete(object sender, OnSignUpEventArgs e) // sign up button in fragment
        {
            gprogressBar.Visibility = ViewStates.Visible;

            //calls to database
            DBRepository dbr = new DBRepository();
            var dbCreate = dbr.CreateDB();
            var tableCreate = dbr.CreateTable();

            if (e.Email.Contains(".edu")) // workaround for actual educational authentication
            {
                string recordInsert = dbr.InsertRecord(e.Email);
                Toast.MakeText(this, recordInsert, ToastLength.Short).Show(); //record checker (working)
            }
            else
            Toast.MakeText(this, dbCreate, ToastLength.Short).Show(); //database checker (working)
            //Toast.MakeText(this, tableCreate, ToastLength.Short).Show(); //table checker (working)

        }

        void gRelativeLayout_Click(object sender, EventArgs e) //exits any menu when you click outside of a textbox in Main
        {
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Activity.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }

        void gFragmentRelativeLayout_Click(object sender, EventArgs e) //exits any menu when you click outside of a textbox in signup fragment
        {
            InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Activity.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.None);
        }

    }
}

