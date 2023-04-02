using Lab10.Server.Dtos;
using Lab10.Server.Models;
using Lab10.Server.Repositories;
using Lab10.Server.Uows;
using System.Text.Json;

namespace Lab10.Server.Services
{
    public interface IAuthService
    {
        Task<Response> Login(User user);
        Task<Response> Register(User user);
    }
    public class AuthService : IAuthService
    {
        IUnitOfWork _uow;

        public AuthService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Response> Login(User user)
        {
            var userInDB = await _uow.UserRepository.GetByUsernameAsync(user.Username);
            var loginTimeout = await _uow.TimeoutRepository.GetByUsernameAsync(user.Username);
            var response = new Response();
            if (userInDB != null)
            {
                if (loginTimeout == null)
                {
                    await _uow.TimeoutRepository.InsertAsync(new LoginTimeout { Tries=0, UserUsername = userInDB.Username});
                    await _uow.SaveChangesAsync();
                    loginTimeout = await _uow.TimeoutRepository.GetByUsernameAsync(userInDB.Username);
                }
                var first = loginTimeout?.Tries < 3;
                var second = loginTimeout?.Expiration < DateTime.Now;
                loginTimeout.Tries = second ? 0 : loginTimeout.Tries;
                if (first || second)
                {
                    if (user.Password == userInDB.Password)
                    {
                        loginTimeout.Tries = 0;
                        loginTimeout.Expiration = null;
                        await _uow.TimeoutRepository.UpdateAsync(loginTimeout);
                        response.Message = "Auth successful";
                    }
                    else
                    {
                        loginTimeout.Tries++;
                        if (loginTimeout.Tries >= 3)
                        {
                            loginTimeout.Expiration = DateTime.Now + TimeSpan.FromSeconds(60);
                        }
                        await _uow.TimeoutRepository.UpdateAsync(loginTimeout);
                        response.Message = "Password mismatch";
                    }
                }
                else
                {
                    response.Message = "Out of tries. Try again in 10 minutes";
                }
            }
            else
            {
                response.Message = "No user with such email";
            }
            await _uow.SaveChangesAsync();
            return response;
        }

        public async Task<Response> Register(User user)
        {
            var userInDB = await _uow.UserRepository.GetByUsernameAsync(user.Username);
            var response = new Response();

            if (userInDB == null)
            {
                await _uow.UserRepository.InsertAsync(user);
                await _uow.SaveChangesAsync();
                response.Message = "Succesfully registered";
            }
            else
            {
                response.Message = "User with such login already exist";

            }
            return response;
        }
    }
}
