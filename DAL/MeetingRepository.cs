using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EleosHealth.DAL
{
    public interface IMeetingRepository : IDisposable
    {
        List<Meeting> GetAccountLiveMeetings();
        List<Meeting> EndMeeting(long id);
    }
    public class MeetingRepository : IMeetingRepository
    {
        private Context _dbContext = null;
        private const string _apiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJhdWQiOm51bGwsImlzcyI6IkZySnh5TkFSU3VxbnpUeF92ekRaSmciLCJleHAiOjEwNDEzNzg4NDYwLCJpYXQiOjE1OTY3MDEzODV9.CeBXFRnHcgAmjqohODTSExJl7qa7msacZe0tlQ8BCfs";

        public MeetingRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Meeting> GetAccountLiveMeetings()
        {
            GetAndSaveAccountMeetings();
            if (!_dbContext.Meetings.Any())
            {
                return new List<Meeting>();
            }

            return _dbContext.Meetings.OrderBy(m => m.Id).ToList();
        }

        public List<Meeting> EndMeeting(long id)
        {
            string[] header = { "content-type", "application/json" };
            string[] parameter = { "application/json", "{\"action\":\"end\"}" };
            var response = GetRequestResponse("https://api.zoom.us/v2/meetings/" + id + "/status", Method.PUT, header, parameter);
            if (response.IsSuccessful)
            {
                var meetingToDelete = _dbContext.Meetings.Where(m => m.Id == id).FirstOrDefault();
                if (meetingToDelete != null)
                {

                    _dbContext.Remove(meetingToDelete);
                    _dbContext.SaveChanges();
                }
            }

            return _dbContext.Meetings.OrderBy(m => m.Id).ToList();
        }

        private void GetAndSaveAccountMeetings()
        {
            var response = GetRequestResponse("https://api.zoom.us/v2/metrics/meetings?page_size=30&type=live", Method.GET, null, null);
            var res = JsonConvert.DeserializeObject<ApiMeetingResult>(response.Content);

            bool isDBEmpty = !_dbContext.Meetings.Any() && _dbContext.Meetings.Count() == 0;
            foreach (var meeting in res.Meetings)
            {
                Meeting meetingFromDB = new Meeting();
                if (!isDBEmpty)
                {
                    meetingFromDB = _dbContext.Meetings.Where(m => m.Id == meeting.Id).FirstOrDefault();
                }
               
                if (isDBEmpty || (!isDBEmpty && meetingFromDB == null))
                {
                    _dbContext.Add(new Meeting()
                    {
                        Id = meeting.Id,
                        Participants = meeting.Participants
                    });
                }
            }

            _dbContext.SaveChanges();
        }

        private IRestResponse GetRequestResponse(string requestUrl, Method method, string[] header, string[] parameter)
        {
            var client = new RestClient(requestUrl);
            var request = new RestRequest(method);
            request.AddHeader("authorization", "Bearer " + _apiKey);
            if (header != null)
            {
                request.AddHeader(header[0], header[1]);
            }
            if (parameter != null)
            {
                request.AddParameter(parameter[0], parameter[1], ParameterType.RequestBody);
            }
            IRestResponse response = client.Execute(request);

            return response;
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
