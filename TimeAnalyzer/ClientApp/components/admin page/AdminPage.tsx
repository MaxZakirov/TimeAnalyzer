import * as React from 'react';
import AuthService from '../services/AuthService';
import { Route, Redirect, Router, Switch } from 'react-router-dom';
import TableUsers from './TableUsers';
import TableActivitiesTyped from './TableActivitiesTyped';
import TableActivities from './TableActivities';
import { Link } from 'react-router-dom'

export default class AdminPage extends React.Component<any, any>{
    Auth:AuthService;
    constructor() {
        super();

        this.Auth = new AuthService();
    }
    render() {
        return (
            <div>
                <div className="App">
                    <div className="topbar">
                        <div className="navbarBrand">
                            <Link to="/" style={{ color: "#eee", textDecoration: "none" }}>TimeAnalyzer</Link>
                        </div>
                        <div className="getOutBtn">
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