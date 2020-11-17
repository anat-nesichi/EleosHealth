
namespace EleosHealth
{
    public class ApiMeetingResult
    {
        public string From { get; set; }
        public string To { get; set; }
        public int Page_count { get; set; }
        public int Page_size { get; set; }
        public int Total_records { get; set; }
        public string Next_page_token { get; set; }
        public Meeting[] Meetings { get; set; }
    }
}
