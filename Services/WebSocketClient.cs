using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Yticket.Services;

public class WebSocketClient
{
    private ClientWebSocket _client = new ClientWebSocket();

    public delegate void MessageReceivedHandler(string message);

    public event MessageReceivedHandler MessageReceived;

    public async Task Connect(string uri)
    {
        try
        {
            await _client.ConnectAsync(new Uri(uri), CancellationToken.None);
            await SendMessage(JsonConvert.SerializeObject(new
            {
                type = "client",
            }));

            Task.Run(() => ReceiveMessages());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Une erreur s'est produite lors de la connexion : {ex.Message}");
        }
    }

    private async Task ReceiveMessages()
    {
        var buffer = new byte[1024];
        Console.WriteLine(WebSocketState.Open);

        var result = await _client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
        var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Console.WriteLine(message);
        Task.Run(() => ReceiveMessages());
        MessageReceived?.Invoke(message);
    }


    public async Task SendMessage(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);
        await _client.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true,
            CancellationToken.None);
    }
}