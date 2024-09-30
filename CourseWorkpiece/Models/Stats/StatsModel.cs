using CourseWorkpiece.Enums;
using System.Text.Json.Serialization;

namespace CourseWorkpiece.Models.Stats
{
    [System.Serializable]
    public class StatsModel
	{
		[JsonInclude]
		public readonly Dictionary<TypeTraffic, int> Traffics;
		[JsonInclude]
		public readonly Dictionary<int, StudentDataModel> Students;

        public StatsModel()
        {
            Traffics = new Dictionary<TypeTraffic, int>
            {
                { TypeTraffic.None, 0 },
                { TypeTraffic.Missing, 0 },
                { TypeTraffic.ThosePresent, 0 },
                { TypeTraffic.Sick, 0 },
                { TypeTraffic.Respectful, 0 }
            };

            Students = new Dictionary<int, StudentDataModel>();
        }
    }
}
