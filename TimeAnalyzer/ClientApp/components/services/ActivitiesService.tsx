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
        debugger;
        return this.authorizedApi.authorizedGet('/api/Activity/GetAllActivities', null);
    }

    getAllActivityTypes() {
        return this.authorizedApi.authorizedGet('/api/ActivityType/GetAllActivityTypes', null);
    }

    deleteActivity(activityModel:any){
        return this.authorizedApi.authorizedPost('/api/Activity/Delete', activityModel)
            .then((response) => {
                return Promise.resolve(response);
            });
    }
    deleteActivityType(activityTypedModel:any){
        return this.authorizedApi.authorizedPost('/api/ActivityType/Delete', activityTypedModel)
            .then((response) => {
                return Promise.resolve(response);
            });
    }

    editActivity(activity:any){
        return this.authorizedApi.authorizedPost('/api/Activity/Update', activity)
            .then((response) => {
                return Promise.resolve(response);
            });
    }

    addActivity(activity:any){
        return this.authorizedApi.authorizedPost('/api/Activity/Create', activity)
            .then((response) => {
                return Promise.resolve(response);
            });
    }
}