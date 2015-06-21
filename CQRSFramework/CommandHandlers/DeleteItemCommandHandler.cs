using CQRSFramework.Commands;
using CQRSFramework.Domain;
using CQRSFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSFramework.CommandHandlers
{
    public class DeleteItemCommandHandler : ICommandHandler<DeleteItemCommand>
    {
        private readonly IDomainRepository<DiaryItem> _domainRepository;

        public DeleteItemCommandHandler(IDomainRepository<DiaryItem> domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public void Execute(DeleteItemCommand command)
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
            aggregate.Delete();
            _domainRepository.Save(aggregate, aggregate.Version);
        }
    }
}
