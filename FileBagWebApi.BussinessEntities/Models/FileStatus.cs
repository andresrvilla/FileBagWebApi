namespace FileBagWebApi.Entities.Models
{
    public enum FileStatus
    {
        Uploaded = 1,
        ForRevision = 2,
        Approved = 3,
        Deleted = 4,
        Corrupt = 5
    }
}