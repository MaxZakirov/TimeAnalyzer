import * as React from 'react';
import axios from 'axios';




export default class UsersService extends React.Component<any, any> {

    getAllUsers() {
        return axios.get('/api/Authentication/CheckIn')
            .then((response) => {
                return Promise.resolve(response);
            });
    }
}