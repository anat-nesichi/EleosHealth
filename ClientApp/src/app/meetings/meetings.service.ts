import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Meeting } from './meeting-list/meeting-list.component';

export class MeetingsService {
  public meetings: Meeting[];

  constructor() { }

  getMeetings(http: HttpClient, baseUrl: string): Observable<Meeting[]> {
    return http.get<Meeting[]>(baseUrl + 'meeting');
  }

  endMeeting(http: HttpClient, baseUrl: string, id: number): Observable<Meeting[]> {
    return http.get<Meeting[]>(baseUrl + 'meeting/endmeeting/' + id);
  }

}

