import * as React from 'react';
import AuthService from './AuthService';
import axios from 'axios';

export default class AuthorizeHttpRequestService extends React.Component<any, any>{
    Auth: AuthService;
    constructor() {
        super();
        this.Auth = new AuthService();
    }

    getHeaders(): any {
        var token = this.Auth.getToken();
        return { Authorization: "Bearer ".concat(token.substr(1, token.length - 2)) };
    };

    authorizedPost(url: any, params: any) {
        debugger;
        return axios.post(
            url,
            params,
            { headers: this.getHeaders() }
        )
    };

    authorizedGet(url: any, params: any) {

        if (params != null) {
            url += this.initializeUrlParams(params);
        }

        console.log(url);

        return axios.get(
            url,
            {
                headers: this.getHeaders()
            }
        )
    };

    initializeUrlParams(params: any) {
        return params.map((p: any) => '/' + p).join('');
    }
}