using System;
using System.Collections.Generic;
using CQRSFramework.Services.ApplicationServices;

namespace CQRSFramework.Storage
{
    public interface IStorage
    {
        DiaryItemDto GetById(Guid id);
        void Add(DiaryItemDto item);
        void Delete(Guid id);
        List<DiaryItemDto> GetItems();
    }
}
