﻿using Abstractions.Services;
using Application.Abstractions.Services;
using Application.Models.Requests;
using Application.Options;
using Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtAuthenticationOptioons jwtAuthOption;
        private readonly IUserService userService;
        private readonly IHashingService hashingService;
        public TokenService(IOptions<JwtAuthenticationOptioons> jwtAuthOption,
                            IUserService userService,
                            IHashingService hashingService)
        {
            this.jwtAuthOption = jwtAuthOption.Value;
            this.userService = userService;
            this.hashingService = hashingService;

        }
        //Pacchetti nuget necessari  System.IdentityModel.Tokens.Jwt
        public string CreateToken(CreateTokenRequest request)
        {
            //STEP 1 : Verificare esattezza della coppia username/password
            //TODO : Effettuare la verifica
            //STEP 2 : Se username/password corrette creo il token con le claims necessarie
            //TODO : Prendere i parametri dalla configurazione
            //TODO : Prendere le claims dal database
            User user = null;
            if(VerifyCredentials(request.Email, request.Password,out user))
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("User_id", $"{user.UserId}"));
                claims.Add(new Claim("First_name", $"{user.FirstName}"));
                claims.Add(new Claim("Last_name", $"{user.LastName}"));
                claims.Add(new Claim("Email", $"{user.Email}"));
                var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtAuthOption.Key)
                );
                var credentials = new SigningCredentials(securityKey
                    , SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(jwtAuthOption.Issuer
                    , null
                    , claims
                    , expires: DateTime.Now.AddMinutes(90)
                    , signingCredentials: credentials
                    );
                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return token;
            }
            throw new Exception("Credentials not valid!! ");
        }

       
        
    }

}