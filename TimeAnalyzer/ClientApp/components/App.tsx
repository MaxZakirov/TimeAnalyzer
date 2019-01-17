import * as React from 'react';
import { Component } from 'react';
import AuthService from './services/AuthService';
import withAuth from './withAuth';
import { Link } from 'react-router-dom'
import Chart from './Chart'
const Auth = new AuthService();


class App extends Component<any, any>{

    componentDidMount() {
        if (!Auth.loggedIn()) {
            return window.location.replace("/login")
        }
    }

    public render() {

        return (
            <div className="App">
                <div className="topbar">
                    <div className="row">
                        <div className="col-sm-4">
                            <h2><Link to="/" style={{ color: "#eee", textDecoration: "none" }}>TimeAnalyzer</Link></h2>
                        </div>
                        <div className="getOutBtn col-sm-4">
                            <Link to="/login" onClick={() => Auth.logout()} className="getOut">Get Out</Link>
                        </div>
                    </div>
                </div>
                <Chart />
            </div>
        )
    }
}

export default withAuth(App);
