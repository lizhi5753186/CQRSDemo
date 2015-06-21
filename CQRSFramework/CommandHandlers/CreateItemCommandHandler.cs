using CQRSFramework.Commands;
using CQRSFramework.Domain;
using CQRSFramework.Repositories;
using System;

namespace CQRSFramework.CommandHandlers
{
    // 对CreateItemCommand处理类
    public class CreateItemCommandHandler : ICommandHandler<CreateItemCommand>
    {
        private readonly IDomainRepository<DiaryItem> _domainRepository;

        public CreateItemCommandHandler(IDomainRepository<DiaryItem> domainRepository)
        {
            _domainRepository = domainRepository;
        }

        // 具体处理逻辑
        public void Execute(CreateItemCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (_domainRepository == null)
            {
                throw new InvalidOperationException("domainRepository is not initialized.");
            }

            var aggregate = new DiaryItem(command.ID, command.Title, command.Description, command.From, command.To)
            {
                Version = -1
            };

            // 将对应的领域实体进行保存
            _domainRepository.Save(aggregate, aggregate.Version);
        }
    }
}
