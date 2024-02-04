using System;

namespace Yticket.Models;

public class Message
{
    public int id { get; set; }
    public int ticket_id { get; set; }
    public object client_id { get; set; }
    public object user_id { get; set; }
    public string message { get; set; }
    public string client_name { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public Client client { get; set; }
    public User user { get; set; }
}