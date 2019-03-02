﻿using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase 
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForReigsterDto userForRegisterDto)
        {
            if (userForRegisterDto == null)
            {
                throw new ArgumentNullException(nameof(userForRegisterDto));
            }
            //validate the request 
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if(await _repo.UserExists(userForRegisterDto.Username))
            {
                return BadRequest("Username already exists");
            }
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username
            };

            var createduser = await _repo.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);
        }

    }
}