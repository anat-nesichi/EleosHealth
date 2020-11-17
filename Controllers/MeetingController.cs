using System.Collections.Generic;
using EleosHealth.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EleosHealth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingRepository _meetingRepository;
        public MeetingController(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        [HttpGet]
        public List<Meeting> Get()
        {
            return _meetingRepository.GetAccountLiveMeetings();
        }


        [HttpGet("endmeeting/{id}")]
        public List<Meeting> EndMeeting(long id)
        {
            return _meetingRepository.EndMeeting(id);
        }
    }
}
