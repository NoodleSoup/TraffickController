using Newtonsoft.Json.Linq;

namespace TraffickController.TrafficLight
{
    public class ValidateJson
    {
        #region Variables
        private static string[] _validKeys = new string[]{"A1", "A2", "A3", "A4", "AB1", "AB2",
                                             "B1", "B2", "B3", "B4", "B5", "BB1",
                                             "C1", "C2", "C3", "D1", "D2", "D3",
                                             "E1", "EV1", "EV2", "EV3", "EV4",
                                             "FF1", "FF2", "FV1", "FV2", "FV3",
                                             "FV4", "GF1", "GF2", "GV1", "GV2",
                                             "GV3", "GV4"};
        #endregion

        #region Validate
        public static (bool, string) Validate(string json)
        {
            JObject trafficLight = JObject.Parse(json);

            foreach (string x in _validKeys)
            {
                if (trafficLight.ContainsKey(x))
                    continue;
                else
                    return (false, x);
            }

            return (true, "");
        }
        #endregion
    }
}
