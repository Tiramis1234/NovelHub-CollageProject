namespace server.Dtos
{

    public class ChapterReadDto
    {
        public int Id { get; set; }
        public int ChapterNumber { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        

        public string NovelTitle { get; set; } 
        

        public int? NextChapterNumber { get; set; }
        public int? PrevChapterNumber { get; set; }
    }

  
    public class NovelDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public List<ChapterListItemDto> Chapters { get; set; }
    }


    public class ChapterListItemDto
    {
        public int ChapterNumber { get; set; }
        public string Title { get; set; }
    }
}