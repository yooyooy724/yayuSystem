public class NotationManager
{
    public static NotationType type { get; } = NotationType.scientific;
}

public interface NotationGetter
{
    NotationType type { get; }
}

public enum NotationType
{
    named,
    scientific
}