import * as React from 'react';
import AuthService from './AuthService';
import { Route, Redirect, Router, Switch } from 'react-router-dom';




export default class Login extends React.Component<any, any>{
    Auth: AuthService;
    constructor() {
        super();
        this.handleChange = this.handleChange.bind(this);
        this.handleFormSubmit = this.handleFormSubmit.bind(this);
        this.Auth = new AuthService();
    }

    handleChange(e: React.FormEvent<HTMLInputElement>) {
        this.setState({ [e.currentTarget.name]: e.currentTarget.value })
    }

    handleFormSubmit(e: any) {
        e.preventDefault();
        console.log(this.state);

            this.Auth.login(this.state.email, this.state.password)
            .then(res => {
                if (this.Auth.loggedIn())

                    <Redirect to={{pathname:"/"}}  />
                
                console.log("done")
            })
            .catch(err => {
                alert(err);
            })

                
        
    }

    componentWillMount() {
        if (this.Auth.loggedIn()) {
            <Redirect 
                        to={{
                            pathname: "/",
                        }}
             />
        }
        
    }
    public render() {
        if (this.Auth.loggedIn()) {
            return (
                <Redirect to={{ pathname: "/" }} />
            )
        } else {
            return (
                <div className="container col-md-3">
                    <div className="form-group">
                        <div className="card">
                            <h1>Login</h1>
                            <form onSubmit={this.handleFormSubmit}>
                                <div className="form-group">
                                    <label htmlFor="exampleInputEmail1">Email address</label>
                                    <input
                                        className="form-control"
                                        placeholder="Username goes here..."
                                        name="email"
                                        type="email"
                                        onChange={this.handleChange} />
                                    <small id="emailHelp" className="form-text text-muted">We'll never share your email with anyone else.</small>
                                </div>
                                <div className="form-group">
                                    <label htmlFor="exampleInputPassword1">Password</label>
                                    <input
                                        className="form-control"
                                        placeholder="Password goes here..."
                                        name="password"
                                        type="password"
                                        onChange={this.handleChange} />
                                </div>
                                <button
                                    className="btn btn-primary"
                                    value="SUBMIT"
                                    type="submit"
                                >SUBMIT</button>
                            </form>
                        </div>

                    </div>
                </div>


            );
        }
        
    }

    
}


