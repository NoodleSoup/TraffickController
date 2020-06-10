using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using TraffickController.JsonStrings;
using TraffickController.TrafficLight;
using TraffickController.WebSocketConnection;

namespace TraffickController
{
    public class Startup
    {
        private byte[] _buffer = new byte[1024 * 6]; // Amount of byes per websocket request
        private WebSocket _webSocket;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole()
                    .AddDebug()
                    .AddFilter<ConsoleLoggerProvider>(category: null, level: LogLevel.Debug)
                    .AddFilter<DebugLoggerProvider>(category: null, level: LogLevel.Debug);
            });

            services.AddSingleton<ISend, Send>();
            services.AddSingleton<IReceive, Receive>();
            services.AddSingleton<IValidateJson, ValidateJson>();
            services.AddSingleton<IData, Data>();
            services.AddSingleton<IPresetLights, PresetLights>();
            services.AddSingleton<IJsonStringBuilder, JsonStringBuilder>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ISend send, IReceive receive)
        {
            var receiveSocket = true;
            var sendStartState = true;
            Task receiveTask = null;
            Task sendTask = null;
            bool connected = false;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var webSocketOptions = new WebSocketOptions()
            {
                KeepAliveInterval = TimeSpan.FromSeconds(300),
                ReceiveBufferSize = 6 * 1024,
            };

            app.UseWebSockets(webSocketOptions);

            #region AcceptWebSocket
            app.Use(async (context, next) =>
            {
                bool firstTime = true;

                if (!connected)
                {
                    _webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    connected = true;
                }

                do
                {
                    if (sendStartState) // Send the start state once when there's a connection
                    {
                        sendStartState = await send.SendStartState(context, _webSocket);
                    }

                    if (sendTask == null || sendTask.IsCompleted) //Note: Performancewise this is faster then no await
                    {
                        sendTask = Task.Run(async () => await send.SendState(context, _webSocket));
                    }

                    if(receiveTask == null || receiveTask.IsCompleted)
                    {
                        receiveTask = Task.Run(async () => receiveSocket = await receive.ReceiveSocket(context, _webSocket, _buffer));
                        if (firstTime)
                        {
                            Thread.Sleep(50);
                            firstTime = false;
                        }
                    }

                    if (_webSocket.State == WebSocketState.Closed) // Handle websocket closing
                    {
                        await _webSocket.CloseAsync(new WebSocketCloseStatus(), "Client disconnected.", new CancellationToken());
                        sendStartState = true;
                        connected = false;
                        break;
                    }
                } while (receiveSocket);

                // Cleanup tasks
                sendTask.Dispose();
                receiveTask.Dispose();
                sendStartState = true;
                connected = false;

            });
            #endregion
        }
    }
}
