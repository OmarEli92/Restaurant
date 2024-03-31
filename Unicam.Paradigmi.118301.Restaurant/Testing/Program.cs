using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

var ctx = new MyDBContext();
OrderRepository orderRepository = new OrderRepository(ctx);
UserRepository userRepository = new UserRepository(ctx);
var user = new User();
user.Email = "omar@gmail.com";
user.FirstName = "Omar";
user.LastName = "elidrissi";
user.Password = "1234";
userRepository.Add(user);

///
var order = new Order
{
    OrderDate = DateTime.Now,
    DeliveryAddress = "1234 Main St",
    OrderedByUser = user
};

var order1 = new Order
{
    OrderDate = DateTime.Now.AddDays(1),
    DeliveryAddress = "1234 Main St",
    OrderedByUser = user
};

var order2 = new Order
{
    OrderDate = DateTime.Now.AddDays(3),
    DeliveryAddress = "1234 Main St",
    OrderedByUser = user
};

var order3 = new Order
{
    OrderDate = DateTime.Now.AddDays(4),
    DeliveryAddress = "1234 Main St",
    OrderedByUser = user
};

orderRepository.Add(order);
orderRepository.Add(order1);
orderRepository.Add(order2);
orderRepository.Add(order3);