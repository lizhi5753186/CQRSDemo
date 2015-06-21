namespace CQRSFramework.Snapshots
{
    public interface ISnapshotOrignator
    {
        ISnapshot CreateSnapshot();
        void BuildFromSnapshot(ISnapshot snapshot);
    }
}
