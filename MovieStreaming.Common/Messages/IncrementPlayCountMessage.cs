namespace MovieStreaming.Common.Messages
{
    public class IncrementPlayCountMessage
    {
        public IncrementPlayCountMessage(string movieTitle)
        {
            MovieTitle = movieTitle;
        }

        public string MovieTitle { get; private set; }
    }
}
