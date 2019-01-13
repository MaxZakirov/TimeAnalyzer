import * as React from 'react';
import axios from 'axios';
import AuthorizeHttpRequestService from './AuthorizeHttpRequestService';

var api = new AuthorizeHttpRequestService();

export default class UsersService extends React.Component<any, any> {

    getAllUsers() {
        return api.authorizedGet('/api/Users/GetAll', null)
            .then((response) => {
                return Promise.resolve(response);
            });
    }

    deleteUser(userModel: any)
    {
        return api.authorizedPost('/api/Users/Delete', userModel)
            .then((response) => {
                return Promise.resolve(response);
            });
    }
}