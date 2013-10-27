using HtmlAgilityPack;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SusiParser
{
	public class SusiParser
	{
		static string susiAddress = @"https://susi.uni-sofia.bg";
		static string loginPageAddress = @"https://susi.uni-sofia.bg/ISSU/forms/Login.aspx";
		static string homePageAddress = @"https://susi.uni-sofia.bg/ISSU/forms/students/home.aspx";
		static string reportExamsAddress = @"https://susi.uni-sofia.bg/ISSU/forms/students/ReportExams.aspx";

		public CookieContainer Cookies = new CookieContainer();

		private string formData = "";
		private string vstate = "";

		public SusiParser()
		{
			HtmlNode.ElementsFlags.Remove("form");
		}

		private string GetExamPage(bool recurse = true)
		{
			//AWFUL CODE, BLAME SUSI
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(reportExamsAddress);
			request.CookieContainer = this.Cookies;
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			using (var requestStream = request.GetRequestStream())
			using (var writer = new StreamWriter(requestStream))
			{
				//writer.Write(@"__EVENTTARGET=Report_Exams1%24btnReportExams&" + this.formData);

				string data = string.Format("__EVENTTARGET=Report_Exams1%24btnReportExams&__EVENTARGUMENT=&__VSTATE={0}&__VIEWSTATE=&Report_Exams1%24chkTaken=on&__EVENTVALIDATION=%2FwEWGAL%2BraDpAgK3pdr8BQKN%2BNnHCgKJ7OiyDgL77521DgLy1aS1CQK%2FqPbTAQLVh87yAgLInunZCAKtla77CAL%2BsbCuCgKd6OrBDQKOiLS8DALkxb3FBwKOy6myDQLrn7D%2BBgKmzenZCALztLy4DgKTxe2tBwLHieKTBgKS4YnZCwKu3dGoAQKZ3sM%2BAt%2FH5N8HqzstQIPHe%2F5TQABqakO68cYBFzo%3D",
					HttpUtility.UrlEncode(this.vstate));
				writer.Write(data);
			}

			WebResponse response = request.GetResponse();

			if (recurse)
			{
				ExtractFormData(response);
				return this.GetExamPage(false);
			}
			using (var responseStream = response.GetResponseStream())
			using (var reader = new StreamReader(responseStream))
			{
				var result = reader.ReadToEnd();
				return result;
			}
		}

		private string GetHomePage()
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(homePageAddress);
			request.CookieContainer = this.Cookies;
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			using (var requestStream = request.GetRequestStream())
			using (var writer = new StreamWriter(requestStream))
			{
				//writer.Write(this.formData);
				writer.Write("__EVENTTARGET=Report_Exams1%24btnReportExams&__EVENTARGUMENT=&__VSTATE=eJz7z8ifws%2fKZWhsamBhYWBgYsmfIsaUhkKIMDHyizHJsYdlFmcm5aRmpDAxA%2fnyDEAGK0gNUBokzxKSWlGSmpLCxI4QlGcECXCiC3CjC%2fDCDORHlxGEyQjzQ1kpALbbHB0%3d&__VIEWSTATE=&Report_Exams1%24chkTaken=on&__EVENTVALIDATION=%2FwEWGAL%2BraDpAgK3pdr8BQKN%2BNnHCgKJ7OiyDgL77521DgLy1aS1CQK%2FqPbTAQLVh87yAgLInunZCAKtla77CAL%2BsbCuCgKd6OrBDQKOiLS8DALkxb3FBwKOy6myDQLrn7D%2BBgKmzenZCALztLy4DgKTxe2tBwLHieKTBgKS4YnZCwKu3dGoAQKZ3sM%2BAt%2FH5N8HqzstQIPHe%2F5TQABqakO68cYBFzo%3D");
			}

			WebResponse response = request.GetResponse();

			using (var responseStream = response.GetResponseStream())
			using (var reader = new StreamReader(responseStream))
			{
				var result = reader.ReadToEnd();
				return result;
			}
		}

		/// <summary>
		/// Logs the user in Susi.
		/// </summary>
		/// <param name="username">The username in Susi</param>
		/// <param name="password">The password in Susi</param>
		public void Login(string username, string password)
		{
			try
			{
				ServicePointManager.Expect100Continue = false;

				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(loginPageAddress);
				request.CookieContainer = this.Cookies;
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				using (var requestStream = request.GetRequestStream())
				using (var writer = new StreamWriter(requestStream))
				{
					// SQL INJECTION TIME!
					string data = string.Format("__EVENTTARGET=&__EVENTARGUMENT=&__VSTATE=eJz7z8ifws%252fKZWhsamBhYWBgYsmfIsaUhkKIMDHyizHJsYdlFmcm5aRmpDAxA%252fnyDEAGK0gNUBokzxKSWlGSmpLCxI4QlGcECXCiC3CjC%252fDCDORHlxGEyQjzQ1kpALbbHB0%253d&__VIEWSTATE=&txtUserName={0}&txtPassword={1}&btnSubmit=%D0%92%D0%BB%D0%B5%D0%B7&__EVENTVALIDATION=%2FwEWBAL%2BraDpAgKl1bKzCQK1qbSRCwLCi9reA3ZPH%2F0OXuiqks41bB%2BF30DwDzP9",
						username,
						password);
					writer.Write(data);
				}

				WebResponse response = request.GetResponse();
				ExtractFormData(response);
			}
			catch (Exception e)
			{
				throw new WebException("Can't log into SUSI", e);
			}
		}

		private void ExtractFormData(WebResponse response)
		{
			HtmlDocument document = new HtmlDocument();
			document.Load(response.GetResponseStream());

			string hiddenXpath = @"//input[@id=""__VSTATE""]";
			HtmlNode node = document.DocumentNode.SelectSingleNode(hiddenXpath);
			this.vstate = node.Attributes["value"].Value.Trim();
		}

		/// <summary>
		/// Gets a collection of <see cref="CourseInfo"/> that shows all courses that the student has passed successfully.
		/// </summary>
		public IEnumerable<CourseInfo> GetCourses()
		{
			try
			{
				string examPage = this.GetExamPage();
				HtmlNode.ElementsFlags.Remove("form");

				HtmlDocument document = new HtmlDocument();
				document.LoadHtml(examPage);

				//string xpath = @"/html/body/form/table//table[@width=""100%""]//tr[not(@class)]";
				string xpath = @"/html/body/form/table/tr/td/table[@width=""100%""]/tr/td/table/tr[not(@class)]";
				var nodes = document.DocumentNode.SelectNodes(xpath);

				List<CourseInfo> courseInfos = new List<CourseInfo>();
				CultureInfo culture = Thread.CurrentThread.CurrentCulture;
				Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("bg-BG");
				foreach (HtmlNode node in nodes)
				{
					HtmlNodeCollection children = node.SelectNodes("td");

					if (children[0].InnerText.Contains("Предмет"))
						continue;

					CourseInfo courseInfo = new CourseInfo();
					courseInfo.CourseName = children[0].InnerText.Trim();
					courseInfo.Teacher = children[1].InnerText.Trim();
					courseInfo.IsElective = children[2].InnerText.Trim() != "Задължителни";
					courseInfo.IsTaken = children[3].SelectSingleNode("span").InnerText.Trim() == "да";
					courseInfo.Grade = Double.Parse(children[4].SelectSingleNode("span").InnerText.Trim().Substring(0, 1));
					courseInfo.Credits = Double.Parse(children[5].InnerText.Trim());

					courseInfos.Add(courseInfo);
				}
				Thread.CurrentThread.CurrentCulture = culture;

				return courseInfos;
			}
			catch (Exception e)
			{
				throw new WebException("Can't load data from SUSI", e);
			}
		}

		/// <summary>
		/// Gets information about the currently logged in student from Susi.
		/// </summary>
		public StudentInfo GetStudentInfo()
		{
			try
			{
				string xpath = @"html/body/form/table[3]/tr/td[2]";

				HtmlDocument document = new HtmlDocument();
				document.LoadHtml(this.GetHomePage());

				HtmlNode node = document.DocumentNode.SelectSingleNode(xpath);

				StudentInfo info = new StudentInfo();
				string[] names = node.SelectSingleNode("span/span").InnerText.Trim().Split(' ');
				info.FirstName = names[0];
				info.MiddleName = names[1];
				info.LastName = names[2];

				HtmlNodeCollection otherInfo = node.SelectNodes("strong/span");
				info.FacultyNumber = otherInfo[0].InnerText.Trim();
				info.Programme = otherInfo[1].InnerText.Trim(); 
				// In case the student has dropped (the base)
				if (string.IsNullOrWhiteSpace(otherInfo[2].InnerHtml))
				{
					info.Year = 0;
					info.Group = 0;
				}
				else
				{
					info.Year = Int32.Parse(otherInfo[2].InnerText.Replace("Курс", "").Trim());
					info.Group = Int32.Parse(otherInfo[3].InnerText.Replace("Група", "").Trim());
				}
				// Don't ask
				string studentType = node.SelectNodes("span").Last().InnerText.Split(',')[1].Trim().ToUpperInvariant();
				StudentType type;
				if (studentType == "БАКАЛАВРИ")
					type = StudentType.Bachelor;
				else if (studentType == "МАГИСТРИ")
					type = StudentType.Master;
				else
					type = StudentType.Doctor;

				info.Type = type;

				return info;
			}
			catch (Exception e)
			{
				throw new WebException("Can't load data from SUSI", e);
			}
		}
	}
}
