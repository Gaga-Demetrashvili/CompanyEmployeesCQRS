using Application.Notifications;
using Contracts;
using Entities.Exceptions;
using MediatR;

namespace Application.Handlers;

internal sealed class DeleteCompanyHandler(IRepositoryManager repository) : INotificationHandler<CompanyDeletedNotification>
{
    public async Task Handle(CompanyDeletedNotification notification, CancellationToken cancellationToken)
    {
        var company = await repository.Company.GetCompanyAsync(notification.Id, notification.TrackChanges);

        if (company is null)
            throw new CompanyNotFoundException(notification.Id);

        repository.Company.DeleteCompany(company);
        await repository.SaveAsync();
    }
}
