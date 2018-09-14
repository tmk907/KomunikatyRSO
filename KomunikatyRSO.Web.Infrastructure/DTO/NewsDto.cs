namespace KomunikatyRSO.Web.Infrastructure.DTO
{
    public class NewsDto
    {
        public NewsDto(string title, string body, int newsId, string provinceSlug, string categorySlug)
        {
            Title = title;
            Body = body;
            NewsId = newsId;
            ProvinceSlug = provinceSlug;
            CategorySlug = categorySlug;
        }

        public string Title { get; }
        public string Body { get; }
        public int NewsId { get; }
        public string ProvinceSlug { get; }
        public string CategorySlug { get; }
    }
}
