import * as React from 'react';
import AuthorizeHttpRequestService from './AuthorizeHttpRequestService';
import TimeConverterService from './TimeConverterService';
import axios from "axios"

export default class TimeReportApiService extends React.Component<any, any>{

    authorizedApi: AuthorizeHttpRequestService;
    timeReportApiService: TimeConverterService;

    constructor() {
        super();
        this.authorizedApi = new AuthorizeHttpRequestService();
        this.timeReportApiService = new TimeConverterService();
    }

    getAllUserTimeReports() {
        return this.authorizedApi.authorizedGet('/api/TimeReport/GetUserTimeReports', null)
            .then((response: any) => {
                return Promise.resolve(response);
            });
    }

    getDayUserTimeReports(date: any) {
        var jsonDate = this.timeReportApiService.toServerFormatDate(date);
        return this.authorizedApi.authorizedGet('/api/TimeReport/GetDayTimeReports', [jsonDate]);
    }

    getUserTimeReportsInInterval(startDate: any, endDate: any) {
        var jsonStartDate = this.timeReportApiService.toServerFormatDate(startDate);
        var jsonEndDate = this.timeReportApiService.toServerFormatDate(endDate);
        return this.authorizedApi.authorizedGet('/api/TimeReport/GetTimeReportsInInterval', [jsonStartDate, jsonEndDate]);
    }

    addTimeReport(jsDate: any, Duration: any, ActivityId: any) {
        var date = this.timeReportApiService.toServerFormatDate(jsDate);
        return this.authorizedApi.authorizedPost(`/api/TimeReport/AddTimeReport`, {
            date,
            Duration,
            ActivityId
        });
    }

    updateTimeReport(id: any, jsDate: any, Duration: any, ActivityId: any) {
        var date = this.timeReportApiService.toServerFormatDate(jsDate);
        return this.authorizedApi.authorizedPost(`/api/TimeReport/UpdateTimeReport`, {
            id,
            date,
            Duration,
            ActivityId
        });
    }
}