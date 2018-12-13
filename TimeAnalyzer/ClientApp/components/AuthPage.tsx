import * as React from 'react';
import AuthService from './services/AuthService';
import { Route, Redirect, Router, Switch } from 'react-router-dom';
import * as $ from 'jquery';





export default class AuthPage extends React.Component<any, any>{
    Auth: AuthService;
    constructor() {
        super();
        this.state = {
            fields: {},
            errors: {}
        }
        this.handleChange = this.handleChange.bind(this);
        this.loginFormSubmit = this.loginFormSubmit.bind(this);
        this.submituserRegistrationForm = this.submituserRegistrationForm.bind(this);
        this.Auth = new AuthService();
    }

    handleChange(e: any) {
        var fields = this.state.fields;
        fields[e.target.name] = e.target.value;
        this.setState({
            [e.currentTarget.name]: e.currentTarget.value,
            fields
        });
    }

    registration() {
        if ($('.registration').hasClass('inv')) {
            $('.registration').removeClass('inv')
            $('.log').addClass('inv')
            $('.registrationLabel').addClass('blue')
            $('.logLabel').removeClass('blue')
            return false;
        }
    }
    log() {
        if ($('.log').hasClass('inv')) {
            $('.log').removeClass('inv')
            $('.registration').addClass('inv')
            $('.logLabel').addClass('blue')
            $('.registrationLabel').removeClass('blue')
            return false;
        }
    }

    submituserRegistrationForm(e: any) {
        e.preventDefault();
        if (this.validateForm()) {
            let fields: any = {};
            fields["username"] = "";
            fields["email"] = "";
            fields["password"] = "";
            fields["passwordConfirm"] = "";
            this.setState({ fields: fields });
            let service: AuthService = new AuthService();
            var name: any = this.state.fields.username;
            var email: any = this.state.fields.email;
            var password: any = this.state.fields.password;
            service.checkIn(name, email, password);
            window.location.replace("/")
        }


    }

    validateForm() {

        let fields = this.state.fields;
        let errors: any = {};
        let formIsValid = true;

        if (!fields["username"]) {
            formIsValid = false;
            errors["username"] = "*Please enter your username.";
        }

        if (typeof fields["username"] !== "undefined") {
            if (!fields["username"]) {
                formIsValid = false;
                errors["username"] = "*Please enter your username";
            }
        }

        if (!fields["email"]) {
            formIsValid = false;
            errors["email"] = "*Please enter your email-ID.";
        }

        if (typeof fields["email"] !== "undefined") {
            //regular expression for email validation
            var pattern = new RegExp(/^(("[\w-\s]+")|([\w-]+(?:\.[\w-]+)*)|("[\w-\s]+")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)/i);
            if (!pattern.test(fields["email"])) {
                formIsValid = false;
                errors["email"] = "*Please enter valid email-ID.";
            }
        }

        if (!fields["password"].match(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$/)) {
            formIsValid = false;
            errors["password"] = "*";
        }
        if (!fields["password"]) {
            formIsValid = false;
            errors["password"] = "*Please enter your password.";
        }

        if (!fields["passwordConfirm"]) {
            formIsValid = false;
            errors["passwordConfirm"] = "*Please repeat yourpassword.";
        }

        if (fields["passwordConfirm"] !== fields["password"]) {
            formIsValid = false;
            errors["passwordConfirm"] = "*Passwords do not matches";
        }



        this.setState({
            errors: errors
        });
        return formIsValid;
    }

    loginFormSubmit(e: any) {
        e.preventDefault();
        this.Auth.login(this.state.email, this.state.password)
            .then(response => {
                console.log("response", response)
                window.location.replace("/")
                window.location.reload();
            })
            .catch(err => {
                alert(err);
            })



    }



    public render() {
        if (this.Auth.loggedIn()) {
            return (
                <Redirect to={{ pathname: "/" }} />
            )
        } else {
            return (
                <div className="bodyLogin">

                    <div className="loginForm">

                        <div className="card">

                            <div className="leftSide">

                                <div className="leftSideContent">
                                    <h1>H</h1>
                                    <h2>Hey you!</h2>
                                    <h6>You feel you waste your time?</h6>
                                    <p>However after loading the components the body style is not present in the page.It shows up in the console though. Is there a way to display a background image in the main page whilst having the CSS rule in the CSS file that is loaded by webpack?Is this because reactjs does something I am not aware off?</p>
                                    <ul>
                                        <li>Mops</li>
                                        <li>Beer</li>
                                        <li>Bar</li>
                                    </ul>
                                </div>
                            </div>


                            <div className="rightSide">

                                <div className="rightSideContent">

                                    <div className="inCard">
                                        <h2 onClick={this.log} className="logLabel blue">Login</h2><h1>/</h1><h2 onClick={this.registration} className="registrationLabel">Registration</h2>

                                        <form className="registration inv" onSubmit={this.submituserRegistrationForm}>


                                            <div className="group">

                                                <label htmlFor="username" id="name">Name</label>
                                                <input className="form-control" placeholder="Username goes here..." type="text" name="username" value={this.state.fields.username || ''} onChange={this.handleChange} />
                                                <div className="errorMsg">{this.state.errors.username}</div>

                                            </div>
                                            <div className="group">

                                                <label htmlFor="email" id="email">Email</label>
                                                <input className="form-control" placeholder="Email goes here..." type="text" name="email" value={this.state.fields.email || ''} onChange={this.handleChange} />
                                                <div className="errorMsg">{this.state.errors.email}</div>

                                            </div>
                                            <div className="group">

                                                <label htmlFor="password" id="pass">Enter your password</label>
                                                <input className="form-control" placeholder="Password goes here..." type="password" name="password" value={this.state.fields.password || ''} onChange={this.handleChange} />
                                                <div className="errorMsg">{this.state.errors.password}</div>

                                            </div>
                                            <div className="group">

                                                <label htmlFor="passwordConfirm" id="passConf">Confirm your password</label>
                                                <input className="form-control" placeholder="Confirm your password" type="password" name="passwordConfirm" value={this.state.fields.passwordConfirm || ''} onChange={this.handleChange} />
                                                <div className="errorMsg">{this.state.errors.passwordConfirm}</div>

                                            </div>

                                            <div className="form-group">
                                                <button type="submit" className="btn btn-primary">SUBMIT</button>
                                            </div>

                                        </form>

                                        <form className="log" onSubmit={this.loginFormSubmit}>
                                            <h3>Welcome to our community!</h3>
                                            <div className="inputs">
                                                <div className="form-group">
                                                    <label>Email</label>
                                                    <input
                                                        className="form-control"
                                                        placeholder="Type your email here..."
                                                        name="email"
                                                        type="email"
                                                        onChange={this.handleChange} />
                                                </div>
                                                <div className="form-group">
                                                    <label>Password</label>
                                                    <input
                                                        className="form-control"
                                                        placeholder="Type your password here..."
                                                        name="password"
                                                        type="password"
                                                        onChange={this.handleChange} />
                                                </div>
                                                <button
                                                    className="btn btn-primary"
                                                    value="SUBMIT"
                                                    type="submit"
                                                >SUBMIT</button>
                                            </div>

                                        </form>
                                    </div>

                                </div>

                            </div>

                        </div>

                    </div>
                </div>


            );
        }

    }


}


