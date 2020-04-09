using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TraffickController.TrafficLight
{
    public class Prioritize
    {
        private static List<string> _lightPriority;
        #region PrioritizeLights
        private static List<string> PrioritizeLights()
        {
            Regex rx = new Regex(@"([A-z]*?[0-9]): (\d)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection matches;
            GroupCollection group;
            int position = 1;

            var trafficLight = Data.ToDict();

            foreach(var x in trafficLight)
            {
                if (_lightPriority == null)
                {
                    _lightPriority.Add($"{x.Key}: {x.Value}");
                }
                else
                {
                    matches = rx.Matches(_lightPriority[0]);
                    
                    foreach (Match match in matches)
                    {
                        group = match.Groups;
                        
                        if (int.Parse(group[1].Value) > x.Value)
                        {
                            _lightPriority.Insert(0, $"{x.Key}: {x.Value}");
                        }
                        else
                        {
                            foreach(var y in _lightPriority)
                            {
                                MatchCollection matchesLight = rx.Matches(_lightPriority[position]);
                                
                                foreach (Match match1 in matchesLight)
                                {
                                    GroupCollection groupsLight = match1.Groups;
                                    if (Int32.Parse(groupsLight[1].Value) > x.Value)
                                    {
                                        _lightPriority.Insert(position, $"{x.Key}: {x.Value}");
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }

                                position++;
                            }
                        }
                    }

                    
                }
            }

            return _lightPriority;
        }
        #endregion

        public static string PriotizedLights()
        {
            if (_lightPriority[0] == "Test")
                return "test";

            return "";
        }
    }
}
