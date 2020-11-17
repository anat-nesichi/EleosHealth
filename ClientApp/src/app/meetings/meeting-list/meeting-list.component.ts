import { Component, Inject, OnInit, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MeetingsService } from '../meetings.service';

@Component({
  selector: 'meeting-list',
  templateUrl: './meeting-list.component.html',
  styleUrls: ['./meeting-list.component.css'],
  providers: [MeetingsService]
})
export class MeetingListComponent {
  public meetings: Meeting[];
  httpClient: HttpClient;
  url: string = "";
  endedMeetingId: number;

  constructor(private service: MeetingsService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.httpClient = http;
    this.url = baseUrl;
    this.endedMeetingId = 0;
  }

  ngOnInit() {
    this.service.getMeetings(this.httpClient, this.url)
      .subscribe((result) => {
        this.meetings = result;
      },
        error => {
          console.log("getMeetings() returned with an error", error);
        },
      );
  }

  endMeeting(meetingId: number) {
    this.service.endMeeting(this.httpClient, this.url, meetingId)
      .subscribe((result) => {
        this.meetings = result;
      },
        error => {
          console.log("endMeeting() returned with an error", error);
        },
      );
  }
}


export class Meeting {
  id: number;
  participants: number;
}
