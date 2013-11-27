using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SusiParser
{
    public enum StudentType
	{
		Bachelor,
		Master,
		Doctor
	}

	/// <summary>
	/// Holds information about a student.
	/// </summary>
    public struct StudentInfo
	{
		private static Dictionary<StudentType, int> educationLength = new Dictionary<StudentType, int>()
		{
			{ StudentType.Bachelor, 4 },
			{ StudentType.Master, 2 },
			{ StudentType.Doctor, 3}
		};

		public string FirstName;
		public string MiddleName;
		public string LastName;
		public string FacultyNumber;
		public string Programme;
		public StudentType Type;
		public int Year;
		public int Group;

		public override string ToString()
		{
			return string.Format("Name: {0}, FN: {1}, Programme: {2}, Year: {3}, Group: {4}, Type: {5}",
				FirstName + " " + MiddleName + " " + LastName,
				FacultyNumber,
				Programme,
				Year,
				Group,
				Type);
		}

		public Tuple<int, int> GetStartEndYear()
		{
			DateTime currentDate = DateTime.UtcNow;
			int startYear = currentDate.Year - this.Year + Convert.ToInt32(currentDate.Month > 6) * 1;
			int educationLength = StudentInfo.educationLength[this.Type];
			int endYear = startYear + educationLength;

			return Tuple.Create(startYear, endYear);
		}
	}
}
