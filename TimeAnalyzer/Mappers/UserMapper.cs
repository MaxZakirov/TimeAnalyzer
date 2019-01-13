using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Models.Users;
using TimeAnalyzer.Models;

namespace TimeAnalyzer.Mappers
{
    public static class UserMapper
    {
        public static User ToDomainModel(this UserViewModel userViewModel)
        {
            return new User {
                Id = userViewModel.Id,
                Email = userViewModel.Email,
                Name = userViewModel.Name
            };
        }

        public static UserViewModel ToViewModel(this User domainModel)
        {
            return new UserViewModel(
                domainModel.Id,
                domainModel.Name,
                domainModel.Email);
        }
    }
}
