import * as React from 'react';
import AuthorizeHttpRequestService from './AuthorizeHttpRequestService';
import TimeConverterService from './TimeConverterService';
import axios from "axios"

export default class ActivitiesService extends React.Component<any, any>{

    authorizedApi: AuthorizeHttpRequestService;

    constructor() {
        super();
        this.authorizedApi = new AuthorizeHttpRequestService();
    }

    getAllActivities() {
        return this.authorizedApi.authorizedGet('/api/Activity/GetAllActivities', null);
    }
}