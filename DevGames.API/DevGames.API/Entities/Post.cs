﻿namespace DevGames.API.Entities
{
    public class Post
    {
        public Post(string title, string description, int boardId)
        {
            BoardId = boardId;
            Title = title;
            Description = description;

            CreatedAt = DateTime.Now;
            Comments = new List<Comment>();
        }

        public int Id { get; private set; }
        public string Title { get; set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; set; }
        public int BoardId { get; private set; }
        public List<Comment> Comments { get; private set; }

        internal void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }
    }
}
