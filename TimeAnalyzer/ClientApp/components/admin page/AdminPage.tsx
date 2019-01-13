import * as React from 'react';
import AuthService from '../services/AuthService';
import { Route, Redirect, Router, Switch } from 'react-router-dom';
import * as $ from 'jquery';
import TableUsers from './TableUsers';
import TableActivitiesTyped from './TableActivitiesTyped';
import TableActivities from './TableActivities';

export default class AdminPage extends React.Component<any, any>{
    render() {
        return (
            <div className="tables">
                <TableUsers/>
                <TableActivities/>
                <TableActivitiesTyped/>
            </div>
        )
    }
}