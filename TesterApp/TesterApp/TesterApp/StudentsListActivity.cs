
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

namespace TesterApp
{
	[Activity(Label = "Students List")]
	public class StudentsListActivity : Activity
	{
		
		PracticalData[] students;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Layout2);



			students = new PracticalData[] { new PracticalData("001", "Aaron") , new PracticalData("002" , "Jack")};
			ListView list = (ListView)FindViewById<ListView>(Resource.Id.listview1);
			list.Adapter = new ArrayAdapter<PracticalData>(this, Resource.Layout.ListViewTemplate2, students);

			list.ItemClick += (object sender, AdapterView.ItemClickEventArgs args) => OnListItemClick(sender, args);
		}
		protected  void OnListItemClick(object sender, EventArgs e)
		{
			AdapterView.ItemClickEventArgs arg = (AdapterView.ItemClickEventArgs) e;
			var student = students[arg.Position];

			var detailsActivity = new Intent(this, typeof(Details));
			detailsActivity.PutExtra("StudentID", student.StudentId);
			detailsActivity.PutExtra("StudentName", student.StudentName);
			StartActivity(detailsActivity);
		}

	}
}


/*
 (object sender, AdapterView.ItemClickEventArgs e) =>
			{
				String selectedFromList = (String)list.GetItemAtPosition(e.Position);
				Android.Widget.Toast.MakeText(this, selectedFromList, Android.Widget.ToastLength.Short).Show();
				var detailsActivity = new Intent(this, typeof(Details));
				detailsActivity.PutExtra("StudentID", selectedFromList);
				detailsActivity.PutExtra("StudentName", selectedFromList);
				StartActivity(detailsActivity);
			};


protected  void OnListItemClick(ListView l, View v, int position, long id)
		
*/
