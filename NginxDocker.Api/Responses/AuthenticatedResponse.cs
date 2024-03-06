using System;
namespace NginxDocker.Api.Responses
{
	public class AuthenticatedResponse
	{
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}

