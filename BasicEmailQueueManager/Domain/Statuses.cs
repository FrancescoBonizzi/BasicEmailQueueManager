namespace BasicEmailQueueManager.Domain
{
    public enum Statuses : byte
    {
        New = 0,
        Processing = 1,
        Sent = 2,
        Error = 3
    }
}
