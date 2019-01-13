using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Mappers;
using TimeAnalyzer.Models;

namespace TimeAnalyzer.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await userRepository.GetAll();
            return Ok(users.Select(u => u.ToViewModel()));
        }

        [HttpPost]
        public IActionResult Update([FromBody] UserViewModel userView)
        {
            userRepository.Update(userView.ToDomainModel());
            return Ok();
        }

        [HttpPost]
        public IActionResult Delete([FromBody] UserViewModel userView)
        {
            userRepository.Remove(userView.Id);
            return Ok();
        }
    }
}