import * as React from 'react';
import { Component } from 'react';
import AuthService from './AuthService';
import withAuth from './withAuth';
import { Redirect } from 'react-router-dom'
const Auth = new AuthService();


class App extends Component<any, any>{
    public render() {
        if (Auth.loggedIn()) {
            return (
                <div className="App">
                    <button onClick={Auth.logout()}>Hey</button>
                </div>
            )
        } else {
            return (
                <Redirect to={{ pathname: "/login" }} />
                )
        }
        
        
    }
}

export default withAuth(App);
