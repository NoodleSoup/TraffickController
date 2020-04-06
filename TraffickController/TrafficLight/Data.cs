using System;
using System.Collections.Generic;
using System.Text;

using TraffickController.JsonStrings;

namespace TraffickController.TrafficLight
{
    public class Data
    {
        private static string _trafficLight;
        private static TrafficLightObject _trafficLightObject;
        
        #region SetTrafficData
        public static void SetTrafficData(TrafficLightObject trafficLightObject)
        {
            string newTrafficLight = JsonStringBuilder.BuildJsonString(
                A1: trafficLightObject.A1, A2: trafficLightObject.A2, A3: trafficLightObject.A3,
                A4: trafficLightObject.A4, AB1: trafficLightObject.AB1, AB2: trafficLightObject.AB2,
                B1: trafficLightObject.B1, B2: trafficLightObject.B2, B3: trafficLightObject.B3,
                B4: trafficLightObject.B4, B5: trafficLightObject.B5, BB1: trafficLightObject.BB1,
                C1: trafficLightObject.C1, C2: trafficLightObject.C2, C3: trafficLightObject.C3,
                D1: trafficLightObject.D1, D2: trafficLightObject.D2, D3: trafficLightObject.D3,
                E1: trafficLightObject.E1, EV1: trafficLightObject.EV1, EV2: trafficLightObject.EV2,
                EV3: trafficLightObject.EV3, EV4: trafficLightObject.EV4, FF1: trafficLightObject.FF1,
                FF2: trafficLightObject.FF2, FV1: trafficLightObject.FV1, FV2: trafficLightObject.FV2,
                FV3: trafficLightObject.FV3, FV4: trafficLightObject.FV4, GF1: trafficLightObject.GF1,
                GF2: trafficLightObject.GF2, GV1: trafficLightObject.GV1, GV2: trafficLightObject.GV2,
                GV3: trafficLightObject.GV3, GV4: trafficLightObject.GV4);

            _trafficLight = newTrafficLight;
            _trafficLightObject = trafficLightObject;
        }
        #endregion

        #region GetNewTrafficLight
        public static string GetNewTrafficLight()
        {
            Console.WriteLine(_trafficLight);
            return _trafficLight;
        }
        #endregion

        #region ToDict
        public static Dictionary<string, int> ToDict()
        {
            Dictionary<string, int> trafficLightDict = new Dictionary<string, int>();
            
            trafficLightDict.Add("A1", _trafficLightObject.A1);
            trafficLightDict.Add("A2", _trafficLightObject.A2);
            trafficLightDict.Add("A3", _trafficLightObject.A3);
            trafficLightDict.Add("A4", _trafficLightObject.A4);
            trafficLightDict.Add("AB1", _trafficLightObject.AB1);
            trafficLightDict.Add("AB2", _trafficLightObject.AB2);
            trafficLightDict.Add("B1", _trafficLightObject.B1);
            trafficLightDict.Add("B2", _trafficLightObject.B2);
            trafficLightDict.Add("B3", _trafficLightObject.B3);
            trafficLightDict.Add("B4", _trafficLightObject.B4);
            trafficLightDict.Add("B5", _trafficLightObject.B5);
            trafficLightDict.Add("BB1", _trafficLightObject.BB1);
            trafficLightDict.Add("C1", _trafficLightObject.C1);
            trafficLightDict.Add("C2", _trafficLightObject.C2);
            trafficLightDict.Add("C3", _trafficLightObject.C3);
            trafficLightDict.Add("D1", _trafficLightObject.D1);
            trafficLightDict.Add("D2", _trafficLightObject.D2);
            trafficLightDict.Add("D3", _trafficLightObject.D3);
            trafficLightDict.Add("E1", _trafficLightObject.E1);
            trafficLightDict.Add("EV1", _trafficLightObject.EV1);
            trafficLightDict.Add("EV2", _trafficLightObject.EV2);
            trafficLightDict.Add("EV3", _trafficLightObject.EV3);
            trafficLightDict.Add("EV4", _trafficLightObject.EV4);
            trafficLightDict.Add("FF1", _trafficLightObject.FF1);
            trafficLightDict.Add("FF2", _trafficLightObject.FF2);
            trafficLightDict.Add("FV1", _trafficLightObject.FV1);
            trafficLightDict.Add("FV2", _trafficLightObject.FV2);
            trafficLightDict.Add("FV3", _trafficLightObject.FV3);
            trafficLightDict.Add("FV4", _trafficLightObject.FV4);
            trafficLightDict.Add("GF1", _trafficLightObject.GF1);
            trafficLightDict.Add("GF2", _trafficLightObject.GF2);
            trafficLightDict.Add("GV1", _trafficLightObject.GV1);
            trafficLightDict.Add("GV2", _trafficLightObject.GV2);
            trafficLightDict.Add("GV3", _trafficLightObject.GV3);
            trafficLightDict.Add("GV4", _trafficLightObject.GV4);

            return trafficLightDict;
        }
        #endregion
    }
}
