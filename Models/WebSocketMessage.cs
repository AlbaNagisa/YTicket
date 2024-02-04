namespace Yticket.Models;

public class WebSocketMessage
{
    public string message { get; set; }
    public int ticket_id { get; set; }
    public string client_name { get; set; }
}