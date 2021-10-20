using System;

namespace SimpleNote.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
