using System.Collections.Generic;

namespace TraffickController.TrafficLight
{
    public interface IData
    {
        void SetTrafficData(TrafficLightObject trafficLightObject);

        string GetNewTrafficLight(string lightColor = "Green");

        Dictionary<string, int> ToDict();
    }
}
