
# 🍽️ Restaurant API 🌟
Web API for restaurant order management. This API provides functionalities for user creation, authentication, order creation, and order history retrieval.  
A dump of the db is available inside the Infrastructure assembly of this repository, the chosen db is SQLServer.
## 💡 Usage 
 Every endpoint can be tested through Swagger at the URL: https://localhost:7067/swagger/index.html  
 or through a simple web UI at the URL http://localhost:5500/index.html
 
 ### Login Credentials:
``` json
{
  "email": "admin@gmail.com",
  "password": "Admin1234!"
}
{
  "email": "andrea_bianchi@gmail.com",
  "password": "Andrea1!"
}
{
  "email": "mario_rossi@gmail.com",
  "password": "1234Abcd!"
}
```
### Usage example : Add a new order ->
``` json
{
  "deliveryAddress": "Roma, Contrada Salita dei Leoni 123",
  "orderedDishes": [
    {
      "name": "Carbonara",
      "quantity": 2,
      "price": 13,
      "type": 0
    },
    {
      "name": "Cotoletta di manzo",
      "quantity": 1,
      "price": 14,
      "type": 1
    },
     {
      "name": "Bistecca ai ferri",
      "quantity": 1,
      "price": 18,
      "type": 1
    },
    {
      "name": "Insalata con pomodori e carote",
      "quantity": 1,
      "price": 7,
      "type": 2
    },
    {
      "name": "Tiramisu",
      "quantity": 2,
      "price": 9,
      "type": 3
    }
  ]
}
```
## 🚀 Features

### 1.a Customer User Creation with email and password
- This API allows the creation of a new customer user.
- Users can be registered by providing the following information:
  - Email 📧
  - First Name 📛
  - Last Name 📛
  - Password 🔒
- The user's role will automatically be set as "Customer".

### 1.b Customer User Creation with Google API(not yet implemented)
- This API allows the creation of a new customer user.
- Users can be registered by providing the following information:
  - Email 📧
  - First Name 📛
  - Last Name 📛
  - Password 🔒
- The user's role will automatically be set as "Customer".

### 2. Authentication
- Provides an endpoint for user authentication.
- Authentication via email and password.
- Authentication via oAuth 2.0 through Google API.

### 3. Order Creation
- Allows authenticated users to place a new order.
-The order includes the following information:
  - User details 🧑‍🍳
  - Order date 📅
  - Unique order number 📝
  - Delivery address 🚚
  - ordered dishes 🍲

#### Complete Meal Discount 💰
- When the user orders a complete meal, they will receive a 10% discount.
- The complete meal is discounted only if it includes one dish of each type (appetizer, main course, side dish, dessert).

### 4. Order History
- This API allows users to view their order history.
- For Customer users, it will display the list of orders they have placed.
- For Administrator users, it will display the list of all orders placed.
- Streamline search with  filtering options(AND/OR):
  - Date range 📅
  - User ID 🆔
- Search results are paginated.

## 🛠️ Endpoints

### Customer User Creation
- **POST** `/api/v1/Auth/Register`

### Authentication
- **POST** `/api/v1/Auth/Login`

### Order Creation
- **POST** `/api/v1/Orders/Add`

### Order History
- **GET** `/api/v1/Orders/history`
  - Parameters:
    - Start Date (required)
    - End Date (required)
    - User Id (optional)
    - Page (optional)
      







