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
    public class SignInScreen : Activity
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


            gprogressBar.Visibility = ViewStates.Invisible; // progress bar invisible when nothing happening

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
                //bring up dialog box
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                fragment_SignIn fragmentDialog = new fragment_SignIn();
                fragmentDialog.Show(transaction, "dialog");

                fragmentDialog.gOnSignInComplete += fragment_SignIn_gOnSignInComplete;

                /*
                
                */
            };
            
        }

        /* Sign In / Sign Up Complete Functions */

        void fragment_SignIn_gOnSignInComplete(object sender, OnSignInEventArgs e) // sign in button in fragment
        {
            DBRepository dbr = new DBRepository();
             
            if(dbr.LoginCheck(e.SignInEmail, e.SignInPassword) == 0)
            {
                Toast.MakeText(this, "Sign in Complete! \nEmail is" + e.SignInEmail + ", pass is " + e.SignInPassword, ToastLength.Short).Show();
                Intent intent = new Intent(this, typeof(Home));
                this.StartActivity(intent);
                this.Finish(); //kill login screen after user logs in
            }
            else
                Toast.MakeText(this, "Email or password incorrect. Could not log in.", ToastLength.Long).Show();
        }

        void fragment_SignUp_gOnSignUpComplete(object sender, OnSignUpEventArgs e) // sign up button in fragment
        {

            //calls to database
            DBRepository dbr = new DBRepository();
            var dbCreate = dbr.CreateDB();
            var tableCreate = dbr.CreateTable();

            if (e.Email.Contains(".edu") && (e.Password.Length >= 6)) // workaround for actual educational authentication
            {
                string recordInsert = dbr.InsertRecord(e.FirstName, e.Email, e.Password);
                Toast.MakeText(this, recordInsert, ToastLength.Short).Show(); //record checker (working)
            }

            //errors in signing up
            else if (!e.Email.Contains(".edu"))
                Toast.MakeText(this, "Sorry! You must use a .edu email address", ToastLength.Short).Show();
            else if (e.Password.Length < 6)
                Toast.MakeText(this, "Oops! Your password must be more than 6 characters in length.", ToastLength.Short).Show();

        }

        /* Aesthetic Functions */

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

