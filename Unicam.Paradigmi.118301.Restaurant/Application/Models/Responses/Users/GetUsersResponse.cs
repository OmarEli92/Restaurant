using Application.Models.DTO;
using Models.Entities;


namespace Model.Responses.Users
{
    public class GetUsersResponse
{
        public List<UserDTO> Users { get; set; } = new List<UserDTO>();
        public int NumberOfPages { get; set; }
}
}
