import * as React from 'react';
import { NavMenu } from './NavMenu';
import AuthService from './AuthService';
import Login from './Login';
import { Redirect, Link, Router } from 'react-router-dom';
import withAuth from './withAuth';



export class Layout extends React.Component<{}, {}> {
     public render() {
        const Auth = new AuthService();

            return <div className='container-fluid'>
            <div className='row'>
                <div className='col-sm-3'>
                    <NavMenu />
                </div>
                <div className='col-sm-9'>
                    { this.props.children }
                </div>
            </div>
        </div>;

        }
    }

