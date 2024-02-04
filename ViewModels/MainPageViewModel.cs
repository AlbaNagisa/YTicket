using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Avalonia.Threading;
using Newtonsoft.Json;
using ReactiveUI;
using Yticket.Models;
using Yticket.Models.Utils;
using Yticket.Services;

namespace Yticket.ViewModels;

public class MainPageViewModel : ViewModelBase, INotifyCollectionChanged
{
    private ObservableCollection<Ticket> _tickets;
    private string _title = "Aucun ticket sélectionné";
    private ObservableCollection<Message> _messages;
    private string _message;
    private Ticket _selectedTicket = null;
    private bool _isPopupOpen;
    private WebSocketClient _webSocketClient = new WebSocketClient();
    private string _popupMessage = "Veuillez sélectionner un ticket";

    public string PopupMessage
    {
        get => _popupMessage;
        set => this.RaiseAndSetIfChanged(ref _popupMessage, value);
    }

    public bool IsPopupOpen
    {
        get => _isPopupOpen;
        set => this.RaiseAndSetIfChanged(ref _isPopupOpen, value);
    }


    public string Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }

    public Ticket SelectedTicket
    {
        get => _selectedTicket;
        set
        {
            IsPopupOpen = false;
            this.RaiseAndSetIfChanged(ref _selectedTicket, value);
            Title = value.name;
            Messages = value.messages;
        }
    }

    public ObservableCollection<Message> Messages
    {
        get => _messages;
        set
        {
            this.RaiseAndSetIfChanged(ref _messages, value);
            foreach (var message in value)
            {
                if (message.client == null)
                {
                    message.client_name = message.user.name;
                }
                else
                {
                    message.client_name = message.client.name;
                }
            }
        }
    }

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public ObservableCollection<Ticket> Tickets
    {
        get => _tickets;
        set
        {
            value = new ObservableCollection<Ticket>(value.Where(t => t.status_id == 1));
            this.RaiseAndSetIfChanged(ref _tickets, value);
        }
    }


    public override async void Enter()
    {
        try
        {
            var ticketsJson = await Requests.GetInstance().Get("ticket");
            var tickets = JsonConvert.DeserializeObject<List<Ticket>>(ticketsJson);
            Tickets = new ObservableCollection<Ticket>(tickets);
            InitializeWebSocket();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching tickets: {ex.Message}");
        }
    }

    public async void Close()
    {
        if (SelectedTicket == null)
        {
            IsPopupOpen = true;
            return;
        }

        try
        {
            var res = await Requests.GetInstance().Patch($"ticket/{SelectedTicket.id}", new
            {
                status_id = 2
            });
            _webSocketClient.SendMessage(JsonConvert.SerializeObject(new
            {
                platform_id = _selectedTicket.client.platform_id,
                action = "close",
                client_identifier = _selectedTicket.client.identifier.ToString()
            }));
            _tickets.Remove(_selectedTicket);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching tickets: {ex.Message}");
        }
    }

    public async void SubmitCommand()
    {
        if (SelectedTicket == null)
        {
            IsPopupOpen = true;
            return;
        }

        try
        {
            var res = await Requests.GetInstance().Post("message", new
            {
                ticket_id = _selectedTicket.id,
                message = Message,
                user_id = User.GetInstance().id
            });

            var ticket = JsonConvert.DeserializeObject<Ticket>(res);
            var mes = ticket.messages[ticket.messages.Count - 1];
            mes.client_name = mes.user.name;
            _selectedTicket.messages.Add(mes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching tickets: {ex.Message}");
        }

        _webSocketClient.SendMessage(JsonConvert.SerializeObject(new
        {
            _selectedTicket.client.platform_id,
            client_identifier = _selectedTicket.client.identifier.ToString(),
            message = Message
        }));
        Message = "";
    }

    public async void RefreshTicket()
    {
        try
        {
            var ticketsJson = await Requests.GetInstance().Get("ticket");
            var tickets = JsonConvert.DeserializeObject<List<Ticket>>(ticketsJson);
            Tickets = new ObservableCollection<Ticket>(tickets);
            Title = "Aucun ticket sélectionné";
            Messages = new ObservableCollection<Message>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching tickets: {ex.Message}");
        }
    }

    private void OnMessageReceived(string message)
    {
        Console.WriteLine(message);

        var webSocketMessage = JsonConvert.DeserializeObject<WebSocketMessage>(message);

        var ticket = Tickets.FirstOrDefault(t => t.id == webSocketMessage.ticket_id);
        if (ticket != null)
        {
            var newMessage = new Message
            {
                message = webSocketMessage.message,
                client_name = ticket.client.name
            };
            if (ticket.id == _selectedTicket.id)
            {
                ticket.messages.Add(newMessage);
                return;
            }

            ticket.messages.Add(newMessage);
        }
    }


    private async void InitializeWebSocket()
    {
        PopupMessage = "Connection au websocket en cours...";
        IsPopupOpen = true;
        await _webSocketClient.Connect(ENV.WS_URL);
        IsPopupOpen = false;
        PopupMessage = "Veuillez sélectionner un ticket";
        _webSocketClient.MessageReceived += OnMessageReceived;
    }

    public MainPageViewModel()
    {
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
}