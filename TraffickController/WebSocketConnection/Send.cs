using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using TraffickController.JsonStrings;
using TraffickController.TrafficLight;

namespace TraffickController.WebSocketConnection
{
    public class Send
    {
        private static string jsonTrafficLight = null;
        #region SendState
        public static async Task SendState(HttpContext context, WebSocket webSocket)
        {
            jsonTrafficLight = Data.GetNewTrafficLight(); // Start setting up the JSON string builder JsonStringBuilder.BuildJsonString();

            // If the trafficlight string is empty for some reason send the default response (start state)
            if (String.IsNullOrEmpty(jsonTrafficLight))
                jsonTrafficLight = JsonStringBuilder.BuildJsonString();

            var jsonBytes = Encoding.UTF8.GetBytes(jsonTrafficLight); // Convert the JSON object to bytes to send over WebSocket connection

            try
            {
                await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);
            }catch(WebSocketException we)
            {
                System.Diagnostics.Debug.WriteLine($"Exception: {we.Message} in {we.Source} at {DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")}");
                return;
            }

            Thread.Sleep(6000);

            jsonTrafficLight = Data.GetNewTrafficLight("Orange"); // Start setting up the JSON string builder JsonStringBuilder.BuildJsonString();

            // If the trafficlight string is empty for some reason send the default response (start state)
            if (String.IsNullOrEmpty(jsonTrafficLight))
                jsonTrafficLight = JsonStringBuilder.BuildJsonString();

            jsonBytes = Encoding.UTF8.GetBytes(jsonTrafficLight); // Convert the JSON object to bytes to send over WebSocket connection

            await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);

            Thread.Sleep(2000);

            jsonTrafficLight = Data.GetNewTrafficLight("Red"); // Start setting up the JSON string builder JsonStringBuilder.BuildJsonString();

            // If the trafficlight string is empty for some reason send the default response (start state)
            if (String.IsNullOrEmpty(jsonTrafficLight))
                jsonTrafficLight = JsonStringBuilder.BuildJsonString();

            jsonBytes = Encoding.UTF8.GetBytes(jsonTrafficLight); // Convert the JSON object to bytes to send over WebSocket connection

            await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);
            Thread.Sleep(3000);

            /* TODO: Think about this solution to see what is better
            * The above just sends the same thing every second for 3 seconds long, the below sends it once
            * and keeps the code from sending something else in those 3 seconds.

            You could also use something like this:
            send GreenState();
            Thread.Sleep(6000); for the 6 seconds of greenlight decided by law
            send OrangeState();
            Thread.Sleep(2000); 2 seconds on orange
            send RedState(); no sleep here as it isn't necessary
            */

        }
        #endregion

        #region SendStartState
        public static async Task<bool> SendStartState(HttpContext context, WebSocket webSocket)
        {
            var jsonTrafficLight = JsonStringBuilder.BuildJsonString();
            var jsonBytes = Encoding.UTF8.GetBytes(jsonTrafficLight);

            await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);
            return false;
        }
        #endregion
    }
}
