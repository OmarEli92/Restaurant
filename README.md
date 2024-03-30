
# 🍽️ Restaurant API 🌟
Web API for restaurant order management. This API provides functionalities for user creation, authentication, order creation, and order history retrieval that can be downloaded or sent by email in pdf format.
A dump of the db is available inside the main folder of this repository, the chosen db is SQLServer.
## 💡 Usage 
 Every endpoint can be tested through swagger at the URL: https://localhost:7067/swagger/index.html or through a simple web UI at the URL http://localhost:3000/
 
 To use the UI you need nodeJS installed in your machine
## 🚀 Features

### 1.a Customer User Creation with email and password
- This API allows the creation of a new customer user.
- Users can be registered by providing the following information:
  - Email 📧
  - First Name 📛
  - Last Name 📛
  - Password 🔒
- The user's role will automatically be set as "Customer".

### 1.b Customer User Creation with Google API
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
- If the user orders a complete meal, they will receive a 10% discount.
- The complete meal is discounted only if it includes one dish of each type (appetizer, main course, side dish, dessert).

### 4. Order History
- This API allows users to view their order history.
- For Customer users, it will display the list of orders they have placed.
- For Administrator users, it will display the list of all orders placed.
- Streamline search with  filtering options(AND/OR):
  - Date range 📅
  - User ID 🆔
- search results are paginated.

## 🛠️ Endpoints

### Customer User Creation
- **POST** `/api/v1/users/register`

### Authentication
- **POST** `/api/v1/users/login`

### Order Creation
- **POST** `/api/v1/orders/create`

### Order History
- **GET** `/api/v1/orders/history`
  - Parameters:
    - Start Date (required)
    - End Date (required)
    - User Id (optional)
    - Page (optional)
      
### Order History download
- **GET** `/api/v1/orders/history/download `

### Order History sent by email
- **GET** `/api/v1/orders/history/send 





