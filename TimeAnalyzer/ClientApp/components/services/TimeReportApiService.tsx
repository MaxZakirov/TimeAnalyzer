import * as React from 'react';
import AuthorizeHttpRequestService from './AuthorizeHttpRequestService';
import TimeConverterService from './TimeConverterService';

export default class TimeReportApiService extends React.Component<any, any>{
    
    authorizedApi:AuthorizeHttpRequestService;
    timeReportApiService: TimeConverterService;

    constructor(){
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
        console.log(jsonDate);
        return this.authorizedApi.authorizedGet('/api/TimeReport/GetDayTimeReports', [jsonDate])
        .then((response: any) => {
            return Promise.resolve(response);
        });
    }

    addTimeReport(Id: any, Date: any, Duration: any, ActivityId: any, Activity: any) {
        
    }

}