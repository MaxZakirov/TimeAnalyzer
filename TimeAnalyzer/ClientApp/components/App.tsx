import * as React from 'react';
import { Component } from 'react';
import AuthService from './AuthService';
import withAuth from './withAuth';
import { Redirect } from 'react-router-dom'
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
                    <Chart />
                    <form>
                        <input type="submit" value="Get Out" onClick={()=>Auth.logout()} ></input>
                    </form>
                    
                </div>
            )

        
        
    }
}

export default withAuth(App);
