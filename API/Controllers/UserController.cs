using API.Controllers;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API;
public class UserController(Datacontext context) : BaseController
{
    public readonly Datacontext _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetAllUsers(){
        var Users = await _context.AppUsers.ToListAsync();
        return Ok(Users);
    }

    [Authorize]
    [HttpGet("{id:int}")]   
    public async Task<ActionResult<AppUser>> GetAllUsers(int id)
    {
        var Users = await _context.AppUsers.FindAsync(id);
        if(Users==null){
            return BadRequest("Not Found");
        }
        return Ok(Users);
    }
}