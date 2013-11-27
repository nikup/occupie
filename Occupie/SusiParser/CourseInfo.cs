using Occupie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SusiParser
{
	/// <summary>
	/// Holds information about a single course.
	/// </summary>
    public struct CourseInfo
	{
		public struct JsonStructuredCourse
		{
			[JsonProperty("group")]
			public string Group;
			[JsonProperty("name")]
			public string Name;
		}

		public static string CoursesDescription;// = HttpContext.Current.Server.MapPath("~/App_Data/courses.json");
		public static Func<JsonStructuredCourse, string> keySelector,// = (x) => x.Name,
			valueSelector;// = (y) => y.Group;
		public static Dictionary<string, string> CourseToCategory;// =
			//JsonConvert.DeserializeObject<JsonStructuredCourse[]>(File.ReadAllText(CoursesDescription)).ToDictionary(keySelector, valueSelector);

		private string courseName;

		static CourseInfo()
		{
			CoursesDescription = HttpContext.Current.Server.MapPath("~/App_Data/courses.json");
			keySelector = (x) => x.Name;
			valueSelector = (y) => y.Group;
			CourseToCategory =
				JsonConvert.DeserializeObject<JsonStructuredCourse[]>(File.ReadAllText(CoursesDescription)).ToDictionary(keySelector, valueSelector);

		}

		public string CourseName
		{
			get { return this.courseName; }
			set
			{
				this.courseName = value;
				this.Category = CourseToCategory.ContainsKey(value) ? CourseToCategory[value] : string.Empty;
			}
		}
		public string Teacher;
		public double Grade;
		public bool IsTaken;
		public bool IsElective;
		public double Credits;

		public string Category { get; private set; }

		public override string ToString()
		{
			return string.Format("Course: {0}, Teacher: {1}, Grade: {2}, Credits: {3}, Elective: {4}, Taken: {5}", 
				this.CourseName, this.Teacher, this.Grade, this.Credits, this.IsElective, this.IsTaken);
		}

		public Subject ToSubject()
		{
			Subject subject = new Subject();
			subject.Name = this.CourseName;
			subject.Grade = this.Grade;
			subject.Credits = this.Credits;
			subject.Category = this.Category;

			return subject;
		}

		public static List<string> GetCoursesFromCategory(string category)
		{
			return CourseToCategory.Keys.Where(x => CourseToCategory[x] == category).ToList();
		}

		public static Dictionary<string, List<string>> PartitionCoursesByCategory()
		{
			var partition = new Dictionary<string, List<string>>();
			foreach (var entry in CourseToCategory)
			{
				if (!partition.ContainsKey(entry.Value))
				{
					partition.Add(entry.Value, new List<string>());
				}
				partition[entry.Value].Add(entry.Key);
			}

			return partition;
		}
	}
}
