using JobTrackPro.Domain.Entities;

namespace JobTrackPro.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}