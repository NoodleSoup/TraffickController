using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace TraffickController.WebSocketConnection
{
    public interface ISend
    {
        Task SendState(HttpContext context, WebSocket webSocket);

        Task<bool> SendStartState(HttpContext context, WebSocket webSocket);
    }
}
