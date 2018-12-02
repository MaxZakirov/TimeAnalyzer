import * as React from 'react';
import { RouteComponentProps, Redirect } from 'react-router-dom';
import AuthService from './AuthService';
import Login from './Login';

interface RegistrationState { fields: any, errors: any }


export default class Registration extends React.Component<RouteComponentProps<{}>, RegistrationState> {
    constructor() {
        super();
        this.state = {
            fields: {},
            errors: {}
        }

        this.handleChange = this.handleChange.bind(this);
        this.submituserRegistrationForm = this.submituserRegistrationForm.bind(this);

    };

    handleChange(e: any) {
        var fields = this.state.fields;
        fields[e.target.name] = e.target.value;
        this.setState({
            fields
        });

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
        }

        let service: AuthService = new AuthService();
        var name: any = this.state.fields.username;
        var email: any = this.state.fields.email;
        var password: any = this.state.fields.password;
        console.log(name);
        console.log(email);
        console.log(password);
        service.checkIn(name, email, password);
        <Redirect
            to={{
                pathname: "/",
            }}
        />
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
            if (!fields["username"].match(/^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}$/)) {
                formIsValid = false;
                errors["username"] = "*Please enter alphabet characters only.";
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




    public render() {
        return (
            <div id="main-registration-container">
                <div id="register">

                    <div>

                    </div>
                    <h3>Welcome to our community!</h3>
                    <form name="userRegistrationForm" onSubmit={this.submituserRegistrationForm}>
                        <div className="group">

                            <input className="in" type="text" name="username" value={this.state.fields.username || ''} onChange={this.handleChange} />
                            <div className="errorMsg">{this.state.errors.username}</div>
                            <label htmlFor="username" id="name">Name</label>

                        </div>
                        <div className="group">

                            <input className="in" type="text" name="email" value={this.state.fields.email || ''} onChange={this.handleChange} />
                            <div className="errorMsg">{this.state.errors.email}</div>
                            <label htmlFor="email" id="email">Email</label>
                        </div>
                        <div className="group">

                            <input className="in" type="password" name="password" value={this.state.fields.password || ''} onChange={this.handleChange} />
                            <div className="errorMsg">{this.state.errors.password}</div>
                            <label htmlFor="password" id="pass">Enter yout password</label>

                        </div>
                        <div className="group">

                            <input className="in" type="password" name="passwordConfirm" value={this.state.fields.passwordConfirm || ''} onChange={this.handleChange} />
                            <div className="errorMsg">{this.state.errors.passwordConfirm}</div>
                            <label htmlFor="passwordConfirm" id="passConf">Confirm your password</label>

                        </div>

                        <input type="submit" className="button" value="Register" />
                        <p>Already have an account?</p>
                        <a href="/login">Login</a>
                    </form>
                </div>
            </div >

        );
    }


}
