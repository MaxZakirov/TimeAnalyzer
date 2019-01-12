import * as React from 'react';
import * as JWT from "jwt-decode";
import axios from 'axios';




export default class AuthService extends React.Component<any, any> {

    self = this;
    // Initializing important variables
    constructor() {
        super();
        this.state = {
            domain: 'http://localhost:54953'
        };
        this.fetch = this.fetch.bind(this) // React binding stuff
        this.login = this.login.bind(this)
        this.getProfile = this.getProfile.bind(this)
    }


    login(email: any, password: any) {
        // Get a token from api server using the fetch apiF
        return axios.post(`/api/Authentication/SignIn`, {
            email,
            password
        })
            .then((response: any) => {
                this.setToken(response.data.token) // Setting the token in localStorage
                return Promise.resolve(response);
            });
    }

    checkIn(name: any, email: any, password: any) {
        var body = JSON.stringify({
            name,
            password,
            email
        });

        return axios.post('/api/Authentication/CheckIn', {
            name: name,
            email: email,
            password: password
        }
        )
            .then((response) => {
                this.setToken(response.data.token) // Setting the token in localStorage
                return Promise.resolve(response);
            });
    }

    loggedIn() {
        // Checks if there is a saved token and it's still valid
        const token = this.getToken() // GEtting token from localStorage
        return !!token && !this.isTokenExpired(token) // handwaiving here
    }

    isTokenExpired(token: any) {
        try {
            const decoded: any = JWT(token);
            if (decoded.exp > (Date.now() / 1000)) { // Checking if token is expired.
                return false;
            }
            else
                return true;
        }
        catch (err) {
            return true;
        }
    }

    setToken(idToken: any) {
        if (typeof (window) !== "undefined") {
            window.localStorage.setItem('id_token', JSON.stringify(idToken))
        }


        // Saves user token to localStorage

    }

    getToken(): any {
        if (typeof (window) !== "undefined") {
            return window.localStorage.getItem('id_token') || "";
        }


        // Retrieves the user token from localStorage

    }

    logout(): any {
        if (typeof (window) !== "undefined") {
            window.localStorage.removeItem('id_token');
        }
    }

    getProfile(): any {
        // Using jwt-decode npm package to decode the token
        return JWT(this.getToken());
    }


    fetch(url: any, options: any) {
        // performs api calls sending the required authentication headers
        const headers: any = {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }

        // Setting Authorization header
        // Authorization: Bearer xxxxxxx.xxxxxxxx.xxxxxx
        if (this.loggedIn()) {
            headers['Authorization'] = 'Bearer ' + this.getToken()
        }

        return fetch(url, {
            headers,
            ...options
        })
            .then(this._checkStatus)
            .then(response => response)
    }

    _checkStatus(response: any) {
        // raises an error in case response status is not a success
        if (response.status >= 200 && response.status < 300) { // Success status lies between 200 to 300
            return response
        } else {
            var error: any = new Error(response.statusText)
            error.response = response
            throw error
        }
    }
}