using Newtonsoft.Json;

namespace TraffickController.JsonStrings
{
    public class JsonStringBuilder
    {
        #region BuildString
        public static string BuildJsonString(int A1 = 0, int A2 = 0, int A3 = 0, int A4 = 0, int AB1 = 0, int AB2 = 0,
                                             int B1 = 0, int B2 = 0, int B3 = 0, int B4 = 0, int B5 = 0, int BB1 = 0,
                                             int C1 = 0, int C2 = 0, int C3 = 0, int D1 = 0, int D2 = 0, int D3 = 0,
                                             int E1 = 0, int E2 = 0, int EV1 = 0, int EV2 = 0, int EV3 = 0, int EV4 = 0,
                                             int FF1 = 0, int FF2 = 0, int FV1 = 0, int FV2 = 0, int FV3 = 0,
                                             int FV4 = 0, int GF1 = 0, int GF2 = 0, int GV1 = 0, int GV2 = 0,
                                             int GV3 = 0, int GV4 = 0)
        {
            // All traffic lights, can have a value of [0, 1, 2]
            var trafficLights = new TrafficLightObject()
            {
                A1 = A1,
                A2 = A2,
                A3 = A3,
                A4 = A4,
                AB1 = AB1,
                AB2 = AB2,
                B1 = B1,
                B2 = B2,
                B3 = B3,
                B4 = B4,
                B5 = B5,
                BB1 = BB1,
                C1 = C1,
                C2 = C2,
                C3 = C3,
                D1 = D1,
                D2 = D2,
                D3 = D3,
                E1 = E1,
                E2 = E2,
                EV1 = EV1,
                EV2 = EV2,
                EV3 = EV3,
                EV4 = EV4,
                FF1 = FF1,
                FF2 = FF2,
                FV1 = FV1,
                FV2 = FV2,
                FV3 = FV3,
                FV4 = FV4,
                GF1 = GF1,
                GF2 = GF2,
                GV1 = GV1,
                GV2 = GV2,
                GV3 = GV3,
                GV4 = GV4
            };
            
            return JsonConvert.SerializeObject(trafficLights);
        }
        #endregion
    }
}
