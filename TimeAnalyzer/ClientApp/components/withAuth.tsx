import * as React from 'react';
import AuthService from './AuthService';
import { Component } from 'react';
import { Redirect } from 'react-router-dom';

export default function withAuth(AuthComponent: any) {
    const Auth = new AuthService();
    return class AuthWrapped extends Component<any, any> {
        constructor() {
            super();
            this.state = {
                user: null
            }
        }

        componentDidMount() {
            if (!Auth.loggedIn()) {
                return window.location.replace("/login")
            }
            else {
                try {
                    if (Auth.loggedIn()) {
                        const profile = Auth.getProfile()
                        this.setState({
                            user: profile
                        })
                    }

                }
                catch (err) {
                    console.log("errorrrrrr", err);
                }
            }
        }

        render() {
            console.log("authLogged", Auth.loggedIn())
            if (!Auth.loggedIn() && typeof (Storage) !== "undefined"){
                    return <Redirect to={{pathname: "/login"}} />
                    
                }else{
                    return (
                        <AuthComponent history={this.props.history} user={this.state.user} />
                    )
                }
                

        }
    }
}

