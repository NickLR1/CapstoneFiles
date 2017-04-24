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

using SQLite;

namespace NewCapstone
{
    public class OnSignInEventArgs : EventArgs //custom args for sign in button
    {
        private string gSignInEmail;
        private string gSignInPassword;

        public string SignInEmail
        {
            get { return gSignInEmail; }
            set { gSignInEmail = value; }
        }

        public string SignInPassword
        {
            get { return gSignInPassword; }
            set { gSignInPassword = value; }
        }

        //constructor
        public OnSignInEventArgs(string email, string password) : base()
        {
            SignInEmail = email;
            SignInPassword = password;
        }
    }

    class fragment_SignIn : DialogFragment
    {
        private EditText gSignInEmail; //g for global
        private EditText gSignInPassword;
        private Button gSignInButton;

        public event EventHandler<OnSignInEventArgs> gOnSignInComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragmentSignIn, container, false);

            gSignInEmail = view.FindViewById<EditText>(Resource.Id.txtSignInEmail);
            gSignInPassword = view.FindViewById<EditText>(Resource.Id.txtSignInPassword);
            gSignInButton = view.FindViewById<Button>(Resource.Id.btnSignInFragmentEmail);

            gSignInButton.Click += (object sender, EventArgs e) =>
            {
                //when the fragment sign in button is clicked
                gOnSignInComplete.Invoke(this, new OnSignInEventArgs(gSignInEmail.Text, gSignInPassword.Text));
                this.Dismiss();
            };
            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //title bar invisible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; //set the animation
        }
    }
}