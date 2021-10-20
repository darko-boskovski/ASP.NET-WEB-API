using Lotto_3000_App.DataAccess.Interfaces;
using Lotto_3000_App.Domain.Models;
using Lotto_3000_App.Models;
using Lotto_3000_App.Mappers;
using Lotto_3000_App.Models.Users;
using Lotto_3000_App.Services.Interfaces;
using Lotto_3000_App.Shared.CustomEntities;
using Lotto_3000_App.Shared.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Lotto_3000_App.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<AppSettings> _options;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _options = options;
        }

        public void AddUser(UserModel userModel)
        {

            if (string.IsNullOrEmpty(userModel.Username))
            {
                throw new UserException("The property UserName for user is required");
            }
            if (userModel.Username.Length > 100)
            {
                throw new UserException("The property Username can't contain more then 100 characters");
            }
            if (userModel.Username.Length > 50)
            {
                throw new UserException("The property UserName can't contain more then 10 characters");
            }
            if (userModel.Id != 0)
            {
                throw new UserException("Id must not be set!");
            }

            User userForDb = userModel.ToUser();
            _userRepository.Add(userForDb);
        }

        public void DeleteUser(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new NotFoundException($"The User with id {id} was not found");
            }
            _userRepository.Delete(userDb);
        }

        public List<UserModel> GetAllUsers()
        {
            List<User> usersDb = _userRepository.GetAll();
            List<UserModel> userModels = new List<UserModel>();
            foreach (User user in usersDb)
            {
                userModels.Add(user.ToUserModel());
            }
            return userModels;
        }

        public UserModel GetUserById(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new NotFoundException($"The User with id {id} was not found");
            }

            return userDb.ToUserModel();
        }

        public string Login(LoginModel loginUserModel)
        {
            var md5 = new MD5CryptoServiceProvider();
         
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(loginUserModel.Password));
        
            var hashedPassword = Encoding.ASCII.GetString(md5Data);
         
            User userDb = _userRepository.LoginUser(loginUserModel.Username, hashedPassword);
            if (userDb == null)
            {
                throw new NotFoundException($"User with username {loginUserModel.Username} cannot be found");
            }
       
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);
     
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(7),
       
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
              
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name,$"{userDb.Firstname} {userDb.Lastname}"),
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString(), userDb.Role.ToString())
                    }
                )
            };
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            string tokenString = jwtSecurityTokenHandler.WriteToken(token);
            return tokenString;
        }

        public void Register(RegisterModel registerUserModel)
        {
            ValidateUser(registerUserModel);

            var md5 = new MD5CryptoServiceProvider();

            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(registerUserModel.Password));

            var hashedPassword = Encoding.ASCII.GetString(md5Data);


            User newUser = new User
            {
                Firstname = registerUserModel.Firstname,
                Lastname = registerUserModel.Lastname,
                Username = registerUserModel.Username,
                Password = hashedPassword,
                Role = (int)registerUserModel.Role
                
            };
            _userRepository.Add(newUser);
        }


        private void ValidateUser(RegisterModel registerUserModel)
        {
            if (string.IsNullOrEmpty(registerUserModel.Username) || string.IsNullOrEmpty(registerUserModel.Password))
            {
                throw new UserException("Username and password are required fields");
            }
            if (registerUserModel.Username.Length > 30)
            {
                throw new UserException("Username can contain max 30 characters");
            }
            if (registerUserModel.Firstname.Length > 50 || registerUserModel.Lastname.Length > 50)
            {
                throw new UserException("Firstname and Lastname can contain maximum 50 characters!");
            }
            if (!IsUserNameUnique(registerUserModel.Username))
            {
                throw new UserException("A user with this username already exists!");
            }
            if (registerUserModel.Password != registerUserModel.ConfirmedPassword)
            {
                throw new UserException("The passwords do not match!");
            }
            if (!IsPasswordValid(registerUserModel.Password))
            {
                throw new UserException("The password is not complex enough!");
            }
        }

        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }

        private bool IsUserNameUnique(string username)
        {
            return _userRepository.GetUserByUsername(username) == null;
        }


        public void UpdateUser(UserModel userModel)
        {
            User userDb = _userRepository.GetById(userModel.Id);
            if (userDb == null)
            {
                throw new NotFoundException($"The User with id {userModel.Id} was not found!");
            }
            if (string.IsNullOrEmpty(userModel.Username))
            {
                throw new UserException("The property Username is required");
            }
            if (userModel.Username.Length > 50)
            {
                throw new UserException("The property Username can not contain more than 50 characters");
            }
            if (!string.IsNullOrEmpty(userModel.Firstname) && userModel.Firstname.Length > 50)
            {
                throw new UserException("The property FirstName can not contain more than 30 characters");
            }

            _userRepository.Update(UserMapper.ToUser(userModel));
        }


    }
}
