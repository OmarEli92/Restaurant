using Infrastructure.Context;
using Infrastructure.Repositories;
using Models.Entities;

var dbContext = new MyDBContext();
var userRepo = new UserRepository(dbContext);

var user = new User();
user.Email = "omar@gmail.com";
user.FirstName = "Omar";
user.LastName = "elidrissi";
user.Password = "1234";
userRepo.Add(user);
