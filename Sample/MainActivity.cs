using Android.App;
using Java.Lang;
using System.Collections.Generic;
using MaterialSteppers;
using Sample.Fragments;

namespace Sample
{
    [Activity(Label = "Sample", MainLauncher = true)]
    public class MainActivity : SimpleMobileStepper // OR Using 'ProgressMobileStepper'
    {
        
        List<Class> stepperFragmentList = new List<Class>();

        public override List<Class> init()
        {
            stepperFragmentList.Add(Java.Lang.Class.FromType(typeof(TextFragment)));
            stepperFragmentList.Add(Java.Lang.Class.FromType(typeof(FormFragment)));
            stepperFragmentList.Add(Java.Lang.Class.FromType(typeof(Instruction)));
            stepperFragmentList.Add(Java.Lang.Class.FromType(typeof(TextFragment)));

            stepperFragmentList.Add(Java.Lang.Class.FromType(typeof(TextFragment)));
            stepperFragmentList.Add(Java.Lang.Class.FromType(typeof(FormFragment)));
            stepperFragmentList.Add(Java.Lang.Class.FromType(typeof(Instruction)));
            stepperFragmentList.Add(Java.Lang.Class.FromType(typeof(TextFragment)));

            return stepperFragmentList;
        }

        public override void onStepperCompleted()
        {
            showCompletedDialog();
        }
        protected void showCompletedDialog()
        {
            Android.Support.V7.App.AlertDialog.Builder alertDialogBuilder = new Android.Support.V7.App.AlertDialog.Builder(
                    this);

            // set title
            alertDialogBuilder.SetTitle("Hooray");
            alertDialogBuilder
                    .SetMessage("We've completed the stepper")
                    .SetCancelable(true)
                    .SetPositiveButton("Yes", delegate { });

            // create alert dialog
            Android.Support.V7.App.AlertDialog alertDialog = alertDialogBuilder.Create();

            // show it
            alertDialog.Show();

        }
    }
}

