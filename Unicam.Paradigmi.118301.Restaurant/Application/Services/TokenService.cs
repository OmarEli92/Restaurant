using Application.Abstractions.Services;
using Application.Models.Requests;
using Application.Options;
using Infrastructure.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtAuthenticationOptioons jwtAuthOption;
        private readonly UserRepository userRepository;
        public TokenService(IOptions<JwtAuthenticationOptioons> jwtAuthOption, UserRepository userRepository)
        {
            this.jwtAuthOption = jwtAuthOption.Value;
            this.userRepository = userRepository;
        }
        //Pacchetti nuget necessari  System.IdentityModel.Tokens.Jwt
        public string CreateToken(CreateTokenRequest request)
        {
            //STEP 1 : Verificare esattezza della coppia username/password
            //TODO : Effettuare la verifica
            //STEP 2 : Se username/password corrette creo il token con le claims necessarie
            //TODO : Prendere i parametri dalla configurazione
            //TODO : Prendere le claims dal database

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("id_utente", "1"));
            claims.Add(new Claim("Nome", "Federico"));
            claims.Add(new Claim("Cognome", "Paoloni"));

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtAuthOption.Key)
                );
            var credentials = new SigningCredentials(securityKey
                , SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(jwtAuthOption.Issuer
                , null
                , claims
                , expires: DateTime.Now.AddMinutes(30)
                , signingCredentials: credentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            //STEP 3 : Restituisco il token
            return token;
        }

        public bool CheckCredentials(string username, string password)
        {
            return true;
        }
    }

}