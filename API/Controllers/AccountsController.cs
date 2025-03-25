using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountsController(Datacontext context,ITokenService tokenService) : BaseController
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto register)
        {
            if (await Checkuserexist(register.Username)){
                return BadRequest("Already taken");
            }
            using var hmca =new HMACSHA512();
            var user = new AppUser
            {
                UserName = register.Username,
                PasswordHash = hmca.ComputeHash(Encoding.UTF8.GetBytes(register.Password)),
                PasswordSalt = hmca.Key
            };
            context.AppUsers.Add(user);
            await context.SaveChangesAsync();
            return new UserDto
            {
                UserName = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }   

        public async Task<bool> Checkuserexist(string username){
            bool user = await context.AppUsers.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] RegisterDto login)
        {
            var user = await context.AppUsers.FirstOrDefaultAsync(x => x.UserName.ToLower() == login.Username.ToLower());
            if (user == null) return Unauthorized("No user");
            using var hmca =new HMACSHA512(user.PasswordSalt);
            var computedHash = hmca.ComputeHash(Encoding.UTF8.GetBytes(login.Password));
            // foreach(var i in computedHash){
            //     if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Wrong password");
            // }
            for(int i = 0; i < computedHash.Length; i++){
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Wrong password");
            }
            return new UserDto
            {
                UserName = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }
    }

    
}
