namespace DevGames.API.Entities
{
    public class Comment
    {
        public Comment(string title, string description, string user, int postId)
        {
            PostId = postId;
            Title = title;
            Description = description;
            User = user;

            CreatedAt = DateTime.Now;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; set; }
        public string User { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int PostId { get; private set; }
    }
}
