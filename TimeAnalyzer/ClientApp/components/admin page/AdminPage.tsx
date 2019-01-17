import * as React from 'react';
import AuthService from '../services/AuthService';
import { Route, Redirect, Router, Switch } from 'react-router-dom';
import TableUsers from './TableUsers';
import TableActivitiesTyped from './TableActivitiesTyped';
import TableActivities from './TableActivities';
import { Link } from 'react-router-dom'

export default class AdminPage extends React.Component<any, any>{
    Auth: AuthService;
    constructor() {
        super();

        this.Auth = new AuthService();
    }
    render() {
        return (
            <div className="App">
                <div className="topbar">
                    <div className="row">
                        <div className="col-sm-4">
                            <h2><Link to="/" style={{ color: "#eee", textDecoration: "none" }}>TimeAnalyzer</Link></h2>
                        </div>
                        <div className="col-sm-4">
                            <h3><Link to="/admin" style={{ color: "#eee", textDecoration: "none" }}>Administration</Link></h3>
                        </div>
                        <div className="getOutBtn col-sm-4">
                            <Link to="/login" onClick={() => this.Auth.logout()} className="getOut">Get Out</Link>
                        </div>
                    </div>
                </div>
                <div className="tableSection">
                    <div className="tables">
                        <TableUsers />
                        <TableActivities />
                        <TableActivitiesTyped />
                    </div>
                </div>
            </div>

        )
    }
}