using GameStore.Dtos.Order;
using GameStore.Dtos.OrderItemsDto;
using GameStore.Interfaces.IRepository;
using GameStore.Interfaces.IServices;
using GameStore.Models;

namespace GameStore.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserRepository _userRepository;

    public OrderService(IOrderRepository orderRepository, IGameRepository gameRepository, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _gameRepository = gameRepository;
        _userRepository = userRepository;
    }

    public async Task<OrderDto> CreateOrderAsync(int userId, List<int> gameIds)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) throw new ArgumentException("User not found!");

        var games = new List<Game>();
        decimal totalPrice = 0;

        foreach (var gameId in gameIds) 
        { 
            var game = await _gameRepository.GetByIdAsync(gameId);
            if (game == null) throw new ArgumentException($"Гра з ID {gameId} не знайдена");

            games.Add(game);
            totalPrice += game.Price;
        }

        var order = new Order
        {
            UserId = userId,
            TotalPrice = totalPrice,
            OrderItems = games.Select(g => new OrderItem
            {
                GameId = g.Id,
                Price = g.Price
            }).ToList()
        };

        await _orderRepository.CreateAsync(order);

        return new OrderDto
        {
            UserName = order.User.Name,
            TotalPrice = order.TotalPrice,
            OrderDate = order.OrderDate,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                GameName = oi.Game.Name,
                Price = oi.Price
            }).ToList()
        };

    }
    public async Task<IEnumerable<OrderDto>> GetOrdersAsync(int userId)
    {
        var orders = await _orderRepository.GetByUserIdAsync(userId);

        return orders.Select(o => new OrderDto
        {
            Id = o.Id,
            UserName = o.User.Name,
            TotalPrice = o.TotalPrice,
            OrderDate = o.OrderDate,
            OrderItems = o.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                GameName = oi.Game.Name,
                Price = oi.Price,
            }).ToList(),
        }).ToList();
    }

    public async Task<OrderDto?> GetOrderDetailsAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync(orderId);
        if (order == null) return null;

        return new OrderDto
        {
            Id = order.Id,
            UserName = order.User.Name,
            TotalPrice = order.TotalPrice,
            OrderDate = order.OrderDate,
            OrderItems = order.OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id,
                OrderId = oi.OrderId,
                GameName = oi.Game.Name,
                Price = oi.Price,
            }).ToList()

        };
    }
}
