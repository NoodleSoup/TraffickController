using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Newtonsoft.Json;

namespace TraffickController
{
    public class Startup
    {
        private string pathUrl = Program.getPathUrl();
        private byte[] buffer = new byte[1024 * 4];
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole()
                    .AddDebug()
                    .AddFilter<ConsoleLoggerProvider>(category: null, level: LogLevel.Debug)
                    .AddFilter<DebugLoggerProvider>(category: null, level: LogLevel.Debug);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var receive = true;
            var sendStartState = true;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(120),
                ReceiveBufferSize = 4 * 1024,
            };

            webSocketOptions.AllowedOrigins.Any(); // Set app to accept all header formats

            app.UseWebSockets(webSocketOptions);

            #region AcceptWebSocket
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == pathUrl)
                {
                    if (context.WebSockets.IsWebSocketRequest) // Check if the recieve command didn't send 'close' as a disconnect notice
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                        do
                        {
                            if (sendStartState) // Send the start state once when theres a connection
                            {
                                sendStartState = await SendStartState(context, webSocket);
                            }

                            await SendState(context, webSocket);
                            receive = await Receive(context, webSocket);
                        } while (receive);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }

            });
            #endregion
        }

        #region Receive
        private async Task<bool> Receive(HttpContext context, WebSocket webSocket)
        {
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            return false;
        }
        #endregion

        #region SendStates
        private async Task SendState(HttpContext context, WebSocket webSocket)
        {
            var jsonTrafficLight = BuildStateString(); // Start setting up the JSON string builder TODO: Make it send dynamic states
            var jsonMessage = JsonConvert.SerializeObject(jsonTrafficLight, Formatting.None); // Create a proper JSON object from the string
            var jsonBytes = Encoding.UTF8.GetBytes(jsonMessage); // Convert the JSON object to bytes to send over WebSocket connection

            await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);
        }
        #endregion

        #region SendStartState
        private async Task<bool> SendStartState(HttpContext context, WebSocket webSocket)
        {
            var jsonTrafficLight = BuildStateString();
            var jsonMessage = JsonConvert.SerializeObject(jsonTrafficLight, Formatting.None);
            var jsonBytes = Encoding.UTF8.GetBytes(jsonMessage);

            await webSocket.SendAsync(new ArraySegment<byte>(jsonBytes, 0, jsonBytes.Length), 0, true, CancellationToken.None);
            return false;
        }
        #endregion

        #region BuildString
        public string BuildStateString()
        {
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
            int E2 = 0;
            int EF1 = 0;
            int EF2 = 0;
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

            var jsonString = $@"
            {{
                ""A1"": {A1},
                ""A2"": {A2},
                ""A3"": {A3},
                ""A4"": {A4},
                ""AB1"": {AB1},
                ""AB2"": {AB2},
                ""B1"": {B1},
                ""B2"": {B2},
                ""B3"": {B3},
                ""B4"": {B4},
                ""B5"": {B5},
                ""BB1"": {BB1},
                ""C1"": {C1},
                ""C2"": {C2},
                ""C3"": {C3},
                ""D1"": {D1},
                ""D2"": {D2},
                ""D3"": {D3},
                ""E1"": {E1},
                ""E2"": {E2},
                ""EF1"": {EF1},
                ""EF2"": {EF2},
                ""EV1"": {EV1},
                ""EV2"": {EV2},
                ""EV3"": {EV3},
                ""EV4"": {EV4},
                ""FF1"": {FF1},
                ""FF2"": {FF2},
                ""FV1"": {FV1},
                ""FV2"": {FV2},
                ""FV3"": {FV3},
                ""FV4"": {FV4},
                ""GF1"": {GF1},
                ""GF2"": {GF2},
                ""GV1"": {GV1},
                ""GV2"": {GV2},
                ""GV3"": {GV3},
                ""GV4"": {GV4}
            }}";

            return jsonString;
        }
        #endregion
    }
}
