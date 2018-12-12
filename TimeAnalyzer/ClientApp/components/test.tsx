import * as React from 'react';
import AuthorizeHttpRequestService from './AuthorizeHttpRequestService';
import TimeReportApiService from './TimeReportApiService';

export default class Test extends React.Component<any,any>{
    Authorize:AuthorizeHttpRequestService;
    timeReport:TimeReportApiService;
    constructor(){
        super();
        this.Authorize = new AuthorizeHttpRequestService();
        this.timeReport = new TimeReportApiService();
    }

    test(){
        this.Authorize.authorizedGet('/api/TimeReport/GetUserTimeReports',null).then(res => console.log(res));
        this.timeReport.getTimeReports(this.timeReport, this.timeReport, this.timeReport, this.timeReport, this.timeReport).then(res => console.log("govno: ",res));
    }

    render(){
        return(
            <div>
                <button type="submit" onClick={() => this.test()}>
                    test
                </button>
            </div>
        )
    }
}