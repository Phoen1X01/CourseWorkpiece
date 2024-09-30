using CourseWorkpiece.Enums;
using System.Text.Json.Serialization;

namespace CourseWorkpiece.Models.Stats
{
    [System.Serializable]
    public class StudentDataModel
	{
		[JsonInclude]
		public readonly Dictionary<TypeTraffic, int> Traffics;
		[JsonInclude]
		public readonly Student Student;

        public StudentDataModel(Student student)
        {
			Student = student;
            Traffics = new Dictionary<TypeTraffic, int>
            {
                { TypeTraffic.None, 0 },
                { TypeTraffic.Missing, 0 },
                { TypeTraffic.ThosePresent, 0 },
                { TypeTraffic.Sick, 0 },
                { TypeTraffic.Respectful, 0 }
            };
        }
    }
}
