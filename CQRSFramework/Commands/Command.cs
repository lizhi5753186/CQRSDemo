using System;

namespace CQRSFramework.Commands
{
    [Serializable]
    public abstract class Command :ICommand
    {
        public Guid ID { get; private set; }

        public int Version { get; private set; }

        protected Command(Guid id, int version)
        {
            ID = id;
            Version = version;
        }
    }
}
