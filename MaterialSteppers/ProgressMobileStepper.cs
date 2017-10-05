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
using Android.Support.V7.App;
using Java.Lang;
using Android.Support.V4.View;

namespace MaterialSteppers
{
    public abstract class ProgressMobileStepper : AppCompatActivity, View.IOnClickListener
    {
        private Button mPrevious;
        private ProgressBar mStepperProgress;
        List<Class> mStepperFragmentList;
        private BaseStepper mBaseStepper;
        private int RECOVERCURRENTSTATE = 0;
        private ScrollView mScroll;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_progress_mobile_stepper);

            Button mNext = (Button)FindViewById(Resource.Id.next);
            mPrevious = (Button)FindViewById(Resource.Id.back);
            mStepperProgress = (ProgressBar)FindViewById(Resource.Id.stepperprogressbar);
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
            UpdateUI();
            mStepperProgress.Max=(mBaseStepper.TOTAL_PAGE);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutSerializable("HSTEPPERBASE",(Java.IO.ISerializable)mStepperFragmentList);
            outState.PutInt("HCURRENT", mBaseStepper.CURRENT_PAGE);
            base.OnSaveInstanceState(outState);
        }

        protected void BackButtonConfig()
        {
            if (mBaseStepper.CURRENT_PAGE == 0)
                mPrevious.Visibility= ViewStates.Invisible;
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
                    UpdateUI();
                }

            }
            else if (i == Resource.Id.back)
            {
                mBaseStepper.OnBackButtonClicked();
                BackButtonConfig();
                UpdateUI();

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

      
        protected void UpdateUI()
        {
            mScroll.PageScroll( FocusSearchDirection.Up);
            mStepperProgress.Progress=(mBaseStepper.CURRENT_PAGE + 1);
        }

        public abstract void onStepperCompleted();
        public abstract List<Class> init();
    }
}