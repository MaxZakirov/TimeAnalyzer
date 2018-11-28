import * as React from 'react';
import AuthService from './AuthService';
import { Component } from 'react';
import { Redirect } from 'react-router-dom';

export default function withAuth(AuthComponent: any) {
    const Auth = new AuthService();
    return class AuthWrapped extends Component <any,any> {
        constructor() {
            super();
            this.state = {
                user: null
            }
        }

        componentWillMount() {
            if (!Auth.loggedIn()) {
                return <Redirect
                    to={{
                        pathname: "/login",
                    }}
                />
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
                    console.log(err);
                }
            }
        }

        render() {
            console.log(Auth.loggedIn())
            if (Auth.loggedIn()) {
                return (
                    <AuthComponent history={this.props.history} user={this.state.user} />
                )
            }
            else {
                return (
                    <Redirect to={{ pathname: "/login" }} />
                    )
            }
        }
    }
}

