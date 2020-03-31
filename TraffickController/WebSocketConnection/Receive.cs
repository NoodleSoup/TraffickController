using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

using TraffickController.JsonStrings;

namespace TraffickController.WebSocketConnection
{
    public class Receive
    {
        private static string _trafficLight;
        #region Receive
        public static async Task<bool> ReceiveSocket(HttpContext context, WebSocket webSocket, byte[] buffer)
        {
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            byte[] msgBytes = buffer.Take(result.Count).ToArray();
            string rcvMsg = Encoding.UTF8.GetString(msgBytes);

            var jsonObject = JsonConvert.DeserializeObject<TrafficLightObject>(rcvMsg);
            SetObjectData(jsonObject);

            Thread.Sleep(500);

            if (result.CloseStatus.HasValue)
                return false;
            
            return true;
        }
        #endregion

        #region SetObjectData
        private static void SetObjectData(TrafficLightObject trafficLightObject)
        {
            string newTrafficLight = JsonStringBuilder.BuildJsonString(
                A1: trafficLightObject.A1, A2: trafficLightObject.A2, A3: trafficLightObject.A3,
                A4: trafficLightObject.A4, AB1: trafficLightObject.AB1, AB2: trafficLightObject.AB2,
                B1: trafficLightObject.B1, B2: trafficLightObject.B2, B3: trafficLightObject.B3,
                B4: trafficLightObject.B4, B5: trafficLightObject.B5, BB1: trafficLightObject.BB1,
                C1: trafficLightObject.C1, C2: trafficLightObject.C2, C3: trafficLightObject.C3,
                D1: trafficLightObject.D1, D2: trafficLightObject.D2, D3: trafficLightObject.D3,
                E1: trafficLightObject.E1, E2: trafficLightObject.E2, EV1: trafficLightObject.EV1,
                EV2: trafficLightObject.EV2, EV3: trafficLightObject.EV3, EV4: trafficLightObject.EV4,
                FF1: trafficLightObject.FF1, FF2: trafficLightObject.FF2, FV1: trafficLightObject.FV1,
                FV2: trafficLightObject.FV2, FV3: trafficLightObject.FV3, FV4: trafficLightObject.FV4,
                GF1: trafficLightObject.GF1, GF2: trafficLightObject.GF2, GV1: trafficLightObject.GV1,
                GV2: trafficLightObject.GV2, GV3: trafficLightObject.GV3, GV4: trafficLightObject.GV4);

            _trafficLight = newTrafficLight;

        }
        #endregion

        #region GetNewTrafficLight
        public static string GetNewTrafficLight()
        {
            return _trafficLight;
        }
        #endregion
    }
}
