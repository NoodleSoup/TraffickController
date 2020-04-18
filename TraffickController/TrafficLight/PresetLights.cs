using System;
using System.Collections.Generic;
using System.Text;

using TraffickController.JsonStrings;

namespace TraffickController.TrafficLight
{
    public class PresetLights
    {
        #region Presets
        private static readonly List<List<string>> presets = new List<List<string>>() { 
            new List<string>() { "A2", "A3", "A4", "B2", "B3", "B4" }, 
            new List<string>() { "C1", "C2", "C3", "A4" }, 
            new List<string>() { "D1", "D2", "D3", "B4" } 
        }; // Preset, welke stoplichten tegelijk aankunnen

        #endregion

        #region ReturnPreset
        public static string ReturnPreset(Dictionary<string, int> lightsReceived)
        {
            if (lightsReceived == null)
                return JsonStringBuilder.BuildJsonString();

            var result = FindPreset(lightsReceived);

            return result;
        }
        #endregion

        #region FindPreset
        private static string FindPreset(Dictionary<string, int> trafficAtLights)
        {
            #region Initalize Variables
            int A1 = 0;
            int A2 = 0;
            int A3 = 0;
            int A4 = 0;
            int AB1 = 0;
            int AB2 = 0;
            int B1 = 0;
            int B2 = 0;
            int B3 = 0;
            int B4 = 0;
            int B5 = 0;
            int BB1 = 0;
            int C1 = 0;
            int C2 = 0;
            int C3 = 0;
            int D1 = 0;
            int D2 = 0;
            int D3 = 0;
            int E1 = 0;
            int EV1 = 0;
            int EV2 = 0;
            int EV3 = 0;
            int EV4 = 0;
            int FF1 = 0;
            int FF2 = 0;
            int FV1 = 0;
            int FV2 = 0;
            int FV3 = 0;
            int FV4 = 0;
            int GF1 = 0;
            int GF2 = 0;
            int GV1 = 0;
            int GV2 = 0;
            int GV3 = 0;
            int GV4 = 0;
            #endregion

            Dictionary<string, int> lightsAtSameTime = new Dictionary<string, int>();

            foreach (var x in trafficAtLights)
            {
                foreach (var preset in presets)
                {
                    if (preset.Contains(x.Key))
                    {
                        foreach (var i in preset)
                            lightsAtSameTime.Add(i, x.Value);
                    }
                }
            }

            /* TODO: 
             * Dict gebruiken om via 'Key' te kijken welke stoplichten aanmogen
             * op basis van de 'Value' kijken of de stoplicht aangaat of niet, als deze 0 is
             * dan hoeft deze niet aan te gaan.
             */
            foreach (var y in lightsAtSameTime)
            {
                #region Set Lights Values
                _ = y.Equals("A1") && y.Value != 0 ? A1 = 2 : A1 = 0;
                _ = y.Equals("A2") && y.Value != 0 ? A2 = 2 : A2 = 0;
                _ = y.Equals("A3") && y.Value != 0 ? A3 = 2 : A3 = 0;
                _ = y.Equals("A4") && y.Value != 0 ? A4 = 2 : A4 = 0;
                _ = y.Equals("AB1") && y.Value != 0 ? AB1 = 2 : AB1 = 0;
                _ = y.Equals("AB2") && y.Value != 0 ? AB2 = 2 : AB2 = 0;
                _ = y.Equals("B1") && y.Value != 0 ? B1 = 2 : B1 = 0;
                _ = y.Equals("B2") && y.Value != 0 ? B2 = 2 : B2 = 0;
                _ = y.Equals("B3") && y.Value != 0 ? B3 = 2 : B3 = 0;
                _ = y.Equals("B4") && y.Value != 0 ? B4 = 2 : B4 = 0;
                _ = y.Equals("B5") && y.Value != 0 ? B5 = 2 : B5 = 0;
                _ = y.Equals("BB1") && y.Value != 0 ? BB1 = 2 : BB1 = 0;
                _ = y.Equals("C1") && y.Value != 0 ? C1 = 2 : C1 = 0;
                _ = y.Equals("C2") && y.Value != 0 ? C2 = 2 : C2 = 0;
                _ = y.Equals("C3") && y.Value != 0 ? C3 = 2 : C3 = 0;
                _ = y.Equals("D1") && y.Value != 0 ? D1 = 2 : D1 = 0;
                _ = y.Equals("D2") && y.Value != 0 ? D2 = 2 : D2 = 0;
                _ = y.Equals("D3") && y.Value != 0 ? D3 = 2 : D3 = 0;
                _ = y.Equals("E1") && y.Value != 0 ? E1 = 2 : E1 = 0;
                _ = y.Equals("EV1") && y.Value != 0 ? EV1 = 2 : EV1 = 0;
                _ = y.Equals("EV2") && y.Value != 0 ? EV2 = 2 : EV2 = 0;
                _ = y.Equals("EV3") && y.Value != 0 ? EV3 = 2 : EV3 = 0;
                _ = y.Equals("EV4") && y.Value != 0 ? EV4 = 2 : EV4 = 0;
                _ = y.Equals("FF1") && y.Value != 0 ? FF1 = 2 : FF1 = 0;
                _ = y.Equals("FF2") && y.Value != 0 ? FF2 = 2 : FF2 = 0;
                _ = y.Equals("FV1") && y.Value != 0 ? FV1 = 2 : FV1 = 0;
                _ = y.Equals("FV2") && y.Value != 0 ? FV2 = 2 : FV2 = 0;
                _ = y.Equals("FV3") && y.Value != 0 ? FV3 = 2 : FV3 = 0;
                _ = y.Equals("FV4") && y.Value != 0 ? FV4 = 2 : FV4 = 0;
                _ = y.Equals("GF1") && y.Value != 0 ? GF1 = 2 : GF1 = 0;
                _ = y.Equals("GF2") && y.Value != 0 ? GF2 = 2 : GF2 = 0;
                _ = y.Equals("GV1") && y.Value != 0 ? GV1 = 2 : GV1 = 0;
                _ = y.Equals("GV2") && y.Value != 0 ? GV2 = 2 : GV2 = 0;
                _ = y.Equals("GV3") && y.Value != 0 ? GV3 = 2 : GV3 = 0;
                _ = y.Equals("GV4") && y.Value != 0 ? GV4 = 2 : GV4 = 0;
                #endregion
            }

            string newTrafficLight = JsonStringBuilder.BuildJsonString(
                A1: A1, A2: A2, A3: A3,
                A4: A4, AB1: AB1, AB2: AB2,
                B1: B1, B2: B2, B3: B3,
                B4: B4, B5: B5, BB1: BB1,
                C1: C1, C2: C2, C3: C3,
                D1: D1, D2: D2, D3: D3,
                E1: E1, EV1: EV1, EV2: EV2,
                EV3: EV3, EV4: EV4, FF1: FF1,
                FF2: FF2, FV1: FV1, FV2: FV2,
                FV3: FV3, FV4: FV4, GF1: GF1,
                GF2: GF2, GV1: GV1, GV2: GV2,
                GV3: GV3, GV4: GV4);

            return newTrafficLight;
        }
        #endregion
    }
}
