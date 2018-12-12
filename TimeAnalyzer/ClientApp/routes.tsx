import * as React from 'react';
import { Route, Router } from 'react-router-dom';
import { Layout } from './components/Layout';
import AuthPage from './components/AuthPage';
import App from './components/App';
import Test from './/components/test'


export const routes =
    <Layout>
        <Route exact path='/' component={App} />
        <Route path='/login' component={AuthPage} />
        <Route path='/t' component={Test} />
    </Layout>;
