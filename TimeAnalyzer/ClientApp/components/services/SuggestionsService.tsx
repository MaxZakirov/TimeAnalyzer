import * as React from 'react';
import axios from 'axios';
import AuthorizeHttpRequestService from './AuthorizeHttpRequestService';
import TimeConverterService from './TimeConverterService';

var api = new AuthorizeHttpRequestService();

export default class SuggestionsService extends React.Component<any, any> {

    timeConverterService: TimeConverterService;
    authorizedApi: AuthorizeHttpRequestService;

    constructor() {
        super();

        this.timeConverterService = new TimeConverterService();
        this.authorizedApi = new AuthorizeHttpRequestService();
    }

    getAllSuggestions(date: any) {
        var jsonDate = this.timeConverterService.toServerFormatDate(date);
        return this.authorizedApi.authorizedGet('/api/Suggestions/GetForDay', [jsonDate]);
    }
}