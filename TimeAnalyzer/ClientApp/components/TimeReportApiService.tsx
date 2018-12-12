import * as React from 'react';
import AuthorizeHttpRequestService from './AuthorizeHttpRequestService';

export default class TimeReportApiService extends React.Component<any, any>{
    
    AuthorizedApi:AuthorizeHttpRequestService;

    constructor(){
        super();
        this.AuthorizedApi = new AuthorizeHttpRequestService();
    }
    
    getAllUserTimeReports(){  
        return this.AuthorizedApi.authorizedGet('http://localhost:54953/api/TimeReport/GetUserTimeReports', null)
    }

    addTimeReport(Id: any, Date: any, Duration: any, ActivityId: any, Activity: any) {
        
    }

}