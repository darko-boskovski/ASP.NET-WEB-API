using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using WebApi_Workshop.DataAccess;
using WebApi_Workshop.DataAccess.Interfaces;
using WebApi_Workshop.Domain.Models;
using WebApi_Workshop.Helpers;
using WebApi_Workshop.Mappers;
using WebApi_Workshop.Models;
using WebApi_Workshop.Models.Users;
using WebApi_Workshop.Services.Interfaces;
using WebApi_Workshop.Shared.Exceptions;
using WebApi_Workshop_Class05.Models;

namespace WebApi_Workshop.Services.Implementations
{
    public class UserService : IUserService
    {
        private IRepository<Movie> _movieRepository;
        private IUserRepository _userRepository;
        private IOptions<AppSettings> _options;

        public UserService(IRepository<Movie> movieRepository, IUserRepository userRepository, IOptions<AppSettings> options)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
            _options = options;
        }

        public void AddUser(UserModel userModel)
        {

            if (string.IsNullOrEmpty(userModel.Username))
            {
                throw new MovieException("The property UserName for user is required");
            }
            if (userModel.Username.Length > 100)
            {
                throw new MovieException("The property Username can't contain more then 100 characters");
            }
            if (userModel.Username.Length > 50)
            {
                throw new MovieException("The property UserName can't contain more then 10 characters");
            }
            if (userModel.Id != 0)
            {
                throw new MovieException("Id must not be set!");
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

        public string Login(LoginUserModel loginUserModel)
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
                            new Claim(ClaimTypes.Name, userDb.Username),
                            new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                            new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),
                    }
                )
            };
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            string tokenString = jwtSecurityTokenHandler.WriteToken(token);
            return tokenString;
        }

        public void Register(RegisterUserModel registerUserModel)
        {
            ValidateUser(registerUserModel);
         
            var md5 = new MD5CryptoServiceProvider();
        
            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(registerUserModel.Password));
        
            var hashedPassword = Encoding.ASCII.GetString(md5Data);

            
            User newUser = new User
            {
                FirstName = registerUserModel.FirstName,
                LastName = registerUserModel.LastName,
                Username = registerUserModel.Username,
                Password = hashedPassword
            };
            _userRepository.Add(newUser);
        }

        public void UpdateUser(UserModel userModel)
        {
            User userDb = _userRepository.GetById(userModel.Id);
            if (userDb == null)
            {
                throw new NotFoundException($"The User with id {userModel.Id} was not found!");
            }
            Movie movieDb = _movieRepository.GetById(userModel.Id);
            if (movieDb == null)
            {
                throw new NotFoundException($"The movie with id {userModel.Id} was not found");
            }
            if (string.IsNullOrEmpty(userModel.Username))
            {
                throw new MovieException("The property Username for Movie is required");
            }
            if (userModel.Username.Length > 50)
            {
                throw new MovieException("The property Username can not contain more than 50 characters");
            }
            if (!string.IsNullOrEmpty(userModel.FirstName) && userModel.FirstName.Length > 50)
            {
                throw new MovieException("The property FirstName can not contain more than 30 characters");
            }

            _userRepository.Update(UserMapper.MapUser(userDb, userModel,movieDb));
        }


        private void ValidateUser(RegisterUserModel registerUserModel)
        {
            if (string.IsNullOrEmpty(registerUserModel.Username) || string.IsNullOrEmpty(registerUserModel.Password))
            {
                throw new UserException("Username and password are required fields");
            }
            if (registerUserModel.Username.Length > 30)
            {
                throw new UserException("Username can contain max 30 characters");
            }
            if (registerUserModel.FirstName.Length > 50 || registerUserModel.LastName.Length > 50)
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

    }
}
