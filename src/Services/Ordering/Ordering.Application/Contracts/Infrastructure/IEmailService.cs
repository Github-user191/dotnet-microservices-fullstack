using Ordering.Application.Contracts.Models;
namespace Ordering.Application.Contracts.Infrastructure; 

// Abstraction of our Infrastructure layer
public interface IEmailService {
    Task<bool> SendEmail(Email email);
}