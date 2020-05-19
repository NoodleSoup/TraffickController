using System;
using System.Collections.Generic;

namespace TraffickController.TrafficLight
{
    public class Data
    {
        private static TrafficLightObject _trafficLightObject = null;

        #region SetTrafficData
        public static void SetTrafficData(TrafficLightObject trafficLightObject)
        {
            _trafficLightObject = trafficLightObject;
        }
        #endregion

        #region GetNewTrafficLight
        public static string GetNewTrafficLight(string lightColor = "Green")
        {
            return PresetLights.ReturnPreset(ToDict(), lightColor);
        }
        #endregion

        #region ToDict
        public static Dictionary<string, int> ToDict()
        {
            Dictionary<string, int> trafficLightDict = new Dictionary<string, int>();

            // Use 'try catch' to prevent exceptions in case the trafficlightobject is null or hasn't been filled
            try
            {
                #region Add lights to Dictionary
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
                #endregion
            }
            catch (Exception e)
            {
                // var trace = new System.Diagnostics.StackFrame(e);
                // Console.WriteLine($"Exception in {trace.GetFileName()} line {trace.GetFileLineNumber()}: {e}");
                return null; // return null to make the next function send the start state instead of trying to find a preset
            }

            return trafficLightDict;
        }
        #endregion
    }
}
