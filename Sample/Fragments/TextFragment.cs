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
using MaterialSteppers;

namespace Sample.Fragments
{
    class TextFragment : StepperFragment
    {
        public override bool OnNextButtonHandler()
        {
            return true;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(
                Resource.Layout.textfragment, container, false);
        }
    }
}