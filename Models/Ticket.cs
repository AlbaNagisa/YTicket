using System;
using System.Collections.ObjectModel;
using Yticket.Models;
public class Ticket
{
    public int id { get; set; }
    public string name { get; set; }
    public int status_id { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public object channel_id { get; set; }
    public ObservableCollection<Message> messages { get; set; }
    public Client client { get; set; }
    public Status status { get; set; }
}

