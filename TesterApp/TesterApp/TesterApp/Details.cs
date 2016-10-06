
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;

namespace TesterApp
{
	[Activity(Label = "Student Details")]
	public class Details : Activity
	{
		ImageView _imageView;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Details);
			EditText id =  FindViewById<EditText>(Resource.Id.editText1);
			id.Text = Intent.GetStringExtra("StudentID");

			EditText name = FindViewById<EditText>(Resource.Id.editText2);
			name.Text = Intent.GetStringExtra("StudentName");


			Button backBtn = FindViewById<Button>(Resource.Id.backBtn);
			backBtn.Click += (sender, e) => { base.OnBackPressed(); };

			if (IsThereAnAppToTakePictures())
			{
				CreateDirectoryForPictures();

				var button = FindViewById<Button>(Resource.Id.takeImagebtn);
				_imageView = FindViewById<ImageView>(Resource.Id.imageView1);

				button.Click += new EventHandler(this.TakeAPicture);
			}

		}
		private bool IsThereAnAppToTakePictures()
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			IList<ResolveInfo> availableActivities =
				PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
			return availableActivities != null && availableActivities.Count > 0;
		}

		private void TakeAPicture(object sender, EventArgs eventArgs)
		{
			Intent intent = new Intent(MediaStore.ActionImageCapture);
			App._file = new File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
			intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(App._file));
			StartActivityForResult(intent, 0);
		}
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			// Make it available in the gallery

			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
			Android.Net.Uri contentUri = Android.Net.Uri.FromFile(App._file);
			mediaScanIntent.SetData(contentUri);
			SendBroadcast(mediaScanIntent);

			// Display in ImageView. We will resize the bitmap to fit the display.
			// Loading the full sized image will consume to much memory
			// and cause the application to crash.

			int height = Resources.DisplayMetrics.HeightPixels;
			int width = _imageView.Height;
			App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
			if (App.bitmap != null)
			{
				_imageView.SetImageBitmap(App.bitmap);
				App.bitmap = null;
			}

			// Dispose of the Java side bitmap.
			GC.Collect();
		}

		private void CreateDirectoryForPictures()
		{
			App._dir = new File(
				Android.OS.Environment.GetExternalStoragePublicDirectory(
					Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
			if (!App._dir.Exists())
			{
				App._dir.Mkdirs();
			}
		}

	}
}

