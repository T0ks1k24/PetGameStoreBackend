using GameStore.Dtos.User;
using GameStore.Models;

namespace GameStore.Mappers;

public static class UserMappers
{
    public static UserDto ToUserDto(this User userModel)
    {
        return new UserDto
        {
            Id = userModel.Id,
            Name = userModel.Name,
            Email = userModel.Email,
            Password = userModel.PasswordHash,
            Role = userModel.Role,
        };
    }

    public static User ToUserFromCreateDTO(this RegisterUserDto createUser)
    {
        return new User
        {
            Name = createUser.Name,
            Email = createUser.Email,
            PasswordHash = createUser.Password,
        };
    }
}
