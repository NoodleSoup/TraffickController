using System;
using System.Collections.Generic;
using System.Text;

namespace TraffickController.JsonStrings
{
    public class JsonStringBuilder
    {
        #region BuildString
        public static string BuildJsonString()
        {
            var trafficLights = new TrafficLightObject();
            // All traffic lights, can have a value of [0, 1, 2]
            trafficLights.A1 = 0;
            trafficLights.A2 = 0;
            trafficLights.A3 = 0;
            trafficLights.A4 = 0;
            trafficLights.AB1 = 0;
            trafficLights.AB2 = 0;
            trafficLights.B1 = 0;
            trafficLights.B2 = 0;
            trafficLights.B3 = 0;
            trafficLights.B4 = 0;
            trafficLights.B5 = 0;
            trafficLights.BB1 = 0;
            trafficLights.C1 = 0;
            trafficLights.C2 = 0;
            trafficLights.C3 = 0;
            trafficLights.D1 = 0;
            trafficLights.D2 = 0;
            trafficLights.D3 = 0;
            trafficLights.E1 = 0;
            trafficLights.E2 = 0;
            trafficLights.EV1 = 0;
            trafficLights.EV2 = 0;
            trafficLights.EV3 = 0;
            trafficLights.EV4 = 0;
            trafficLights.FF1 = 0;
            trafficLights.FF2 = 0;
            trafficLights.FV1 = 0;
            trafficLights.FV2 = 0;
            trafficLights.FV3 = 0;
            trafficLights.FV4 = 0;
            trafficLights.GF1 = 0;
            trafficLights.GF2 = 0;
            trafficLights.GV1 = 0;
            trafficLights.GV2 = 0;
            trafficLights.GV3 = 0;
            trafficLights.GV4 = 0;

            var jsonString = $@"
            {{
                ""A1"": {trafficLights.A1},
                ""A2"": {trafficLights.A2},
                ""A3"": {trafficLights.A3},
                ""A4"": {trafficLights.A4},
                ""AB1"": {trafficLights.AB1},
                ""AB2"": {trafficLights.AB2},
                ""B1"": {trafficLights.B1},
                ""B2"": {trafficLights.B2},
                ""B3"": {trafficLights.B3},
                ""B4"": {trafficLights.B4},
                ""B5"": {trafficLights.B5},
                ""BB1"": {trafficLights.BB1},
                ""C1"": {trafficLights.C1},
                ""C2"": {trafficLights.C2},
                ""C3"": {trafficLights.C3},
                ""D1"": {trafficLights.D1},
                ""D2"": {trafficLights.D2},
                ""D3"": {trafficLights.D3},
                ""E1"": {trafficLights.E1},
                ""E2"": {trafficLights.E2},
                ""EV1"": {trafficLights.EV1},
                ""EV2"": {trafficLights.EV2},
                ""EV3"": {trafficLights.EV3},
                ""EV4"": {trafficLights.EV4},
                ""FF1"": {trafficLights.FF1},
                ""FF2"": {trafficLights.FF2},
                ""FV1"": {trafficLights.FV1},
                ""FV2"": {trafficLights.FV2},
                ""FV3"": {trafficLights.FV3},
                ""FV4"": {trafficLights.FV4},
                ""GF1"": {trafficLights.GF1},
                ""GF2"": {trafficLights.GF2},
                ""GV1"": {trafficLights.GV1},
                ""GV2"": {trafficLights.GV2},
                ""GV3"": {trafficLights.GV3},
                ""GV4"": {trafficLights.GV4}
            }}";

            return jsonString;
        }
        #endregion
    }
}
