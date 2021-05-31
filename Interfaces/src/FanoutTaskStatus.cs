namespace Interfaces
{
    public record FanoutTaskStatus(
        int NumberOfSuccessfulTasks,
        int NumberOfFailedTasks);
}