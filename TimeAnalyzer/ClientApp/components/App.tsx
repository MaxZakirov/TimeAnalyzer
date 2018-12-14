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
                        <div className="navbarBrand">
                            <Link to="/" style={{color: "#eee", textDecoration: "none"}}>TimeAnalyzer</Link>
                        </div>
                        <div className="getOutBtn">   
                        <Link to="/login" onClick={()=>Auth.logout()} className="getOut">Get Out</Link>
                        </div>
                          
                    </div>
                    <Chart />                 
                </div>
            )

        
        
    }
}

export default withAuth(App);
