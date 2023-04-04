namespace InputReader
{
    public interface IEntityInputSource
    {
        float HorizontalDirection { get; }
        float VerticalDirection { get; }
        bool Jump { get; }

        void ResetOneTimeActions();
    }
}