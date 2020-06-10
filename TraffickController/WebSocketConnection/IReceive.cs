using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace TraffickController.WebSocketConnection
{
    public interface IReceive
    {
        Task<bool> ReceiveSocket(HttpContext context, WebSocket webSocket, byte[] buffer);
    }
}
