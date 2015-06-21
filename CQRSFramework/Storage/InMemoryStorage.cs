using System;
using System.Collections.Generic;
using System.Linq;
using CQRSFramework.Services.ApplicationServices;

namespace CQRSFramework.Storage
{
    public class InMemoryStorage : IStorage
    {
        private static readonly List<DiaryItemDto> Items = new List<DiaryItemDto>();

        public DiaryItemDto GetById(Guid id)
        {
            return Items.FirstOrDefault(a => a.Id == id);
        }

        public void Add(DiaryItemDto item)
        {
            Items.Add(item);
        }

        public void Delete(Guid id)
        {
            Items.RemoveAll(i => i.Id == id);
        }

        public List<DiaryItemDto> GetItems()
        {
            return Items;
        }
    }
}
