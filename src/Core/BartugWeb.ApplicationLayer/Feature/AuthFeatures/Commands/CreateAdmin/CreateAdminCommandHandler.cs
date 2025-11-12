using BartugWeb.ApplicationLayer.Abstracts;
using BartugWeb.ApplicationLayer.Abstracts.IServices;
using BartugWeb.DomainLayer.Entities;
using MediatR;

namespace BartugWeb.ApplicationLayer.Feature.AuthFeatures.Commands.CreateAdmin;

public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, string>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public CreateAdminCommandHandler(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var existingAdmin = await _unitOfWork.AdminRepository.GetByUsernameAsync(request.Username, cancellationToken);
        if (existingAdmin is not null)
        {
            throw new InvalidOperationException($"Admin with username '{request.Username}' already exists.");
        }

        var admin = new Admin
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = _passwordHasher.Hash(request.Password),
            Email = "admin@bartugweb.com", // Placeholder email
            CreatedAt = DateTime.UtcNow,
            LastLoginAt = null
        };

        await _unitOfWork.AdminRepository.AddAsync(admin, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return $"Admin user '{admin.Username}' created successfully.";
    }
}