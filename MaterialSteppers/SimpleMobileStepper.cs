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
using Java.Lang;
using Android.Support.V7.App;
using Android.Support.V4.View;
using Java.IO;

namespace MaterialSteppers
{
    public abstract class SimpleMobileStepper : AppCompatActivity, View.IOnClickListener
    {
        private Button mPrevious;
        private TextView mStepText;
        List<Class> mStepperFragmentList;
        private BaseStepper mBaseStepper;
        private int RECOVERCURRENTSTATE = 0;
        private ScrollView mScroll;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_simple_mobile_stepper);

            Button mNext = (Button)FindViewById(Resource.Id.next);
            mPrevious = (Button)FindViewById(Resource.Id.back);
            mStepText = (TextView)FindViewById(Resource.Id.steps);
            ViewPager mViewPager = (ViewPager)FindViewById(Resource.Id.viewpager);
            mScroll = (ScrollView)FindViewById(Resource.Id.mobilescroll);

            mNext.SetOnClickListener(this);
            mPrevious.SetOnClickListener(this);
            if (savedInstanceState != null)
            {
                if (savedInstanceState.GetSerializable("HSTEPPERBASE") != null)
                {
                    try
                    {
                        mStepperFragmentList = (List<Class>)savedInstanceState.GetSerializable("HSTEPPERBASE");
                        RECOVERCURRENTSTATE = savedInstanceState.GetInt("HCURRENT");
                    }
                    catch (Java.Lang.Exception e)
                    {
                        //it's  okay we will recover from the init method
                        mStepperFragmentList = init();
                    }
                }
                else
                {
                    mStepperFragmentList = init();
                }
            }
            else
            {
                mStepperFragmentList = init();
            }
            mBaseStepper = new BaseStepper(mViewPager, mStepperFragmentList, SupportFragmentManager);
            mBaseStepper.CURRENT_PAGE = RECOVERCURRENTSTATE;
            RECOVERCURRENTSTATE = 0;
            BackButtonConfig();
            updateUI();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutSerializable("HSTEPPERBASE", (ISerializable)mStepperFragmentList);
            outState.PutInt("HCURRENT", mBaseStepper.CURRENT_PAGE);
            base.OnSaveInstanceState(outState);
        }

        protected void BackButtonConfig()
        {
            if (mBaseStepper.CURRENT_PAGE == 0)
                mPrevious.Visibility = ViewStates.Invisible;
            else
                mPrevious.Visibility=(ViewStates.Visible);
        }

        public void OnClick(View v)
        {
            int i = v.Id;
            if (i == Resource.Id.next)
            {
                if (checkStepper())
                {
                    mBaseStepper.OnNextButtonClicked();
                    BackButtonConfig();
                    updateUI();
                }

            }
            else if (i == Resource.Id.back)
            {
                mBaseStepper.OnBackButtonClicked();
                BackButtonConfig();
                updateUI();

            }
        }

        public int GetCurrentFragmentId()
        {
            return mBaseStepper.CURRENT_PAGE;
        }

        public bool checkStepper()
        {
            if (mBaseStepper.ResolveNavigation())
            {
                return true;
            }
            onStepperCompleted();
            return false;

        }

        public void updateUI()
        {
            mStepText.Text=("Step " + (mBaseStepper.CURRENT_PAGE + 1) + " of " + mBaseStepper.TOTAL_PAGE);
            mScroll.PageScroll( FocusSearchDirection.Up);
        }
        public abstract void onStepperCompleted();
        public abstract List<Class> init();
    }
}