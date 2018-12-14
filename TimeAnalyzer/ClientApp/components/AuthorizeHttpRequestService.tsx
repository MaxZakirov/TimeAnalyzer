import * as React from 'react';
import AuthService from './AuthService';
import axios from 'axios';

export default class AuthorizeHttpRequestService extends React.Component<any, any>{
     Auth: AuthService;
     constructor() {
         super();
         this.Auth = new AuthService();
     }

     getConfig(): any {
         var token = this.Auth.getToken();
        
         return { headers: { Authorization: "Bearer ".concat(token.substr(1,token.length-2)) } };
     };

     authorizedPost(url: any, params: any) {
         return axios.post(
             url,
             params,
             this.getConfig()
         )
     }

     authorizedGet(url: any, params: any) {
         var token = this.getConfig().headers;
         console.log(token);

         return axios.get(
             url,
             {
                 data: params,
                 headers: token 
             }
         )
     }
}