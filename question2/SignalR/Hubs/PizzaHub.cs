using Microsoft.AspNetCore.SignalR;
using SignalR.Services;

namespace SignalR.Hubs
{
    public class PizzaHub : Hub
    {
        private readonly PizzaManager _pizzaManager;

        public PizzaHub(PizzaManager pizzaManager) {
            _pizzaManager = pizzaManager;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            _pizzaManager.AddUser();

            await Clients.All.SendAsync("UpdateNbUsers", _pizzaManager.NbConnectedUsers);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnConnectedAsync();
            _pizzaManager.RemoveUser();
            await Clients.All.SendAsync("UpdateNbUsers", _pizzaManager.NbConnectedUsers);

        }

        public async Task SelectChoice(PizzaChoice choice)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, choice.ToString());
            await Clients.Caller.SendAsync("UpdateNbPizzasAndMoney", _pizzaManager.GetNbPizzas(choice), _pizzaManager.GetMoney(choice));
            await Clients.Group(choice.ToString()).SendAsync("UpdatePizzaPrice", _pizzaManager.GetPizzaPrice(choice));

        }

        public async Task UnselectChoice(PizzaChoice choice)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, choice.ToString());
        }

        public async Task AddMoney(PizzaChoice choice)
        {
            _pizzaManager.IncreaseMoney(choice);
            await Clients.Group(choice.ToString()).SendAsync("UpdateMoney", _pizzaManager.GetMoney(choice));
        }

        public async Task BuyPizza(PizzaChoice choice)
        {
            _pizzaManager.BuyPizza(choice);
            await Clients.Group(choice.ToString()).SendAsync("UpdateNbPizzasAndMoney", _pizzaManager.GetNbPizzas(choice), _pizzaManager.GetMoney(choice));

        }
    }
}
