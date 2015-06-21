using CQRSFramework.Commands;
using CQRSFramework.Domain;
using CQRSFramework.Repositories;
using System;

namespace CQRSFramework.CommandHandlers
{
    public class ChangeItemCommandHandler : ICommandHandler<ChangeItemCommand>
    {
        private readonly IDomainRepository<DiaryItem> _domainRepository;

        public ChangeItemCommandHandler(IDomainRepository<DiaryItem> domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void Execute(ChangeItemCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (_domainRepository == null)
            {
                throw new InvalidOperationException("domainRepository is not initialized.");
            }

            var aggregate = _domainRepository.GetById(command.ID);

            if (aggregate.Title != command.Title)
                aggregate.ChangeTitle(command.Title);

            if (aggregate.Description != command.Description)
                aggregate.ChangeDescription(command.Description);

            if (aggregate.From != command.From)
                aggregate.ChangeFrom(command.From);

            if (aggregate.To != command.To)
                aggregate.ChangeTo(command.To);

            _domainRepository.Save(aggregate, command.Version);
        }
    }
}
