using System;

namespace Yticket.Models;

public class Client
{
    public int id { get; set; }
    public int platform_id { get; set; }
    public object identifier { get; set; }
    public string name { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
}