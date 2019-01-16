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
    editActivity(name:any){
        return this.authorizedApi.authorizedPost('/api/Activity/Update', name)
            .then((response) => {
                return Promise.resolve(response);
            });
    }
    addActivity(name:any, typeId:any){
        
        return this.authorizedApi.authorizedPost('/api/Activity/Create', {name,typeId})
            .then((response) => {
                return Promise.resolve(response);
            });
    }
}