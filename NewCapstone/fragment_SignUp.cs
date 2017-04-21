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

namespace NewCapstone
{
    public class OnSignUpEventArgs : EventArgs //custom event args for sign up button
    {
        private string gFirstName;
        private string gEmail;
        private string gPassword;

        public string FirstName
        {
            get { return gFirstName; }
            set { gFirstName = value; }
        }

        public string Email
        {
            get { return gEmail; }
            set { gEmail = value; }
        }

        public string Password
        {
            get { return gPassword; }
            set { gPassword = value; }
        }

        //constructor, base() calls base method of EventArgs
        public OnSignUpEventArgs(string firstName, string email, string password) : base()
        {
            FirstName = firstName;
            Email = email;
            Password = password;
        }
    }
    class fragment_SignUp : DialogFragment
    {
        private EditText gDialogFirstName; //g for global variables
        private EditText gDialogEmail;
        private EditText gDialogPassword;
        private Button gDialogSignUp;

        public event EventHandler<OnSignUpEventArgs> gOnSignUpComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.fragmentSignUp, container, false);

            gDialogFirstName = view.FindViewById<EditText>(Resource.Id.txtFirstName);
            gDialogEmail = view.FindViewById<EditText>(Resource.Id.txtEmail);
            gDialogPassword = view.FindViewById<EditText>(Resource.Id.txtPassword);
            gDialogSignUp = view.FindViewById<Button>(Resource.Id.btnDialogEmail);

            gDialogSignUp.Click += (object sender, EventArgs e) =>
            {
                //when the fragment sign up button is clicked
                gOnSignUpComplete.Invoke(this, new OnSignUpEventArgs(gDialogFirstName.Text, 
                                        gDialogEmail.Text, gDialogPassword.Text));
                this.Dismiss();
            };

            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //makes title bar invisible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; //set the animation
        }
    };
};