using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SprintPlannerDashboard.Application.DTOs.FormDtos;
using SprintPlannerDashboard.Application.DTOs.SprintPlannerUserDtos;
using SprintPlannerDashboard.Domain.Entites;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SprintPlannerDashboard.Server.Handlers
{
    public static class SprintPlannerUserHandler
    {
        public static async Task<IResult> RegisterUser([FromServices] UserManager<SprintPlannerUser> userManager,
        [FromBody] RegistrationFormDto registrationFormDto)
        {
            // Maps submitted form details to a new application user record
            SprintPlannerUser user = new SprintPlannerUser()
            {
                Email = registrationFormDto.Email,
                UserName = registrationFormDto.Email,
            };

            // Attempts to securely save the user and hash their password using the Identity manager
            var createdUser = await userManager.CreateAsync(user, registrationFormDto.Password);
            await userManager.AddToRoleAsync(user, registrationFormDto.Role);

            // Returns a 200 OK status if successful, or a 400 Bad Request status with errors if it fails
            if (createdUser.Succeeded)
                return Results.Ok(createdUser);
            else
                return Results.BadRequest(createdUser);
        }

        public static async Task<IResult> LoginUser([FromServices] UserManager<SprintPlannerUser> userManager, [FromBody] LoginFormDto loginDto, [FromServices]IConfiguration config)
        {
            // Finds the user in the database by their email address
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            // Checks if the user exists and if the submitted password is correct
            // Fail early if user doesn't exist or password matches incorrectly
            if (user == null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return TypedResults.BadRequest(new { message = "Email or password is incorrect." });
            }

            // Safely extract token configurations (automatically reads merged User Secrets locally)
            var jwtSecret = config["AppSettings:JWTSecret"] ?? throw new InvalidOperationException("Missing JWTSecret configuration.");
            var issuer = config["AppSettings:Issuer"];
            var audience = config["AppSettings:Audience"];

            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
            var userRoles = await userManager.GetRolesAsync(user);

            var claims = new ClaimsIdentity(new[]
            {
                new Claim("UserID", user.Id.ToString()),
                new Claim("Country", "America"),
                new Claim(ClaimTypes.Role, userRoles.FirstOrDefault() ?? "Unknown")
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return TypedResults.Ok(new { token });
        }

        public static async Task<IResult> DeleteUser([FromServices] UserManager<SprintPlannerUser> userManager, [FromBody] UserDto userDto, IConfiguration _config)
        {
            // Finds the user in the database by their email address
            var user = await userManager.FindByEmailAsync(userDto.Email);

            // Checks if the user exists and if the submitted password is correct
            if (user != null && await userManager.CheckPasswordAsync(user, userDto.Password))
            {

                await userManager.DeleteAsync(user);
                return TypedResults.NoContent();
            }
            // Returns a 400 Bad Request response if the email or password is wrong
            else return Results.BadRequest(new { message = "Email or password is incorrect." });
        }

        [Authorize]
        public static async Task<IResult> GetUserProfile(ClaimsPrincipal userClaims, UserManager<SprintPlannerUser> userManager)
        {
            var userId = userClaims.Claims.FirstOrDefault(c => c.Type == "UserID")!.Value;
            var user = await userManager.FindByIdAsync(userId);

            if (user is not null)
            {
                var userRoles = await userManager.GetRolesAsync(user);

                if (userRoles.Count > 0)
                {
                    return TypedResults.Ok(new
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Role = userRoles[0]
                    });
                }
                else
                {
                    return TypedResults.Ok(new
                    {
                        Id = user.Id,
                        Email = user.Email,
                    });
                }
            }
            else
            {
                return TypedResults.NotFound();
            }
        }
    }
}
