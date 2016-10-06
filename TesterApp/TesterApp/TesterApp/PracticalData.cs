using System;
using Android.Widget;

namespace TesterApp
{
	
	public class PracticalData
	{
		

		public PracticalData(string v1, string v2)
		{
			this.StudentId = v1;
			this.StudentName = v2;
		}

		public string StudentId { get; set; }
		public string StudentName { get; set; }
		public string PracticalDate { get; set; }
		public string StudentImagePath { get; set; }

		public override string ToString()
		{
			return string.Format("[StudentId={0}, StudentName={1}]", StudentId, StudentName);
		}
	}
}

