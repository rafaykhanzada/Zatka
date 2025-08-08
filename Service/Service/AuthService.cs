using AutoMapper;
using Core.Constant;
using Core.Data.DTO;
using Core.Data.Entities;
using Core.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UnitofWork;

namespace Service.Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<User> _logger;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly ResultModel _resultModel;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IMenuService _menuService;
        //private readonly IPermissionService _permissionService;

        public AuthService(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration, TokenValidationParameters tokenValidationParameters, ResultModel resultModel, ILogger<User> logger, IMapper mapper
, IUnitOfWork unitOfWork
            //, IPermissionService permissionService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            //_context = context;
            _configuration = configuration;
            _tokenValidationParameters = tokenValidationParameters;
            _resultModel = resultModel;
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_menuService = menuService;
            //_permissionService = permissionService;
        }
        #region Methods
        //public async Task<ResultModel> Register(UserVM model)
        //{
        //    try
        //    {
        //        var userExists = await _userManager.FindByEmailAsync(model.Email);
        //        if (userExists != null)
        //        {
        //            _resultModel.Success = false;
        //            _resultModel.Message = $"User {model.Email} already exists";
        //            return _resultModel;
        //        }

        //        User newUser = new User()
        //        {
        //            Name = model.Name,
        //            Email = model.Email,
        //            UserName = model.Username,
        //            SecurityStamp = Guid.NewGuid().ToString()
        //        };

        //        var result = await _userManager.CreateAsync(newUser, model.Password);
        //        _resultModel.Success = result.Succeeded;
        //        if (result.Succeeded)
        //        {
        //            //Add user role
        //            switch (model.Roles.FirstOrDefault())
        //            {
        //                case ApplicationRole.Admin:
        //                    await _userManager.AddToRoleAsync(newUser, ApplicationRole.Admin);
        //                    break;
        //                case ApplicationRole.RegisterUser:
        //                    await _userManager.AddToRoleAsync(newUser, ApplicationRole.RegisterUser);
        //                    break;
        //                default:
        //                    break;
        //            }
        //            _resultModel.Message = "User created successfully!";
        //        }
        //        else
        //            _resultModel.Message = "User could not be created.";
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error:", ex);
        //        _resultModel.Success = false;
        //        _resultModel.Message = MessageString.ServerError;
        //    }
        //    return _resultModel;
        //}

        public async Task<ResultModel> Login(LoginVM model)
        {
            try
            {
                string userName = string.Empty;
                User? user = null;
                UserVM uservm = new UserVM();
                if (model.Email.Contains('@'))
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                        user = await _userManager.FindByNameAsync(user?.UserName);
                    else
                        user = await _userManager.FindByEmailAsync(model.Email);
                }
                else
                    user = await _userManager.FindByNameAsync(model.Email);
                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    _logger.LogInformation("User logged in." + model.Email);
                    var tokenValue = await GenerateJWTTokenAsync(user, null);
                    user.Token = tokenValue.Token;
                    user.TokenExpireOn = tokenValue.ExpireAct;
                    user.UpdatedOn = DateTime.Now;
                    await _userManager.UpdateAsync(user);
                    uservm.Roles = await _userManager.GetRolesAsync(user);
                    //var menu = uservm.Roles.Count > 0 ? await _menuService.GetMenu(uservm.Roles.FirstOrDefault()) : null;
                    //var permission = uservm.Roles.Count > 0 ? await _permissionService.GetsPermissionbyRoleAsync(uservm.Roles.FirstOrDefault()) : null;
                    uservm = _mapper.Map<UserVM>(user);

                    _resultModel.Message = "User Login Successfully.";
                    _resultModel.Success = true;
                    _resultModel.Data = new
                    {
                        UserData = uservm,
                        //Menu = menu,
                        //Permission = permission,
                        Token = tokenValue
                    };
                }
                else
                {
                    _logger.LogError(MessageString.InvalidLogin + userName);
                    _resultModel.Success = false;
                    _resultModel.Message = MessageString.InvalidLogin;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error:", ex);
                _resultModel.Success = false;
                _resultModel.Message = MessageString.ServerError;
            }
            return _resultModel;
        }

        public async Task<ResultModel> RefreshToken(TokenRequestVM tokenRequestVM)
        {
            try
            {
                var result = await VerifyAndGenerateTokenAsync(tokenRequestVM);
                _resultModel.Data = result;
                _resultModel.Success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error:", ex);
                _resultModel.Success = false;
                _resultModel.Message = MessageString.ServerError;
            }
            return _resultModel;
        }
        #endregion
        private async Task<AuthResultVM> VerifyAndGenerateTokenAsync(TokenRequestVM tokenRequestVM)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            //var storedToken = await _context.RefreshToken.FirstOrDefaultAsync(x => x.Token == tokenRequestVM.RefreshToken);
            var storedToken = _unitOfWork.RefreshTokenRepository.GetFirst(x => x.Token == tokenRequestVM.RefreshToken);
            var dbUser = await _userManager.FindByIdAsync(storedToken?.UserId);
            try
            {
                var tokenCheckResult = jwtTokenHandler.ValidateToken(tokenRequestVM.Token, _tokenValidationParameters, out var validatedToken);

                return await GenerateJWTTokenAsync(dbUser, storedToken);

            }
            catch (Exception ex)
            {
                if (storedToken?.DateExpire >= DateTime.UtcNow)
                {
                    return await GenerateJWTTokenAsync(dbUser, storedToken);
                }
                else
                {
                    return await GenerateJWTTokenAsync(dbUser, null);
                }
            }
        }

        private async Task<AuthResultVM> GenerateJWTTokenAsync(User user, RefreshToken rToken)
        {
            var authClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        //new Claim(ClaimTypes.Name, user.UserName),
                        //new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        //new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

            //Add User Role Claims
            //var userRoles = await _userManager.GetRolesAsync(user);
            //foreach (var userRole in userRoles)
            //{
            //    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            //}

            var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtTokenSettings:SymmetricSecurityKey"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtTokenSettings:ValidIssuer"],
                audience: _configuration["JwtTokenSettings:ValidAudience"],
                expires: DateTime.UtcNow.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            if (rToken != null)
            {
                var rTokenResponse = new AuthResultVM()
                {
                    Token = jwtToken,
                    RefreshToken = rToken.Token,
                    ExpireAct = token.ValidTo
                };

                return rTokenResponse;
            }

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMinutes(6),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString(),
            };
            await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.CompleteAsync();

            var response = new AuthResultVM()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpireAct = token.ValidTo
            };
            return response;
        }

        //public async Task<ResultModel> ChangePassword([FromBody] AuthPasswordVM model)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByIdAsync(model.UserId);
        //        if (user == null)
        //            _resultModel.Message = MessageString.NotFound;

        //        var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
        //        if (!result.Succeeded)
        //            _resultModel.Message = string.Join(',', result.Errors.Select(e => e.Description));
        //        else
        //        {
        //            _resultModel.Success = true;
        //            _resultModel.Message = MessageString.Success;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error:", ex);
        //        _resultModel.Message = MessageString.ServerError;
        //    }
        //    return _resultModel;
        //}

    }
}
