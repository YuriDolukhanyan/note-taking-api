﻿namespace note_taking_api.Models
{
    public class CreateNoteItemDto
    {
        public required string Description { get; set; }
        public required DateTime Created { get; set; }
        public required DateTime Updated { get; set; }
    }
}
