using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TraffickController.TrafficLight;

namespace TraffickController.WebSocketConnection
{
    public class Receive : IReceive
    {
        private readonly IValidateJson _validateJson;
        private readonly IData _data;

        public Receive(IValidateJson validateJson, IData data) =>
            (_validateJson, _data) = (validateJson, data);

        public async Task<bool> ReceiveSocket(HttpContext context, WebSocket webSocket, byte[] buffer)
        {
            try
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                byte[] msgBytes = buffer.Take(result.Count).ToArray(); // Get message from websocket received
                string rcvMsg = Encoding.UTF8.GetString(msgBytes); // Encode bytes to string

                (bool valid, string key) = _validateJson.Validate(rcvMsg);

                if (valid)
                {
                    var jsonObject = JsonConvert.DeserializeObject<TrafficLightObject>(rcvMsg);
                    _data.SetTrafficData(jsonObject);

                    if (result.CloseStatus.HasValue || String.IsNullOrEmpty(rcvMsg))
                    {
                        _data.SetTrafficData(new TrafficLightObject());
                        return false;
                    }

                    return true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"JSON invalid, missing key: {key}");

                    var jsonBytes = Encoding.UTF8.GetBytes($"JSON invalid, missing key: {key}");
                    await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);

                    return false;
                }

            }
            catch(WebSocketException we)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {we.Message} in {we.Source} at {DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}");
                return false;
            }
        }
    }
}
