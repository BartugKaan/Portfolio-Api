using System;
using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IRepositories;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.ApplicationLayer.Feature.AuthFeatures.DTOs;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AuthFeatures.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
  private readonly IAdminRepository _adminRepository;
  private readonly IJwtService _jwtService;
  private readonly IPasswordHasher _passwordHasher;
  private readonly IUnitOfWork _unitOfWork;


  public LoginCommandHandler(
    IAdminRepository adminRepository,
    IJwtService jwtService,
    IPasswordHasher passwordHasher,
    IUnitOfWork unitOfWork)
  {
    _adminRepository = adminRepository;
    _jwtService = jwtService;
    _passwordHasher = passwordHasher;
    _unitOfWork = unitOfWork;
  }

  public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
  {
    var admin = await _adminRepository.GetByUsernameAsync(request.Username, cancellationToken);

    if (admin is null)
      throw new UnauthorizedAccessException("Invalid username or password.");

    if (!_passwordHasher.Verify(request.Password, admin.PasswordHash))
      throw new UnauthorizedAccessException("Invalid username or password.");

    var token = _jwtService.GenerateToken(admin.Id, admin.Username, admin.Email);
    var expiresAt = DateTime.UtcNow.AddMinutes(60); // Token expiration time

    admin.LastLoginAt = DateTime.UtcNow;
    _adminRepository.Update(admin);
    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return new LoginResponse(token, expiresAt);

  }
}
