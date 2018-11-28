import * as React from 'react';
import { Route, Router } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home'
import Counter from './components/Counter';
import Registration from './components/Registration';
import Login from './components/Login';
import App from './components/App';

export const routes =
    <Layout>
        <Route exact path='/' component={App} />
        <Route path='/counter' component={Counter} />
        <Route path='/registration' component={Registration} />
        <Route path='/login' component={Login} />
    </Layout>;
