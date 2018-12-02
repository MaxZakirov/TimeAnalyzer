import * as React from 'react';
import { Route, Router } from 'react-router-dom';
import { Layout } from './components/Layout';
import Login from './components/Login';
import App from './components/App';


export const routes =
    <Layout>
        <Route exact path='/' component={App} />
        <Route path='/login' component={Login} />
    </Layout>;
