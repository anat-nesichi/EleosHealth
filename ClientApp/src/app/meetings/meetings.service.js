"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.MeetingsService = void 0;
var MeetingsService = /** @class */ (function () {
    function MeetingsService() {
    }
    MeetingsService.prototype.getMeetings = function (http, baseUrl) {
        return http.get(baseUrl + 'meeting');
    };
    MeetingsService.prototype.endMeeting = function (http, baseUrl, id) {
        return http.get(baseUrl + 'meeting/endmeeting/' + id);
    };
    return MeetingsService;
}());
exports.MeetingsService = MeetingsService;
//# sourceMappingURL=meetings.service.js.map