using System.Collections.Generic;

namespace TraffickController.TrafficLight
{
    public interface IPresetLights
    {
        string ReturnPreset(Dictionary<string, int> lightsReceived, string state);
    }
}
