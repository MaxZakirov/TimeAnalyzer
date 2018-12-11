import * as React from 'react';
import AuthorizeHttpRequestService from './AuthorizeHttpRequestService';

export default class Test extends React.Component<any,any>{
    Authorize:AuthorizeHttpRequestService;
    constructor(){
        super();
        this.Authorize = new AuthorizeHttpRequestService();
    }

    test(){
        this.Authorize.authorizedGet('/api/TimeReport/GetUserTimeReports',null).then(res => console.log(res));
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