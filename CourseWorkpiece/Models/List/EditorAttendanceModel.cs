using CourseWorkpiece.Enums;

namespace CourseWorkpiece.Models.List
{
	public class EditorTrafficModel
	{
		public int id { get; set; }
		public int traffic { get; set; }
		public TypeTraffic Traffic => (TypeTraffic)traffic;
	}
}
