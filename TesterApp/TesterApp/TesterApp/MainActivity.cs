using Android.App;
using Android.Widget;
using Android.OS;
using Android.Util;
using System.Net;
using System;
using System.Linq;
using System.Json;
using Android.Content;
using System.Collections.Generic;
using Android.Provider;
using Android.Content.PM;
using Android.Graphics;
using Java.IO;

namespace TesterApp
{

	[Activity(Label = "TesterApp", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{

		int count = 1;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			Log.Info("Week2", "OnCreate");
			base.OnCreate(savedInstanceState);

			if (savedInstanceState != null)
			{
				count = savedInstanceState.GetInt("clicks");
			}

			//Log.Info("TesterApp", "MainActivity - onCreate");
			//base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			 

			// Get our button from the layout resource,
			// and attach an event to it
			Button counterBtn = FindViewById<Button>(Resource.Id.counterBtn);
			counterBtn.Click += new EventHandler(this.Button1Clicked);


			if (savedInstanceState != null)
			{
				count = savedInstanceState.GetInt("counter");
				counterBtn.Text = count.ToString();
			}


			Button showListBtn = FindViewById<Button>(Resource.Id.showListBtn);
			showListBtn.Click += delegate
				{

					var StudentsList = new Intent(this, typeof(StudentsListActivity));
					StartActivity(StudentsList);
				};


		}

		protected override void OnSaveInstanceState(Bundle outState)
		{
			outState.PutInt("counter", count);
			base.OnSaveInstanceState(outState);
		}
		protected void openPracs()
		{
			

		}
		public void Button1Clicked(object sender, EventArgs e)
		{
				Log.Info("TesterApp", "myButton - Clicked");
				((Button)sender).Text = string.Format("{0} clicks!", count++);
		}

	}



	public static class App
	{
		public static File _file;
		public static File _dir;
		public static Bitmap bitmap;
	}
	public static class BitmapHelpers
	{
		public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
		{
			// First we get the the dimensions of the file on disk
			BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
			BitmapFactory.DecodeFile(fileName, options);

			// Next we calculate the ratio that we need to resize the image by
			// in order to fit the requested dimensions.
			int outHeight = options.OutHeight;
			int outWidth = options.OutWidth;
			int inSampleSize = 1;

			if (outHeight > height || outWidth > width)
			{
				inSampleSize = outWidth > outHeight
								   ? outHeight / height
								   : outWidth / width;
			}

			// Now we will load the image and have BitmapFactory resize it for us.
			options.InSampleSize = inSampleSize;
			options.InJustDecodeBounds = false;
			Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

			return resizedBitmap;
		}
	}

}


